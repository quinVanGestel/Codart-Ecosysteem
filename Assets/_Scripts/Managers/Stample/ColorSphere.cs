using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class ColorSphere : MonoBehaviour
{
    public TouchCheck touchCheck; // Sleep hier het object met TouchCheck in
    [Range(0, 5)]
    public int myColorIndex;      // Kies hier 0, 1, 2, 3, 4 of 5

    void Update()
    {
        foreach (var touch in Touch.activeTouches)
        {
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.screenPosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        touchCheck.colorIndex = myColorIndex;
                        Debug.Log("Kleur gekozen: " + myColorIndex);
                    }
                }
            }
        }
    }
}
