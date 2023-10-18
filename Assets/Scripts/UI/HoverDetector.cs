using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class HoverDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject textMeshProButton;
    public GameObject HopeScene;
    public GameObject RevengeScene;


    public void OnPointerEnter(PointerEventData eventData)
    {
        // O cursor entrou no bot�o
        Debug.Log("Cursor entrou no bot�o.");
        HopeScene.SetActive(true);
        RevengeScene.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // O cursor saiu do bot�o
        Debug.Log("Cursor saiu do bot�o.");
/*        HopeScene.SetActive(false);
        RevengeScene.SetActive(true);*/
    }
}




