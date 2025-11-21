using UnityEngine;
using UnityEngine.Rendering;

public class enemyPatrol : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject pointA;
    public GameObject pointB;
    private GameObject player;
    private Rigidbody2D body;
    private Transform loc;
    public float speed;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        loc = pointB.transform;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
            Vector2 point = loc.position - transform.position;
            if (loc == pointB.transform)
            {
                body.linearVelocity = new Vector2(speed, 0);
            }
            else
            {
                body.linearVelocity = new Vector2(-speed, 0);
            }



            if (Vector2.Distance(transform.position, loc.position) < 0.5f && loc == pointB.transform)
            {
                loc = pointA.transform;
            }

            if (Vector2.Distance(transform.position, loc.position) < 0.5f && loc == pointA.transform)
            {
                loc = pointB.transform;
            }
    }
}
