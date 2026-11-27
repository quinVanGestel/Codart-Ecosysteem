
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using System.Collections.Generic;

using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchCheck : MonoBehaviour
{
    [Header("Touch")]
    public bool MouseAsTouch = true;
    private IPlayShortSound soundManager;

    [Header("Cube Spawning")]
    public Camera MainCamera;
    public GameObject CubePrefab;

    public float SpawnHeight = 24f;
    public float SpawnDistance = 0.02f;
    public float SpawnDelay = 0.5f;

    [Header("Cube Limit")]
    public int CubeCap = 50;

    private Dictionary<int, float> nextSpawnTime =
        new Dictionary<int, float>();

    private List<GameObject> spawnedCubes =
        new List<GameObject>();

    void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        if (soundManager == null)
        {
            Debug.LogWarning("SoundManager not found in the scene.");
        }
    }

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();

        if (MouseAsTouch)
            TouchSimulation.Enable();
    }

    void OnDisable()
    {
        if (MouseAsTouch)
            TouchSimulation.Disable();

        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        foreach (var touch in Touch.activeTouches)
        {
            int fingerID = touch.finger.index;

            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began ||
                touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                if (!nextSpawnTime.ContainsKey(fingerID))
                    nextSpawnTime[fingerID] = 0f;

                if (Time.time >= nextSpawnTime[fingerID])
                {
                    SpawnCube(touch.screenPosition);

                    nextSpawnTime[fingerID] =
                        Time.time + SpawnDelay;
                }
            }

            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended ||
                touch.phase == UnityEngine.InputSystem.TouchPhase.Canceled)
            {
                nextSpawnTime.Remove(fingerID);
            }
        }
    }

    void SpawnCube(Vector2 screenPosition)
    {
        if (MainCamera == null || CubePrefab == null)
            return;

        // Verwijder verwijzingen naar vernietigde cubes
        spawnedCubes.RemoveAll(cube => cube == null);

        // Controleer CubeCap
        if (spawnedCubes.Count >= CubeCap)
            return;

        Ray ray = MainCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 spawnPosition =
                new Vector3(
                    hit.point.x,
                    transform.position.y + SpawnHeight,
                    hit.point.z
                );

            // Extra afstand vanaf het raakpunt
            spawnPosition += hit.normal * SpawnDistance;

            GameObject newCube = Instantiate(CubePrefab,spawnPosition,Quaternion.identity);

            spawnedCubes.Add(newCube);

            soundManager?.PlayPop();
        }
    }

    void OnGUI()
    {
        GUI.Label(
            new Rect(10, 10, 300, 30),
            "Active Fingers: " + Touch.activeTouches.Count
        );

        GUI.Label(
            new Rect(10, 40, 300, 30),
            "Cubes: " + spawnedCubes.Count + " / " + CubeCap
        );
    }
}
