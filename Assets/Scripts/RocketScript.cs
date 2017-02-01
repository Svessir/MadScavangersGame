using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public delegate void EnergyCellStealAction(Grabable eneregyCell);
public delegate void EnergyCellPlacedAction();

public class RocketScript : NetworkBehaviour
{
    [SerializeField]
    int numberOfEnergyCells;

    [SerializeField]
    int maxNumberOfEnergyCells;

    [SerializeField]
    GameObject energyCellPrefab;

    [SerializeField]
    Grabable tools;

    public ScavangerManager owner { get; set; }

    public event EnergyCellPlacedAction EnergyCellPlacedEvent;
    public event EnergyCellStealAction EnergyCellStolenEvent;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [ServerCallback]
    public void OnTriggerEnter(Collider other)
    {
        ScavangerManager scavanger = other.GetComponent<ScavangerManager>();

        if(scavanger != null && scavanger != owner)
        {
            Grabber grabber = scavanger.GetComponent<Grabber>();

            if(grabber.GetGrabbedObject() == tools)
            {
                if (numberOfEnergyCells > 0)
                {
                    Grabable energyCell = Instantiate(energyCellPrefab).GetComponent<Grabable>();
                    NetworkServer.Spawn(energyCell.gameObject);

                    if (EnergyCellStolenEvent != null)
                        EnergyCellStolenEvent(energyCell);

                    grabber.Swap(energyCell);
                    numberOfEnergyCells--;
                }
                else
                    grabber.Swap(null);
            }
        }
        else if(scavanger != null)
        {
            Grabber grabber = scavanger.GetComponent<Grabber>();
            Grabable grabbed = grabber.GetGrabbedObject();
            if (grabbed != null && grabbed.tag == "EnergyCell")
            {
                numberOfEnergyCells++;

                if (EnergyCellPlacedEvent != null)
                    EnergyCellPlacedEvent();

                grabber.Swap(null);
                Destroy(grabbed.gameObject);
            }
        }
    }
}
