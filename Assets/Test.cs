using UnityEngine;

public class Test : MonoBehaviour
{
    int damage = 5;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().takeDamage(damage);
        }
    }
}
