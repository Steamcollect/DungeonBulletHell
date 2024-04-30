using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityState : MonoBehaviour
{
    public EntityStateExisting currentState = EntityStateExisting.Neutral;

    [Header("State color References")]
    public Color frozenColor;

    [Header("References")]
    public SpriteRenderer graphics;
    public Animator anim;

    public void SetFrozenSlow(float delay)
    {
        if (graphics) graphics.DOColor(frozenColor, .2f);
        if (anim) anim.speed = .5f;
        ChangeState(EntityStateExisting.Slow, delay);
    }
    public void SetFrozenStun(float delay)
    {
        if (graphics) graphics.DOColor(frozenColor, .2f);
        if (anim) anim.speed = 0f;
        ChangeState(EntityStateExisting.Stun, delay);
    }

    void ChangeState(EntityStateExisting newState, float delay)
    {
        StopAllCoroutines();
        if(anim && newState != EntityStateExisting.Stun) anim.speed = 1;

        StartCoroutine(StateDelay(newState, delay));
    }

    public float CalculateMoveSpeed(float moveSpeed)
    {
        if(currentState == EntityStateExisting.Slow) return moveSpeed / 2;
        else return moveSpeed;
    }

    IEnumerator StateDelay(EntityStateExisting state, float delay)
    {
        currentState = state;
        yield return new WaitForSeconds(delay);

        if (graphics && graphics.color != Color.white) graphics.DOColor(Color.white, .2f);
        if (anim) anim.speed = 1;

        currentState = EntityStateExisting.Neutral;
    }

    public EntityStateExisting GetState()
    {
        return currentState;
    }

    private void OnDestroy()
    {
        graphics.DOKill();
    }
}

[System.Serializable]
public enum EntityStateExisting
{
    Neutral,
    Slow,
    Stun,
    Burn
}