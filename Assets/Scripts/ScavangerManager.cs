using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScavangerManager : NetworkBehaviour
{
    [SerializeField]
    ScavangerHealthManager healthManager;

    [SerializeField]
    ScavangerFireScript scavangerFireScript;

    [SerializeField]
    ScavangerController scavangerController;

    [SerializeField]
    int numberOfSeconds = 5;

    RespawnManager respawnManager;

    // Use this for initialization
    void Start()
    {
        respawnManager = GameObject.FindGameObjectWithTag("RespawnManager").GetComponent<RespawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnDeath()
    {
        scavangerController.enabled = false;
        scavangerFireScript.enabled = false;
        StartCoroutine(RespawnCountDown());
    }

    public void OnRespawn()
    {
        scavangerController.enabled = true;
        scavangerFireScript.enabled = true;
        healthManager.HealUp();
    }

    private IEnumerator RespawnCountDown()
    {
        for (int i = 0; i < numberOfSeconds; i++)
        {
            yield return new WaitForSeconds(1);
        }

        respawnManager.Respawn(this);
    }
}
