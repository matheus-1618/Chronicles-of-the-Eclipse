using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartInitial : MonoBehaviour
{
    // Start is called before the first frame update
    public string SceneChanger;
    void Start()
    {
        SceneManager.LoadScene(SceneChanger);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
