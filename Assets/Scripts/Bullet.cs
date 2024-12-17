using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerShooting>().takeBullets();
            other.GetComponent<PlayerShooting>().takeGrenades();
            Destroy(gameObject);
        }
    }
}
