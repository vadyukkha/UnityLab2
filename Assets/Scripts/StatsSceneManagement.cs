using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsSceneManagement : MonoBehaviour
{
    [SerializeField] TMP_Text textRecord;

    void Start()
    {
        if (PlayerPrefs.GetInt("record") % 60 < 10)
        {
            textRecord.text = $"{PlayerPrefs.GetInt("record") / 60}:0{PlayerPrefs.GetInt("record") % 60}";
        }
        else
        {
            textRecord.text = $"{PlayerPrefs.GetInt("record") / 60}:{PlayerPrefs.GetInt("record") % 60}";
        }
    }
}

