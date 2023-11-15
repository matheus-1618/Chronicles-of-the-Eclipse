using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSvene : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Selection;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gameObject.SetActive(false);
            fadeComplete();
        }


    }
    public void fadeComplete()
    {
        gameObject.SetActive(false);
        Selection.SetActive(true);
        //SceneManager.LoadScene("Scenes/MenuSelection");
    }

}
