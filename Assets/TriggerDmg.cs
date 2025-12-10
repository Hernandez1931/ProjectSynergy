using UnityEngine;

public class TriggerDmg : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float dmg;
    private float timer;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            timer += Time.deltaTime;
            if (timer > .05  )
            {
                timer = 0;
                other.gameObject.GetComponent<PlayerHealth>().health -= dmg;
            }
        }
    }
}
