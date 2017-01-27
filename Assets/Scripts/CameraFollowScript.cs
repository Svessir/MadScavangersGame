using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

    [SerializeField]
    public Transform Target { get; set; }

    [SerializeField]
    public Vector3 Offset { get; set; }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Target != null)
        {
            transform.position = Target.position + Offset;
            transform.LookAt(Target);
        }
	}
}
