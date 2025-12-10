using UnityEngine;

public class Sense : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject enemy;
    private float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            enemy.GetComponent<enemyPatrol>().isAlert = false;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.GetComponent<enemyPatrol>().isAlert = true;
            timer = 0;
        }

    }
}
