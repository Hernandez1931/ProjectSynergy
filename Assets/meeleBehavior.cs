using UnityEngine;

public class meeleBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject player;
    private float baseSpeed;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        baseSpeed = this.gameObject.GetComponent<enemyPatrol>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);
        if (distance < 2)
        {
            this.gameObject.GetComponent<enemyPatrol>().speed = baseSpeed+1;
        }
        else
        {
            this.gameObject.GetComponent<enemyPatrol>().speed = baseSpeed;
        }
    }
}
