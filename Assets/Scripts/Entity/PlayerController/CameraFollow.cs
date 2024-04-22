using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    Vector3 posOffset = new Vector3(0, 0, -10);
    Vector3 velocity = Vector3.zero;
    float moveTime = .2f;

    bool isPaused = false;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isPaused) return;

        transform.position = Vector3.SmoothDamp(transform.position, target.position + posOffset, ref velocity, moveTime);
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