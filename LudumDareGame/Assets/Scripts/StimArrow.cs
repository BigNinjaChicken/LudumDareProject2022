using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StimArrow : MonoBehaviour
{
    private Transform target;
    private NavMeshPath path;

    public LineRenderer line;

    void Start()
    {
        path = new NavMeshPath();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }

    void Update()
    {

        NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);

        //for (int i = 0; i < path.corners.Length - 1; i++)
        //Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);

        //line.positionCount = path.corners.Length;
        //line.SetPositions(path.corners);

        line.positionCount = path.corners.Length;
        for (int i = 0; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i] - path.corners[0]);
        }
    }
}
