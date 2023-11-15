using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public GameObject Tutorial0;
    public GameObject Tutorial00;
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
    public GameObject Tutorial8;
    public GameObject Tutorial80;
    public GameObject NextPhase;
    public GameObject Menu;
    private int state = -1;
    private float cron;
    private int button = -1;
    private bool control = true;
    // Start is called before the first frame update
    void Start()
    {
        cron = Time.time;
        GameObject playercontroller = GameObject.FindWithTag("Player");
        PlayerController player = playercontroller.GetComponent<PlayerController>();
        player.TakeDamage(300,0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextPhase.SetActive(true);
        }
        if (Time.time-cron > 4.5f && state == -1)
        {
            button = 0;
            Tutorial0.SetActive(false);
            Tutorial00.SetActive(false);
            Tutorial1.SetActive(true);
            Tutorial10.SetActive(true);
        }
        if (state == 0)
        {
            //state += 1;
            Tutorial1.SetActive(false);
            Tutorial10.SetActive(false);
            Tutorial2.SetActive(true);
            Tutorial20.SetActive(true);
        }
        if (state == 1)
        {
            //state += 1;
            Tutorial2.SetActive(false);
            Tutorial20.SetActive(false);
            Tutorial3.SetActive(true);
            Tutorial30.SetActive(true);
        }
        if (state == 2)
        {
           // state += 1;
            Tutorial3.SetActive(false);
            Tutorial30.SetActive(false);
            Tutorial4.SetActive(true);
            Tutorial40.SetActive(true);
        }
        if (state == 3)
        {
            //state += 1;
            Tutorial4.SetActive(false);
            Tutorial40.SetActive(false);
            Tutorial5.SetActive(true);
            Tutorial50.SetActive(true);
        }
        if (state == 4)
        {
            //state += 1;
            Tutorial5.SetActive(false);
            Tutorial50.SetActive(false);
            Tutorial6.SetActive(true);
            Tutorial60.SetActive(true);
        }
        if (state == 5)
        {
            Tutorial6.SetActive(false);
            Tutorial60.SetActive(false);
            Tutorial7.SetActive(true);
            Tutorial70.SetActive(true);
        }
        if (state == 6)
        {
            Tutorial7.SetActive(false);
            Tutorial70.SetActive(false);
            Tutorial8.SetActive(true);
            Tutorial80.SetActive(true);
            GameObject playercontroller = GameObject.FindWithTag("Player");
            PlayerController player = playercontroller.GetComponent<PlayerController>();
            if (control)
            {
                player.GetCure();
                control = false;
            }
        }
        if (state == 7)
        {
            state += 1;
            Tutorial8.SetActive(false);
            Tutorial80.SetActive(false);
        }
        if (state == 8)
        {
            NextPhase.SetActive(true);
        }
       

    }
    public void Button1()
    {
        if (button == 0)
        {
            state += 1;
            button += 1;
        }
    }
    public void Button2()
    {
        if (button == 1)
        {
            state += 1;
            button += 1;
        }
    }
    public void Button3()
    {
        if (button == 2)
        {
            state += 1;
            button += 1;
        }
    }
    public void Button4()
    {
        if (button == 3)
        {
            state += 1;
            button += 1;
        }
    }
    public void DodgeButton()
    {
        if (button == 4)
        {
            state += 1;
            button += 1;
        }
    }
    public void MenuButton()
    {
        if (button == 5)
        {
            state += 1;
            button += 1;
        }
    }
    public void JumpButton()
    {
        if (button == 6)
        {
            state += 1;
            button += 1;
        }
    }
    public void CureButton()
    {
        if (button == 7)
        {
            state += 1;
            button += 1;
        }
    }
}
