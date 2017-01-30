using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScavangerFireScript : NetworkBehaviour {

    [SerializeField]
    Transform gunBarrelOutput;

    [SerializeField]
    float timeBetweenShots = .5f;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    int numberOfPallets = 10;

    [SerializeField]
    private ScavangerHealthManager healthManager;

    [SerializeField]
    private ParticleSystem muzzleFlash;

    [SerializeField]
    private AudioSource shotgunSound;

    float timeUntilNextShot;

	// Use this for initialization
	void Start () {
        timeUntilNextShot = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;
        
        if (Time.time >= timeUntilNextShot && Input.GetAxis("Fire1") > 0)
        {
            timeUntilNextShot = Time.time + timeBetweenShots;
            CmdShoot();
        }
	}

    [Command]
    private void CmdShoot()
    {
        for(int i = 0; i < numberOfPallets; i++)
        {
            Vector3 v = (gunBarrelOutput.forward.normalized + new Vector3(Random.Range(-.5f, .5f), Random.Range(-.2f, .2f), 0)).normalized;
            GameObject instance = Instantiate(bullet, gunBarrelOutput.position, gunBarrelOutput.rotation) as GameObject;
            instance.GetComponent<Rigidbody>().AddForce(v * 1000);
            NetworkServer.Spawn(instance);
            RpcPlayMuzzleFlash();
        }
    }

    [ClientRpc]
    private void RpcPlayMuzzleFlash()
    {
        muzzleFlash.Play();
        shotgunSound.Play();
    }
}
