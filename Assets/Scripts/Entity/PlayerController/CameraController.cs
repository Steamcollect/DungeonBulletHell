using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Follow")]
    public float moveTime = .2f;
    Vector3 posOffset = new Vector3(0, 0, -10);
    Vector3 velocity = Vector3.zero;
    Vector3 currentPos;
    Transform target;

    [Header("Camera shake")]
    [Range(0, 5)] public float shakeIntensity;
    [Range(.01f, .08f)] public float shakeSpeed;
    public CameraShakeType shakeType;

    float startingIntensity, shakeTimer, shakeTimerTotal;

    bool isPaused = false;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        currentPos = target.position + posOffset;

        StartCoroutine(ShakeInterpolation());
    }

    private void Update()
    {
        if (isPaused) return;

        Move();
        Shake();
    }

    void Move()
    {
        currentPos = Vector3.SmoothDamp(currentPos, target.position + posOffset, ref velocity, moveTime);
    }

    public void SetShaking(float intensity, float time)
    {
        startingIntensity = intensity;
        shakeIntensity = intensity;

        shakeTimerTotal = time;
        shakeTimer = time;
    }
    void Shake()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            shakeIntensity = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
        }
    }
    IEnumerator ShakeInterpolation(bool isNegative = false)
    {
        float startTime = Time.time;

        Vector3 initPos = transform.position, targetPos = new Vector3(Random.Range(-shakeIntensity, shakeIntensity), Random.Range(-shakeIntensity, shakeIntensity));

        while (Time.time < startTime + shakeSpeed)
        {
            float delta = 0;
            if(!isPaused) delta = (Time.time - startTime) / shakeSpeed;

            switch (shakeType)
            {
                case CameraShakeType.Rotate:
                    float rotation = Mathf.Lerp(isNegative ? shakeIntensity : -shakeIntensity, isNegative ? -shakeIntensity : shakeIntensity, delta);

                    transform.position = currentPos;
                    transform.rotation = Quaternion.Euler(0, 0, rotation);
                    break;
                
                case CameraShakeType.Move:
                    Vector3 position = Vector3.Lerp(initPos, currentPos + targetPos / 10, delta);

                    transform.position = position;
                    transform.rotation = Quaternion.identity;
                    break;
            }            

            yield return null;
        }

        StartCoroutine(ShakeInterpolation(!isNegative));
    }

    void OnPause()
    {
        isPaused = true;
    }
    void OnResume()
    {
        isPaused = false;
    }

    private void OnEnable()
    {
        GameStateManager.OnPaused += OnPause;
        GameStateManager.OnGameplay += OnResume;
    }
    private void OnDisable()
    {
        GameStateManager.OnPaused -= OnPause;
        GameStateManager.OnGameplay -= OnResume;
    }
}

[System.Serializable]
public enum CameraShakeType
{
    Rotate,
    Move
}