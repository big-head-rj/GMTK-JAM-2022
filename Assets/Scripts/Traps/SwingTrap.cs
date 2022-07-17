using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingTrap : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if(PlayerController.Instance._isAlive == true) PlayerController.Instance.Dead();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (PlayerController.Instance._isAlive == true) PlayerController.Instance.Dead();
        }
    }
}