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
        if (Soundtrack != null)
        Soundtrack.Play();
    }

    public void FadetoLevel()
    {
        StartCoroutine(TransitionRoutine());
    }

    public void fadeComplete()
    {
        SceneManager.LoadScene("Scenes/Cutscene");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            FadetoLevel();
        }
    }

    public IEnumerator TransitionRoutine()
    {
        for (float i = 0; i < 0.2f; i += 0.2f)
        {
            anim.SetTrigger("FadeOut");
            yield return new WaitForSeconds(1f);
            fadeComplete();
        }
    }
}
