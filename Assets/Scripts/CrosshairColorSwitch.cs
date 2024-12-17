using UnityEngine;
using UnityEngine.UI;

public class CrosshairColorSwitch : MonoBehaviour
{
    [SerializeField] Image crosshair;

    void Awake()
    {
        crosshair.color = Color.green;
    }
    
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.tag == "Enemy1" || hit.collider.tag == "Enemy2")
            {
                crosshair.color = Color.red;
            }
            else
            {
                crosshair.color = Color.green;
            }
        }
        else
        {
            crosshair.color = Color.green;
        }
    }
}
