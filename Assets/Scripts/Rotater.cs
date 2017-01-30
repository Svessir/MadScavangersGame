using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Rotater : NetworkBehaviour {
    [SerializeField]
    float degreesPerSecond = 90;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, degreesPerSecond * Time.deltaTime);	
	}
}
