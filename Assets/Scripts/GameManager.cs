using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

    private static GameManager instance;

    [SerializeField]
    private static ScavangerManager playerOne;

    [SerializeField]
    private static ScavangerManager playerTwo;

    [SerializeField]
    private RocketScript rocketOne;

    [SerializeField]
    private RocketScript rocketTwo;
    
    [SerializeField]
    private Grabable tool;

    private Grabable currentEnergyCell;

    public void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        if(isServer)
            InitialState();
    }

    public static void AssignPlayersToRockets()
    {
        instance.AssignPlayerOneToRocket();
        instance.AsssignPlayerTwoToRocket();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void AssignPlayerOne(GameObject player)
    {
        playerOne = player.GetComponent<ScavangerManager>();
        //rocketOne.owner = playerOne;
    }

    public static void AssignPlayerTwo(GameObject player)
    {
        playerTwo = player.GetComponent<ScavangerManager>();
        //rocketTwo.owner = playerTwo;
    }

    private void AssignPlayerOneToRocket()
    {
        rocketOne.owner = playerOne;
    }

    private void AsssignPlayerTwoToRocket()
    {
        rocketTwo.owner = playerTwo;
    }


    /*public void AssignPlayerOne1(GameObject player)
    {
        Debug.Log("isServer player one: " + isServer);
        playerOne = player.GetComponent<ScavangerManager>();
        rocketOne.owner = playerOne;
    }

    public void AssignPlayerTwo1(GameObject player)
    {
        Debug.Log("isServer player two: " + isServer);
        playerTwo = player.GetComponent<ScavangerManager>();
        rocketTwo.owner = playerTwo;
    }*/

    [Command]
    public void CmdAssignPlayerOne(GameObject player)
    {
        Debug.Log("isServer player one 2: " + isServer);
        playerOne = player.GetComponent<ScavangerManager>();
        rocketOne.owner = playerOne;
    }

    [Command]
    public void CmdAssignPlayerTwo(GameObject player)
    {
        Debug.Log("isServer player two 2: " + isServer);
        playerTwo = player.GetComponent<ScavangerManager>();
        rocketTwo.owner = playerTwo;
    }

    // <summary>
    // </summary>
    void InitialState()
    {
        tool.GrabEvent += ToolGrab;
    }

    /// <summary>
    /// </summary>
    /// <returns>
    /// </returns>
    /// <remarks>
    /// </remarks>
    private void ToolGrab()
    {
        Debug.Log("Tool was grabbed!");
        tool.GrabEvent -= ToolGrab;
        tool.DropEvent += ToolDrop;
        rocketOne.EnergyCellStolenEvent += RocketEnergyCellSteal;
        rocketTwo.EnergyCellStolenEvent += RocketEnergyCellSteal;
    }

    /// <summary>
    /// </summary>
    /// <returns>
    /// </returns>
    /// <remarks>
    /// </remarks>
    private void ToolDrop()
    {
        Debug.Log("Tool was Dropped!");
        tool.DropEvent -= ToolDrop;
        rocketOne.EnergyCellStolenEvent -= RocketEnergyCellSteal;
        rocketTwo.EnergyCellStolenEvent -= RocketEnergyCellSteal;
        InitialState();
    }

    /// <summary>
    /// </summary>
    /// <returns>
    /// </returns>
    /// <remarks>
    /// </remarks>
    private void RocketEnergyCellSteal(Grabable energyCell)
    {
        Debug.Log("Energy cell was stolen!");
        currentEnergyCell = energyCell;
        tool.DropEvent -= ToolDrop;
        rocketOne.EnergyCellStolenEvent -= RocketEnergyCellSteal;
        rocketTwo.EnergyCellStolenEvent -= RocketEnergyCellSteal;
        rocketOne.EnergyCellPlacedEvent += EnergyCellPlaced;
        rocketTwo.EnergyCellPlacedEvent += EnergyCellPlaced;
        currentEnergyCell.DropEvent += EnergyCellDrop;
        tool.gameObject.SetActive(false);
        RpcSetToolActive(false);
    }

    /// <summary>
    /// </summary>
    /// <returns>
    /// </returns>
    /// <remarks>
    /// </remarks>
    private void EnergyCellDrop()
    {
        Debug.Log("Energy cell was dopped!");
        rocketOne.EnergyCellPlacedEvent -= EnergyCellPlaced;
        rocketTwo.EnergyCellPlacedEvent -= EnergyCellPlaced;
        currentEnergyCell.DropEvent -= EnergyCellDrop;
        currentEnergyCell.GrabEvent += EnergyCellGrabbed;
    }

    /// <summary>
    /// </summary>
    /// <returns>
    /// </returns>
    /// <remarks>
    /// </remarks>
    private void EnergyCellGrabbed()
    {
        Debug.Log("Energy cell was grabbed");
        currentEnergyCell.GrabEvent -= EnergyCellGrabbed;
        rocketOne.EnergyCellPlacedEvent += EnergyCellPlaced;
        rocketTwo.EnergyCellPlacedEvent += EnergyCellPlaced;
        currentEnergyCell.DropEvent += EnergyCellDrop;
    }

    /// <summary>
    /// </summary>
    /// <returns>
    /// </returns>
    /// <remarks>
    /// </remarks>
    private void EnergyCellPlaced()
    {
        Debug.Log("Energy cell was placed");
        rocketOne.EnergyCellPlacedEvent -= EnergyCellPlaced;
        rocketTwo.EnergyCellPlacedEvent -= EnergyCellPlaced;
        currentEnergyCell.DropEvent -= EnergyCellDrop;
        tool.gameObject.SetActive(true);
        RpcSetToolActive(true);
        InitialState();
    }

    [ClientRpc]
    private void RpcSetToolActive(bool isActive)
    {
        //GameObject tool = GameObject.FindGameObjectWithTag("Tool");

        if (tool != null)
            tool.gameObject.SetActive(isActive);
    }
}
