using UnityEngine;

public class buttonCol : MonoBehaviour
{
    public GameObject appear;
    public GameObject appear2;
    void Start()
    {
        appear.SetActive(false);
        appear2.SetActive(false);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            appear.SetActive(true);
            appear2.SetActive(true);
            Destroy(gameObject);
        }
    }
}
