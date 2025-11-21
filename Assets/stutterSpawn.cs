using UnityEngine;

public class stutterSpawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject L1;
    public GameObject L2;
    public GameObject L3;
    public GameObject L4;
    private float timer;

    void Start()
    {
        L1.SetActive(false);
        L2.SetActive(false);
        L3.SetActive(false);
        L4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 8)
        {
            L4.SetActive(true);
            L3.SetActive(false);
            timer = 0;
        }
        else if (timer > 6)
        {
            L3.SetActive(true);
            L2.SetActive(false);
        }
        else if (timer > 4)
        {
            L2.SetActive(true);
            L1.SetActive(false);
        }
        else if (timer >2)
        {
            L1.SetActive(true);
            L4.SetActive(false);
        }
    }
}
