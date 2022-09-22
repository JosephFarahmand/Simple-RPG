using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    EnemyStats stats;

    Camera cam;

    float visibleTime = 5;
    private float laseMadeVisibleTime;

    private void Start()
    {
        cam = Camera.main;

        stats = GetComponentInParent<EnemyStats>();
        stats.OnChangeHealth += Stats_OnChangeHealth;

        healthSlider.maxValue = stats.maxHealth;
        healthSlider.value = stats.maxHealth;

        healthSlider.gameObject.SetActive(false);
    }

    private void Stats_OnChangeHealth(float maxHealth, float currentHealth)
    {
        if (currentHealth != maxHealth)
        {
            healthSlider.gameObject.SetActive(true);

        }
        laseMadeVisibleTime = Time.deltaTime;
        healthSlider.value = currentHealth;
    }

    private void LateUpdate()
    {
        transform.forward = -cam.transform.forward;

        if (Time.deltaTime - laseMadeVisibleTime > visibleTime)
        {
            healthSlider.gameObject.SetActive(false);
        }
    }
}
