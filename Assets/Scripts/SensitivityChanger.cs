using UnityEngine;
using UnityEngine.UI;

public class SensitivityChanger : MonoBehaviour
{
    [SerializeField] Slider slider;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("sensitivity");
    }

    public void setSensitivity()
    {
        PlayerPrefs.SetFloat("sensitivity", slider.value);
    }
}
