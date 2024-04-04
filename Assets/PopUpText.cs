using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PopUpText : MonoBehaviour
{
    public float dropSpeed;
    public float dropForce;

    public TMP_Text tmpText;
    public AnimationCurve[] dropCurves;
 
    public void CreatePopUp(Vector2 position, bool dropLeft, string text)
    {
        transform.position = position;
        tmpText.text = text;
        tmpText.color = new Color(tmpText.color.r, tmpText.color.g, tmpText.color.b, 1);

        StartCoroutine(DropMovement(position, dropLeft));
    }

    IEnumerator DropMovement(Vector2 initialPosition, bool dropLeft)
    {
        float timer = 0;
        
        AnimationCurve curve = dropCurves.GetRandom();
        float curveTime = curve.keys[curve.length - 1].time;

        while(timer < curve.keys[curve.length - 1].time)
        {
            transform.position = initialPosition + new Vector2(dropLeft ? timer * -1 : timer, curve.Evaluate(timer)) * dropForce;
            yield return null;

            if(timer >= curveTime / 3 * 2)
            {
                float alpha = curveTime / 3 * 2 - (timer - curveTime / 3 * 2) / (curveTime / 3 * 2);
                tmpText.color = new Color(tmpText.color.r, tmpText.color.g, tmpText.color.b, alpha);
            }

            timer += dropSpeed * Time.deltaTime;
        }

        Hid();
        PopUpManager.instance.popUpTexts.Enqueue(this);
    }

    public void Hid()
    {
        tmpText.color = new Color(tmpText.color.r, tmpText.color.g, tmpText.color.b, 0);
    }
}