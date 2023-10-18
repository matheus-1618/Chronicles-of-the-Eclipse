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
}




