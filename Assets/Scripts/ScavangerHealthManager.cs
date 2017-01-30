using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public delegate void DeathAction();

public class ScavangerHealthManager : NetworkBehaviour {

    public event DeathAction OnDeath;
    Animator animator;

    [SyncVar]
    [SerializeField]
    private bool isDead = false;

    [SerializeField]
    private ScavangerManager scavangerManager;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            isDead = true;
            RpcOnZeroHealth();

            if (OnDeath != null)
                OnDeath();
        }
    }
    
    public void HealUp()
    {
        CmdHealUp();
    }

    [ClientRpc]
    private void RpcOnRevive()
    {
        animator.SetTrigger("IsAlive");
    }

    [ClientRpc]
    private void RpcOnZeroHealth()
    {
        animator.SetTrigger("IsDead");

        if (isLocalPlayer)
        {
            scavangerManager.OnDeath();
        }
    }

    [Command]
    private void CmdHealUp()
    {
        isDead = false;
        RpcOnRevive();
    }
}
