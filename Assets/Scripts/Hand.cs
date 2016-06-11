using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

    private Valve.VR.EVRButtonId trigger = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    SteamVR_TrackedObject trackedObj;
    GameObject heldObject;

    private Material originalMaterial;
    [SerializeField]
    Material glowMaterial;
    FixedJoint joint;
    Rigidbody attachPoint;
    bool holding = false;
    // Use this for initialization
    void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        attachPoint = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (heldObject != null)
        {
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                holding = true;
                heldObject.GetComponent<Renderer>().material = originalMaterial;
                //heldObject.transform.position = attachPoint.transform.position;

                joint = heldObject.AddComponent<FixedJoint>();
                joint.connectedBody = attachPoint;
            }
            else if (joint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {

                Rigidbody rigidbody = heldObject.GetComponent<Rigidbody>();

                Object.DestroyImmediate(joint);
                joint = null;

                var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
                if (origin != null)
                {
                    rigidbody.velocity = origin.TransformVector(device.velocity);
                    rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
                }
                else
                {
                    rigidbody.velocity = device.velocity;
                    rigidbody.angularVelocity = device.angularVelocity;
                }

                rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
       
        if (collider.tag == "Holdable")
        {
            heldObject = collider.gameObject;
            Renderer renderer = heldObject.GetComponent<Renderer>();
            originalMaterial = renderer.material;
            renderer.material = glowMaterial;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        
        if (collider.gameObject == heldObject && joint == null)
        {
            Debug.Log("TEST");
            heldObject.GetComponent<Renderer>().material = originalMaterial;
            heldObject = null;
        }

    }
}
