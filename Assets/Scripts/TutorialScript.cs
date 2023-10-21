using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public GameObject Tutorial0;
    public GameObject Tutorial00;
    public GameObject Tutorial000;
    public GameObject Tutorial1;
    public GameObject Tutorial10;
    public GameObject Tutorial2;
    public GameObject Tutorial20;
    public GameObject Tutorial3;
    public GameObject Tutorial30;
    public GameObject Tutorial4;
    public GameObject Tutorial40;
    public GameObject Tutorial5;
    public GameObject Tutorial50;
    public GameObject Tutorial6;
    public GameObject Tutorial60;
    public GameObject Tutorial7;
    public GameObject Tutorial70;
    public GameObject NextPhase;
    public GameObject Menu;
    private int state = -1;
    private float cron;
    // Start is called before the first frame update
    void Start()
    {
        cron = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextPhase.SetActive(true);
        }
        if (Time.time-cron > 7f && state == -1)
        {
            state += 1;
            Tutorial0.SetActive(false);
            Tutorial00.SetActive(false);
            Tutorial000.SetActive(false);
            Tutorial1.SetActive(true);
            Tutorial10.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Z) && state == 0)
        {
            state += 1;
            Tutorial1.SetActive(false);
            Tutorial10.SetActive(false);
            Tutorial2.SetActive(true);
            Tutorial20.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.X) && state == 1)
        {
            state += 1;
            Tutorial2.SetActive(false);
            Tutorial20.SetActive(false);
            Tutorial3.SetActive(true);
            Tutorial30.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.C) && state == 2)
        {
            state += 1;
            Tutorial3.SetActive(false);
            Tutorial30.SetActive(false);
            Tutorial4.SetActive(true);
            Tutorial40.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.V) && state == 3)
        {
            state += 1;
            Tutorial4.SetActive(false);
            Tutorial40.SetActive(false);
            Tutorial5.SetActive(true);
            Tutorial50.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.F) && state == 4)
        {
            state += 1;
            Tutorial5.SetActive(false);
            Tutorial50.SetActive(false);
            Tutorial6.SetActive(true);
            Tutorial60.SetActive(true);
        }
        if (Menu.activeSelf && state == 5)
        {
            state += 1;
            Tutorial6.SetActive(false);
            Tutorial60.SetActive(false);
            Tutorial7.SetActive(true);
            Tutorial70.SetActive(true);
        }
        if (Input.GetButtonDown("Jump") && state == 6)
        {
            state += 1;
            Tutorial7.SetActive(false);
            Tutorial70.SetActive(false);

        }
        if (state == 7)
        {
            NextPhase.SetActive(true);
        }
       

    }
}
