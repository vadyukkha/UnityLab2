using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] LayerMask enemy1Layer;
    [SerializeField] LayerMask enemy2Layer;

    float timer = 0f;
    float lifetime = 3f;
    float distance = 3f;
    float damage = 110f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, distance, enemy1Layer);
            foreach (Collider collider in colliders)
            {
                collider.GetComponent<RunningEnemy>().takeDamage(damage);
            }

            colliders = Physics.OverlapSphere(transform.position, distance, enemy2Layer);
            foreach (Collider collider in colliders)
            {
                collider.GetComponent<HidingEnemy>().takeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
