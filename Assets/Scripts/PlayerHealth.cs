using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Image imageHealthbar;
    [SerializeField] TMP_Text textHealthbar;

    [SerializeField] Image effect;

    Timer timer;

    float effectDuration = 0.2f;
    float effectTimer = 0f;
    float effectTransparency = 0.2f; 

    float maxHealth = 100f;
    float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        timer = GetComponent<Timer>();
    }

    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        imageHealthbar.fillAmount = currentHealth / maxHealth;
        textHealthbar.text = $"{currentHealth} / {maxHealth}";

        if (effectTimer > 0)
        {
            effectTimer -= Time.deltaTime;
        }
        else
        {
            effect.enabled = false;
        }

        if (currentHealth == 0)
        {
            PlayerPrefs.SetInt("time", timer.getTime());
            if (timer.getTime() > PlayerPrefs.GetInt("record"))
            {
                PlayerPrefs.SetInt("record", timer.getTime());
            }
            SceneManager.LoadScene(4);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        effect.color = new Color(255, 0, 0, effectTransparency);
        effect.enabled = true;
        effectTimer = effectDuration;
    }

    public void takeHeal(float heal)
    {
        currentHealth += heal;
        effect.color = new Color(0, 255, 0, effectTransparency);
        effect.enabled = true;
        effectTimer = effectDuration;
    }
}
