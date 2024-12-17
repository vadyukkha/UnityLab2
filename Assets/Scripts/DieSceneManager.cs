using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DieSceneManagemer : MonoBehaviour
{
    [SerializeField] TMP_Text textTime;

    void Start()
    {
        if (PlayerPrefs.GetInt("time") % 60 < 10)
        {
            textTime.text = $"{PlayerPrefs.GetInt("time") / 60}:0{PlayerPrefs.GetInt("time") % 60}";
        }
        else
        {
            textTime.text = $"{PlayerPrefs.GetInt("time") / 60}:{PlayerPrefs.GetInt("time") % 60}";
        }
    }
}
