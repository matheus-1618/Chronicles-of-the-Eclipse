using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource Soundtrack;
    public Animator anim;

    void Start()
    {
        Soundtrack.Play();
    }

    public void FadetoLevel()
    {
        anim.SetTrigger("FadeOut");
    }

    public void fadeComplete()
    {
        SceneManager.LoadScene("Scenes/MenuSelection");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            FadetoLevel();
        }
    }
}
