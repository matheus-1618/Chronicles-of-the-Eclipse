using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Before : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject game1;
    public GameObject game2;
    public GameObject game3;
    public GameObject game4;
    public float timeStart;
    void Start()
    {
        timeStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timeStart > 5f)
        {
            game1.SetActive(true);
            game2.SetActive(true);
            game3.SetActive(true);
            game4.SetActive(true);
            gameObject.SetActive(false);
        }
        
    }
}
