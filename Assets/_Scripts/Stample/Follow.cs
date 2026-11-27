using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using System.Collections.Generic;

using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Follow : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject CursorPrefab;

    [Header("Settings")]
    public float SpawnDistance = 0.02f;
    private int _spawnedCubes = 0;
    public int CubeCap = 50;
    public int MaxCursors = 10;

    private Dictionary<int, GameObject> cursors = new Dictionary<int, GameObject>();
    private List<int> activeFingerIDs = new List<int>();


    void Start()
    {
        for (int i = 0; i < MaxCursors; i++)
        {
            GameObject cursor = Instantiate(CursorPrefab);
            cursor.transform.position = new Vector3(0, -1000, 0);

            cursors.Add(-1 - i, cursor);
        }
    }

    void Update()
    {
        activeFingerIDs.Clear();

        foreach (var touch in Touch.activeTouches)
        {
            int fingerID = touch.finger.index;
            activeFingerIDs.Add(fingerID);

            GameObject currentCursor = GetCursor(fingerID);

            if (currentCursor == null)
                continue;

            Ray ray = MainCamera.ScreenPointToRay(touch.screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                currentCursor.transform.position =
                    hit.point + hit.normal * SpawnDistance;
            }
            else
            {
                currentCursor.transform.position =
                    new Vector3(0, -1000, 0);
            }
        }

        foreach (var cursor in cursors)
        {
            if (!activeFingerIDs.Contains(cursor.Key))
            {
                cursor.Value.transform.position =
                    new Vector3(0, -1000, 0);
            }
        }
    }

    GameObject GetCursor(int fingerID)
    {
        if (cursors.ContainsKey(fingerID))
            return cursors[fingerID];

        foreach (var cursor in cursors)
        {
            if (!activeFingerIDs.Contains(cursor.Key))
            {
                GameObject availableCursor = cursor.Value;

                cursors.Remove(cursor.Key);
                cursors.Add(fingerID, availableCursor);

                return availableCursor;
            }
        }

        return null;
    }

    public bool CanSpawnCube()
    {
        if (_spawnedCubes >= CubeCap)
            return false;

        _spawnedCubes++;
        return true;
    }

    public void RemoveCube()
    {
        _spawnedCubes = Mathf.Max(0, _spawnedCubes - 1);
    }
}