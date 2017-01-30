using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private int playerOneCells;

    [SerializeField]
    private int playerTwoCells;

    [SerializeField]
    private GameObject playerOne;

    [SerializeField]
    private GameObject playerTwo;

    // Use this for initialization
    void Start () {
		// Subscribe to the toolgrab event
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Event function for the toolGrab event.
    /// Should unsubscribe to the toolGrab event and subscribe to the toolDrop event
    /// and the rocketEnergyCellSteal event.
    /// </summary>
    /// <returns>
    /// </returns>
    /// <remarks>
    /// </remarks>
    private void ToolGrab()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>
    /// </returns>
    /// <remarks>
    /// </remarks>
    private void ToolDrop()
    {
    }

    /// <summary>
    /// </summary>
    /// <returns>
    /// </returns>
    /// <remarks>
    /// </remarks>
    private void RocketEnergyCellSteal()
    {
    }

    /// <summary>
    /// </summary>
    /// <returns>
    /// </returns>
    /// <remarks>
    /// </remarks>
    private void EnergyCellDrop()
    {
    }

    /// <summary>
    /// </summary>
    /// <returns>
    /// </returns>
    /// <remarks>
    /// </remarks>
    private void EnergyCellPlaced()
    {
    }
}
