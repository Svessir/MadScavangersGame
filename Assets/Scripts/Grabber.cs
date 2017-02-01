using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Grabber : NetworkBehaviour
{
    private Grabable grabableObject;
    private ScavangerHealthManager healthManager;

    void Awake()
    {
        healthManager = GetComponent<ScavangerHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    [ServerCallback]
    void OnEnable()
    {
        healthManager.OnDeath += OnDeath;
    }

    [ServerCallback]
    void OnDisable()
    {
        healthManager.OnDeath -= OnDeath;
    }

    [ServerCallback]
    public void OnTriggerEnter(Collider other)
    { 
        if (grabableObject == null)
        {
            Grabable grabable = other.gameObject.GetComponent<Grabable>();

            if (grabable != null)
            {
                grabableObject = grabable;
                grabable.Grab(gameObject);
            }
        }
    }

    private void OnDeath()
    {
        if(grabableObject != null)
        {
            grabableObject.Drop();
            grabableObject = null;
        }
    }

    public Grabable GetGrabbedObject()
    {
        return grabableObject;
    }

    public Grabable Swap(Grabable grabable)
    {
        Grabable grab = grabableObject;
        grabableObject = grabable;

        if (grabable != null)
            grabable.Grab(gameObject);

        if (grab != null)
            grab.Drop();

        return grab;
    }
}
