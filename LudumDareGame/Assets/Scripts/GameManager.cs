using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Health/Stims")]
    public GameObject stimGroupObject;
    List<Transform> stimGroup;
    public GameObject stimPrefab;
    public int stimAmount = 1;
    public float minHealthSpawnDistance = 20f;

    [Header("Player")]
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        stimGroup = new List<Transform>(GetComponentsInChildren<Transform>());

        // Stim Amount
        for (int i = 0; i < stimAmount; i++) {
            spawnHealthPickUp();
        }
    }

    public void spawnHealthPickUp()
    {
        bool findingSpot = true;

        while (findingSpot)
        {
            int randomHealthSpawn = Random.Range(0, stimGroup.Count);

            if (Vector3.Distance(stimGroup[randomHealthSpawn].position, playerTransform.position) < minHealthSpawnDistance)
            {
                // Spawn new health
                Instantiate(stimPrefab, stimGroup[randomHealthSpawn]);

                findingSpot = false;
            } 
            else
            {
                Debug.Log("Find new spot.");
            }
        }
    }
}
