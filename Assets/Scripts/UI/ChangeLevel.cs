using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public string SceneChanger;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Change()
    {
        //Selection.SetActive(true);
        SceneManager.LoadScene(SceneChanger);
    }
}