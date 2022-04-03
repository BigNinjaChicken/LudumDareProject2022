
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Nodes")]
    public GameObject EnemyPrefab;
    public GameObject EnemyNodeGroupObject;
    List<EnemySpawnNode> spawnNodes;

    public int intialEnemyAmount = 4;

    public float tillSpawnAnotherEnemy = 20f;

    [Header("Spawn Enemy")]
    int failSpawn = 0;
    public Transform playerTransform;
    public float minHealthSpawnDistance = 40f;

    [Header("Timer")]
    public float maxTime = 100f;

    public float timeLeft = 100f;
    float timeUntilEnemySpawn;

    // Start is called before the first frame update
    void Start()
    {
        spawnNodes = new List<EnemySpawnNode>(EnemyNodeGroupObject.GetComponentsInChildren<EnemySpawnNode>());

        timeLeft = maxTime;

        for (int i = 0; i < intialEnemyAmount; i++)
        {
            spawnEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timeUntilEnemySpawn += Time.deltaTime;

        if (timeUntilEnemySpawn >= tillSpawnAnotherEnemy)
        {
            spawnEnemy();

            timeUntilEnemySpawn = 0;
        }
    }

    private void spawnEnemy()
    {
        failSpawn = 0;

        bool findingSpot = true;

        while (findingSpot)
        {
            int randomHealthSpawn = Random.Range(0, spawnNodes.Count);

            if (Vector3.Distance(spawnNodes[randomHealthSpawn].transform.position, playerTransform.position) < minHealthSpawnDistance)
            {
                // Spawn new health
                Instantiate(EnemyPrefab, spawnNodes[randomHealthSpawn].transform);

                findingSpot = false;
            }
            else
            {
                failSpawn++;
            }

            // Could not find health spawn spot
            if (failSpawn >= spawnNodes.Count)
            {
                Debug.Log(Vector3.Distance(spawnNodes[randomHealthSpawn].transform.position, playerTransform.position));
                findingSpot = false;
            }
            
        }
    }
}
