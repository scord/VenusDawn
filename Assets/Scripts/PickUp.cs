using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

    BoxCollider pickUpTrigger;
    Renderer renderer;

    [SerializeField]
    Material glowMaterial;
    Material originalMaterial;

    // Use this for initialization

    void Start () {
        renderer = GetComponent<Renderer>();
        pickUpTrigger = gameObject.AddComponent<BoxCollider>();
        pickUpTrigger.isTrigger = true;
        pickUpTrigger.size = new Vector3(1.2f, 1.2f, 1.2f);
        originalMaterial = renderer.material;

    }
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand")
            renderer.material = glowMaterial;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hand")
            renderer.material = originalMaterial;
    }
}
