using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpText : MonoBehaviour
{
    public TMP_Text tmpText;

    public AnimationCurve dropCurve;
    public float dropForce;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)) CreatePopUp(Vector2.zero, false, "12");
    }

    public void CreatePopUp(Vector2 position, bool dropLeft, string text)
    {
        transform.position = position;
        this.tmpText.text = text;

        StartCoroutine(DropMovement(position, dropLeft));
    }

    IEnumerator DropMovement(Vector2 initialPosition, bool dropLeft)
    {
        float timer = 0;

        while(timer <= dropCurve.length)
        {
            transform.position = initialPosition + new Vector2(timer, dropLeft ? dropCurve.Evaluate(timer) * -1 : dropCurve.Evaluate(timer)) * dropForce;
            yield return null;

            timer += Time.deltaTime;
        }
    }
}