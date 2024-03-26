using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    public void SetMaxHealth(float maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    public void updateHealthBar(float currentValue, float maxValue)
    {
        healthBar.value = currentValue / maxValue;
    }
    public void SetHealth(float health)
    {
        healthBar.value = health;
    }
}
    