using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform pauseMenu;
    public PlayerController player;
    public Scrollbar mainSlider1;
    public Scrollbar mainSlider2;
    public Scrollbar mainSlider3;
    void Start()
    {
        mainSlider1.value = 0;
        mainSlider1.size = 0.5f;
        mainSlider2.value = 0;
        mainSlider2.size = 0.5f;
        mainSlider3.value = 0;
        mainSlider3.size = 0.5f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            AddAttackExtra();
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            AddMaxHealth();
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            LessDodgeTime();
        }

    }

    public void AddAttackExtra()
    {
        if (player.GetRingCount() >= 5 && mainSlider1.size < 1f)
        {
            player.SetattackImprovement(50);
            player.SetRings(5);
            mainSlider1.size += 0.1f;
        }
    }

    public void AddMaxHealth() {
        if (player.GetRingCount() >= 5  && mainSlider2.size < 1f) {
            player.SetMaxHealth(200);
            player.SetRings(5);
            mainSlider2.size += 0.1f;
        }
    }

    public void LessDodgeTime()
    {
        if (player.GetRingCount() >= 5 && mainSlider3.size < 1f)
        {
            player.SetattackImprovement(50);
            player.SetDodgeTime(0.2f);
            mainSlider3.size += 0.1f;
        }
    }
}