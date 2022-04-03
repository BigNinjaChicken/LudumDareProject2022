using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;

    private Transform goal;

    private GameObject spawnNodeGroupObject;
    List<EnemySpawnNode> spawnNodes;

    // Animation
    public Animator animator;

    // How far away the enemy can be until the enemy picks a new spawn pos
    public float DistanceUntilRespawn = 20f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        spawnNodeGroupObject = GameObject.FindGameObjectWithTag("EnemySpawnGroup");
        spawnNodes = new List<EnemySpawnNode>(spawnNodeGroupObject.GetComponentsInChildren<EnemySpawnNode>());

        EnemySpawnNode node = gameObject.GetComponentInParent<EnemySpawnNode>();
        goal = node.playerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = goal.position;

        if (Vector3.Distance(goal.position, transform.position) > DistanceUntilRespawn)
        {
            findNewSpawn();

            // Play Animation
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealth>())
        {
            PlayerHealth playerHealthScript = other.GetComponent<PlayerHealth>();
            playerHealthScript.InsideEnemy();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerHealth>())
        {
            PlayerHealth playerHealthScript = other.GetComponent<PlayerHealth>();
            playerHealthScript.OutsideEnemy();
        }
    }

    private void findNewSpawn()
    {
        int randomHealthSpawn = Random.Range(0, spawnNodes.Count);
        transform.position = spawnNodes[randomHealthSpawn].transform.position;
    }
}
