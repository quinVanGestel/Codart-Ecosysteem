
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchCubeBrush : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject CubePrefab;

    public float _spawnHeight = 24f;
    public float SpawnDelay = 0.5f;

    private float _nextSpawnTime;

    void Update()
    {
        foreach (var touch in Touch.activeTouches)
        {
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began ||
                touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                if (Time.time >= _nextSpawnTime)
                {
                    SpawnCube(touch.screenPosition);
                    _nextSpawnTime = Time.time + SpawnDelay;
                }
            }
        }
    }


    void SpawnCube(Vector2 screenPosition)
    {
        Ray ray = MainCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 spawnPosition = new Vector3(
                hit.point.x,
                transform.position.y + _spawnHeight,
                hit.point.z
            );

            Instantiate(CubePrefab, spawnPosition, Quaternion.identity);

            Debug.Log("Cube spawned at Y: " + spawnPosition.y);
        }
    }
}
