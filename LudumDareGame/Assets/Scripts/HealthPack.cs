using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealth>())
        {
            StimNode node = GetComponentInParent<StimNode>();
            node.nodeTaken = false;

            PlayerHealth playerHealthScript = other.GetComponent<PlayerHealth>();
            playerHealthScript.pickUpHealth();

            Destroy(gameObject);
        }
    }
}
