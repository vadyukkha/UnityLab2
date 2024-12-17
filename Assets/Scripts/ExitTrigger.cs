using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public RoomManager roomManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roomManager.BuildRoom(gameObject.transform.parent.gameObject);
            Destroy(gameObject); 
        }
    }
}

