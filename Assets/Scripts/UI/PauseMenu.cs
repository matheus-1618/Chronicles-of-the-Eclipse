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
    public static float size1 = 0.5f;
    public static float size2 = 0.5f;
    public static float size3 = 0.5f;
    public AudioSource upgrade;
    void Start()
    {
        mainSlider1.value = 0;
        mainSlider1.size = size1;
        mainSlider2.value = 0;
        mainSlider2.size = size2;
        mainSlider3.value = 0;
        mainSlider3.size = size3;

    }

    // Update is called once per frame
    void Update()
    {
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

    public void ToPause()
    {
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
    public void AddAttackExtra()
    {
        if (player.GetRingCount() >= 5 && mainSlider1.size < 1f)
        {
            upgrade.Play();
            player.SetattackImprovement(5);
            player.SetRings(5);
            size1 += 0.1f;
            mainSlider1.size += 0.1f;
        }
    }

    public void AddMaxHealth() {
        if (player.GetRingCount() >= 5  && mainSlider2.size < 1f) {
            upgrade.Play();
            player.SetMaxHealth(100);
            player.SetRings(5);
            size2 += 0.1f;
            mainSlider2.size += 0.1f;
        }
    }

    public void LessDodgeTime()
    {
        if (player.GetRingCount() >= 5 && mainSlider3.size < 1f)
        {
            upgrade.Play();
            player.SetRings(5);
            player.SetDodgeTime(0.2f);
            size3 += 0.1f;
            mainSlider3.size += 0.1f;
        }
    }
}
