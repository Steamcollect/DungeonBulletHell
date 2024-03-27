using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class InteractiveButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool canScale, canPlaySound;

    public Vector2 initialScale, finalScale;
    public float moveTime;

    public AudioClip[] selectSounds, interactSounds;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(canScale) transform.DOScale(finalScale, moveTime);
        if (canPlaySound && selectSounds.Length > 0)
            AudioManager.instance.PlayClipAt(0, Vector2.zero, selectSounds.GetRandom());
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (canScale) transform.DOScale(initialScale, moveTime);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (canPlaySound && interactSounds.Length > 0)
            AudioManager.instance.PlayClipAt(0, Vector2.zero, interactSounds.GetRandom());
    }
}