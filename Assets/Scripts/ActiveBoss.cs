using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBoss : MonoBehaviour
{
    public GameObject Boss;
    // Start is called before the first frame update
    void Start()
    {
        Boss.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
            Boss.SetActive(true);
            Destroy(gameObject);
        }

    }
}
