using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Collections;

public class HoverDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject textMeshProButton;
    public GameObject HopeScene;
    public GameObject RevengeScene;
    public AudioSource Soundtrack;
    public string SceneName;
    public GameObject BlackFade;
    // public Animator anim;

    void Start()
    {
        if (Soundtrack != null) { }
            //Soundtrack.Play();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // O cursor entrou no botão
        Debug.Log("Cursor entrou no botão.");
        HopeScene.SetActive(true);
        RevengeScene.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // O cursor saiu do botão
        Debug.Log("Cursor saiu do botão.");
/*        HopeScene.SetActive(false);
        RevengeScene.SetActive(true);*/
    }
/*    public void FadetoLevel()
    {
        anim.SetTrigger("FadeOut");
    }*/
    public void Change()
    {
        StartCoroutine(TransitionRoutine());
    }

    public IEnumerator TransitionRoutine()
    {
        Animator anim = BlackFade.GetComponent<Animator>();
        for (float i = 0; i < 0.2f; i += 0.2f)
        {
            anim.SetTrigger("FadeOut");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(SceneName);
        }
    }
}




