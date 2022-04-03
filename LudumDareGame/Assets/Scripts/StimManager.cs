using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StimManager : MonoBehaviour
{
    [Header("Health/Stims")]
    public GameObject stimGroupObject;
    List<StimNode> stimGroup;
    public GameObject stimPrefab;
    public int stimAmount = 1;
    public float minHealthSpawnDistance = 20f;
    int failSpawn;

    [Header("Player")]
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        stimGroup = new List<StimNode>(stimGroupObject.GetComponentsInChildren<StimNode>());

        // Stim Amount
        for (int i = 0; i < stimAmount; i++) {
            spawnHealthPickUp();
        }
    }

    public void spawnHealthPickUp()
    {
        failSpawn = 0;

        bool findingSpot = true;

        while (findingSpot)
        {
            int randomHealthSpawn = Random.Range(0, stimGroup.Count);

            if(stimGroup[randomHealthSpawn].nodeTaken != true)
            {
                if (Vector3.Distance(stimGroup[randomHealthSpawn].transform.position, playerTransform.position) < minHealthSpawnDistance)
                {
                    // Take Node
                    stimGroup[randomHealthSpawn].nodeTaken = true;

                    // Spawn new health
                    Instantiate(stimPrefab, stimGroup[randomHealthSpawn].transform);

                    findingSpot = false;
                }
                else
                {
                    failSpawn++;
                }

                // Could not find health spawn spot
                if (failSpawn >= stimGroup.Count)
                {
                    Debug.Log(Vector3.Distance(stimGroup[randomHealthSpawn].transform.position, playerTransform.position));
                    findingSpot = false;
                }
            }
        }
    }
}
