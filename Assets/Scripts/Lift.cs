using UnityEngine;
using System.Collections;

public class Lift : MonoBehaviour, IActivatable {


    bool turned = false;
    Animator anim;

    // Use this for initialization
    void Start () {
	    anim = GetComponent<Animator>();
	}

  
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Activate()
    {
        if (turned)
        {
            anim.Play("TurnLiftBack");
            turned = false;
        } else
        {
            anim.Play("TurnLift");
            turned = true;
        }
    }


}
