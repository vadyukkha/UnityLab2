using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RoomManager : MonoBehaviour
{
    public GameObject[] roomPrefabs; 
    public GameObject player;     

    private List<GameObject> roomsBuffer = new List<GameObject>();

    public GameObject complitedRoom; 
    public GameObject currentRoom;
    public GameObject roomToBuild;


    void Start()
    {
        for (int i = 0; i < roomPrefabs.Count(); i++)
        {
            roomsBuffer.Add(roomPrefabs[i]);
        }
        int randi = Random.Range(0, roomsBuffer.Count - 1);
        roomToBuild = roomPrefabs[randi];
        roomsBuffer.RemoveAt(randi);
    }

    public void DeleteRoom()
    {
        Destroy(complitedRoom);
    }

    public void BuildRoom(GameObject completedRoom)
    {   
        complitedRoom = currentRoom;
        Quaternion rotation = Quaternion.Euler(0, 270, 0);
        currentRoom = Instantiate(roomToBuild, completedRoom.transform.position + new Vector3(0, 0, 50), rotation);
        ExitTrigger[] triggers = currentRoom.GetComponentsInChildren<ExitTrigger>();
        EntranceTrigger[] triggers2 = currentRoom.GetComponentsInChildren<EntranceTrigger>();
        foreach (ExitTrigger trigger in triggers)
        {
            trigger.roomManager = this;
        }
        foreach (EntranceTrigger trigger in triggers2)
        {
            trigger.roomManager = this;
        }
        if (roomsBuffer.Count == 0)
        {
            roomsBuffer = new List<GameObject>(roomPrefabs);
        }
        int randi = Random.Range(0, roomsBuffer.Count - 1);
        roomToBuild = roomsBuffer[randi];
        roomsBuffer.RemoveAt(randi);
    }
}
