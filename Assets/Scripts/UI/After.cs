using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class After : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource final;
    public AudioSource initial;
    public GameObject game1;
    public GameObject game2;
    public GameObject game3;
    public GameObject game4;
    public float timeStart;
    void Start()
    {
        final.Play();
        initial.Stop();
        timeStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timeStart > 9.5f)
        {
            game1.SetActive(false);
            game2.SetActive(false);
            game3.SetActive(false);
            game4.SetActive(false);
            //gameObject.SetActive(false);
            SceneManager.LoadScene("Scenes/MenuPrincipal");

        }
    }
}
