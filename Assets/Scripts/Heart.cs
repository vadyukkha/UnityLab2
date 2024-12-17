using UnityEngine;

public class Heart : MonoBehaviour
{
    int heal = 10;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().takeHeal(heal);
            Destroy(gameObject);
        }
    }
}
