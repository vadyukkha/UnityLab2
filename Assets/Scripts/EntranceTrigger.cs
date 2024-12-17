using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceTrigger : MonoBehaviour
{
    public RoomManager roomManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roomManager.DeleteRoom();
            Destroy(gameObject); 
        }
    }
}
