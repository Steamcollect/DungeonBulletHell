using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class OrbeCircle : MonoBehaviour
{
    // Orbe references
    public GameObject orbePrefabs;
    List<Transform> orbesReferences = new List<Transform>();

    public float rotationSpeed;
    public float orbesRadius;
    
    Transform lookDirGO;
    float angle;

    // Move
    Transform player;
    Vector2 velocity = Vector2.zero;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        transform.position = player.position;

        // Set look dir go
        lookDirGO = new GameObject("LookDirGO").transform;
        lookDirGO.SetParent(transform);
        lookDirGO.position = transform.position;

        CreateOrbe();
    }

    private void Update()
    {
        if (isPaused) return;

        // Move orbes
        float currentAngle = angle;
        for (int i = 0; i < orbesReferences.Count; i++)
        {
            orbesReferences[i].position = lookDirGO.position + lookDirGO.up * orbesRadius;

            currentAngle += 360 / orbesReferences.Count;
            lookDirGO.rotation = Quaternion.Euler(0, 0, currentAngle);
        }
        angle += rotationSpeed * Time.deltaTime;

        // Follow player
        transform.position = Vector2.SmoothDamp(transform.position, player.position, ref velocity, .12f);
        lookDirGO.position = transform.position;
    }

    public void CreateOrbe()
    {
        Transform current = Instantiate(orbePrefabs, transform).transform;
        orbesReferences.Add(current);
    }

    bool isPaused = false;
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