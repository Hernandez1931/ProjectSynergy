using System.Runtime.CompilerServices;
using UnityEngine;

public class activate : MonoBehaviour
{
    public GameObject appear;
    void Start()
    {
        appear.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            appear.SetActive(true); 
            Destroy(gameObject);
        }
    }
}
