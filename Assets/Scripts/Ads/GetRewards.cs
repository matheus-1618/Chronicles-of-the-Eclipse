using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetRewards : MonoBehaviour
{
    public Button button;
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetReward()
    {
        for (int i = 0; i < 10; i++)
        {
            player.GetRing();
        }
        button.interactable = false;

            
    }
}
