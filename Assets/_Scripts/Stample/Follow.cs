using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using System.Collections.Generic;

using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Follow : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject CursorPrefab;

    public float CursorOffset = 0.02f;

    private Dictionary<int, GameObject> cursors =
        new Dictionary<int, GameObject>();

    void Update()
    {
        List<int> activeFingerIDs = new List<int>();

        foreach (var touch in Touch.activeTouches)
        {
            int fingerID = touch.finger.index;
            activeFingerIDs.Add(fingerID);

            if (!cursors.ContainsKey(fingerID))
            {
                GameObject cursor = Instantiate(CursorPrefab);
                cursors.Add(fingerID, cursor);
            }

            GameObject currentCursor = cursors[fingerID];

            // Vinger raakt het scherm aan
            currentCursor.SetActive(true);

            Ray ray = MainCamera.ScreenPointToRay(touch.screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                currentCursor.transform.position =
                    hit.point + hit.normal * CursorOffset;
            }
            else
            {
                currentCursor.SetActive(false);
            }
        }

        // Vingers die losgelaten zijn
        foreach (var cursor in cursors)
        {
            if (!activeFingerIDs.Contains(cursor.Key))
            {
                cursor.Value.SetActive(false);
            }
        }
    }
}
