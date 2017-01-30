using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Grabable : NetworkBehaviour {

    [SerializeField]
    private Vector3 hoverVector = new Vector3(0, 2, 0);
    private Vector3 initialPosition;
    
    private GameObject grabber;
	
    // Use this for initialization
	void Start () {
        initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(grabber != null)
        {
            transform.position = grabber.transform.position + hoverVector;
        }
	}

    public void Grab(GameObject theGrabber)
    {
        grabber = theGrabber;
    }

    public void Drop()
    {
        grabber = null;
        transform.position = initialPosition;
    }
}
