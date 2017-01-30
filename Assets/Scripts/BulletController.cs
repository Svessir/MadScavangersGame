using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletController : NetworkBehaviour
{
    [SerializeField]
    float timeToLive = 0.5f;
    float timeOfDestruction;

    // Use this for initialization
    void Start()
    {
        Physics.IgnoreLayerCollision(gameObject.layer, gameObject.layer);
        timeOfDestruction = Time.time + timeToLive;
    }

    [ServerCallback]
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timeOfDestruction)
            Destroy(gameObject);
    }

    [ServerCallback]
    public void OnCollisionEnter(Collision collision)
    {
        ScavangerHealthManager healthManager = collision.gameObject.GetComponent<ScavangerHealthManager>();
        if (healthManager != null)
        {
            healthManager.TakeDamage(1);
        }

        // Destroy the pallet
        Destroy(gameObject);
    }
}
