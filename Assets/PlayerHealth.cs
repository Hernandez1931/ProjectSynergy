using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float health;
    public float MaxHealth;
    public Image HealthBar;
    void Start()
    {
        MaxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.fillAmount = Mathf.Clamp(health / MaxHealth, 0, 1);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
