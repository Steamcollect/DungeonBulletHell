using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public Queue<PopUpText> popUpTexts = new Queue<PopUpText>();
    public GameObject popUpTextPrefabs;

    public static PopUpManager instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < 30; i++) popUpTexts.Enqueue(CreatePopUpText());
    }

    public void HitPopUp(Vector2 pos, bool isLeft, string text)
    {
        if (popUpTexts.Count <= 0) popUpTexts.Enqueue(CreatePopUpText());
        PopUpText tmp = popUpTexts.Dequeue();

        tmp.gameObject.SetActive(true);
        tmp.CreatePopUp(pos, isLeft, text);
    }

    PopUpText CreatePopUpText()
    {
        PopUpText tmpPopUp = Instantiate(popUpTextPrefabs, transform).GetComponent<PopUpText>();
        tmpPopUp.Hid();
        return tmpPopUp;
    }
}