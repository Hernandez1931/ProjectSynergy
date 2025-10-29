using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour {

    public float speed = 4.5f;
    public float jumpForce = 12f;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D box;

    // Start is being called before the first frame update
    void Start() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, body.linearVelocity.y);
        body.linearVelocity = movement;

        // tells animator controller how fast we're goin
        anim.SetFloat("speed", Mathf.Abs(deltaX));

        // flips sprite left/right depending on player's direction
        if (!Mathf.Approximately(deltaX, 0)) {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }

        // creates bounding box with player. 
        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - 0.1f);
        Vector2 corner2 = new Vector2(min.x, min.y - 0.2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        // will evaluate to either true or false depending on hit.
        // so if hit = true, grounded = false.
        bool grounded = hit != null;

        // Detects if player is on moving platform. If so, player moves in tandem with platform.
        MovingPlatform platform = null;
        if (grounded) {
            platform = hit.GetComponent<MovingPlatform>();
        }

        if (platform != null) {
            transform.parent = platform.transform;
        }
        else {
            transform.parent = null;
        }

        Vector3 playerScale = Vector3.one;
        if (platform != null) {
            playerScale = platform.transform.localScale;
        }

        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) / playerScale.x, 1 / playerScale.y, 1);
        }
        
        // allows for jumping.
        body.gravityScale = (grounded && Mathf.Approximately(deltaX, 0)) ? 0: 1;

        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }


    }
}