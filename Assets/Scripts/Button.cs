using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    [SerializeField]
    public GameObject subject;
    private IActivatable activatable;
    private Vector3 origin;
    private bool off = true;
    [SerializeField]
    private float activationDepth;

    private AudioSource audioSource;

    void Start()
    {
        activatable = subject.GetComponent<IActivatable>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x + 0.1f*Time.deltaTime, 0, 0);
        if (transform.localPosition.x > 0)
        {
            transform.localPosition = Vector3.zero;
            off = true;
        }
        if (transform.localPosition.x < -activationDepth)
        {
            transform.localPosition = new Vector3(-activationDepth, 0, 0);
            if (off)
            {
           
                off = false;
                audioSource.Play();
                activatable.Activate();
            }
            }
       // Debug.Log(transform.localPosition.x);
    }
}
