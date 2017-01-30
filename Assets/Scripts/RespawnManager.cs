using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour {

    [SerializeField]
    private Transform[] respawnPoints;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Respawn(ScavangerManager scavanger)
    {
        GameObject[] scavangers = GameObject.FindGameObjectsWithTag(scavanger.gameObject.tag);
        Vector3? enemyScavangerPosition = null;
        foreach(var i_scavanger in scavangers)
        {
            if(i_scavanger != scavanger.gameObject)
            {
                enemyScavangerPosition = i_scavanger.transform.position;
                break;
            }
        }

        Transform furthestSpawnPoint = respawnPoints[0];
        float longestDistance = 0;
        Vector3 scalar = new Vector3(1, 0, 1);
        foreach(var spawnPoint in respawnPoints)
        {
            Vector3 v = spawnPoint.position - (Vector3)enemyScavangerPosition;
            v.Scale(scalar);
            float distance = v.magnitude;

            if(distance > longestDistance) {
                furthestSpawnPoint = spawnPoint;
                longestDistance = distance;
            }
        }

        scavanger.transform.position = furthestSpawnPoint.position;
        scavanger.OnRespawn();
    }
}
