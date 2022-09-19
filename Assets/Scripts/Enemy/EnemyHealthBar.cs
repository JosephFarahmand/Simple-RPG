using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    EnemyStats stats;

    private void Start()
    {
        stats = GetComponentInParent<EnemyStats>();
        stats.OnChangeHealth += Stats_OnChangeHealth;

        healthSlider.maxValue = stats.maxHealth;
        healthSlider.value = stats.maxHealth;

        healthSlider.gameObject.SetActive(false);
    }

    private void Stats_OnChangeHealth(float value)
    {
        if(value != stats.maxHealth)
        {
            healthSlider.gameObject.SetActive(true);
        }
        healthSlider.value = value;
    }
}
