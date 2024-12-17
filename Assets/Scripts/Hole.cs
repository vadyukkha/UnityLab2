using UnityEngine;

public class Hole : MonoBehaviour
{
    float timer = 0f;
    float lifetime = 1f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
