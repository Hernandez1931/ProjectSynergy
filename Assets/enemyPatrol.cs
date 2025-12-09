using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.Video;

public class enemyPatrol : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Tilemap tilemap;
    private GameObject player;
    private Rigidbody2D body;
    public GameObject coneR;
    public GameObject coneL;
    public float hp;
    private float leftB;
    private float rightB;
    private bool left;
    public float speed;
    private float timer;
    public bool isAlert;
    public bool isChasing;
    public bool canAttack;
    private float aTimer;
    private float cTimer;
    public GameObject attack;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        GetCurrentPlatformLength();
        isChasing = false;
        canAttack = false;
        isAlert = false;
        isChasing = false;
        canAttack = false;
        aTimer = 1;
        attack.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp == 0)
        {
            Destroy(gameObject);
            return;
        }
        timer += Time.deltaTime;
        Vector3 worldPos = transform.position + Vector3.down * 0.1f;
        Vector3Int cellPos = tilemap.WorldToCell(worldPos);
        Vector3 pWorldPos = player.transform.position;
        Vector3Int pCellPos = tilemap.WorldToCell(pWorldPos);
        Debug.Log(pCellPos.x - cellPos.x);
        if (canAttack)
        {
            if ((pCellPos.x - cellPos.x) < -5 || (pCellPos.x - cellPos.x) > 5)
            {
                canAttack = false;
            }
            if (aTimer > 1)
            {
                attack.SetActive(true);
                aTimer = 0;
            }
            else
            {
                attack.SetActive(false);
                aTimer += Time.deltaTime;
            }
            if ((pCellPos.x - cellPos.x) < 5 && (pCellPos.x - cellPos.x) > -5)
            {
                body.linearVelocity = new Vector2(0, 0);
            }
        }
        else if (isChasing)
        {
            if (!isAlert)
            {
                isChasing = false;
                return;
            }
            if (cellPos.x > rightB || cellPos.x < leftB)
            {
                Debug.Log(rightB);
                Debug.Log(leftB);
                Debug.Log(cellPos.x);
                body.linearVelocity = new Vector2(0, 0);
            }
            else if ((pCellPos.x - cellPos.x) < 7 && (pCellPos.x - cellPos.x) > -7)
            {
                canAttack = true;
                if ((pCellPos.x - cellPos.x) < 5 && (pCellPos.x - cellPos.x) >- 5)
                {
                    body.linearVelocity = new Vector2(0, 0);
                }
            }
            else
            {
                if (cellPos.x > pCellPos.x)
                {
                    body.linearVelocity = new Vector2(-speed, 0);
                }
                else
                {
                    body.linearVelocity = new Vector2(speed, 0);
                }
            }
        }
        else if (isAlert)
        {
            Debug.Log("alert");
            body.linearVelocity = new Vector2(0, 0);
            coneL.SetActive(true);
            coneR.SetActive(true);
            cTimer += Time.deltaTime;
            if (cTimer > 1)
            {
                isChasing = true;
            }
            return;
        }
        
        
        else if(left)
        {
            if (timer > 1.5)
            {
                body.linearVelocity = new Vector2(-speed, 0);
            }
            else if (timer > 1)
            {
                coneL.SetActive(true);
            }
            else if (timer > .5)
            {
                coneR.SetActive(false);
            }
        }
        else
        {
            if (timer > 1.5)
            {
                body.linearVelocity = new Vector2(speed, 0);
            }
            else if (timer > 1)
            {
                coneR.SetActive(true);
            }
            else if (timer > .5)
            {
                coneL.SetActive(false);
            }
        }

        if (cellPos.x < leftB)
        {
            left = false;
            if (timer > 2.5)
            {
                timer = 0;
                Debug.Log("left");
                body.linearVelocity = new Vector2(0, 0);
            }
        }

        if (cellPos.x > rightB)
        {
            left = true;
            if (timer > 2.5)
            {
                timer = 0;
                Debug.Log("right");
                body.linearVelocity = new Vector2(0, 0);
            }
        }
    }

    public void GetCurrentPlatformLength()
    {
        // 1. Get tile position under the object
        Vector3 worldPos = transform.position + Vector3.down * 0.1f;
        Vector3Int below = tilemap.WorldToCell(worldPos);
        Vector3Int cellPos = new Vector3Int(below.x, below.y - 5, below.z);
        Debug.Log(cellPos);

        if (!tilemap.HasTile(cellPos))
        {
            Debug.Log("Not standing on a tile.");
            return ;
        }

        // 2. Scan left
        int left = cellPos.x;
        while (tilemap.HasTile(new Vector3Int(left - 1, cellPos.y, cellPos.z)))
        {
            left--;
        }

        // 3. Scan right
        int right = cellPos.x;
        while (tilemap.HasTile(new Vector3Int(right + 1, cellPos.y, cellPos.z)))
        {
            right++;
        }

        Debug.Log(right);
        Debug.Log(left);
        rightB = right - 1.5f;
        leftB = left + 1.5f;


        return;
    }
}
