using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public delegate void DropAction();
public delegate void GrabAction();

public class Grabable : NetworkBehaviour {

    [SerializeField]
    private Vector3 hoverVector = new Vector3(0, 2, 0);
    private Vector3 initialPosition;
    
    private GameObject grabber;

    public event GrabAction GrabEvent;
    public event DropAction DropEvent;
	
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

        if (GrabEvent != null)
            GrabEvent();
    }

    public void Drop()
    {
        grabber = null;
        transform.position = initialPosition;

        if (DropEvent != null)
            DropEvent();
    }
}
