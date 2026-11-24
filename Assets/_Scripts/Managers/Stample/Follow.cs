using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Follow : MonoBehaviour
{
    void OnEnable() => EnhancedTouchSupport.Enable();

    void Update()
    {
        if (Touch.activeTouches.Count == 0) return;

        // Van pixels op het scherm naar een plek in je scène
        Vector3 _world = Camera.main.ScreenToWorldPoint(Touch.activeTouches[0].screenPosition);
        _world.z = 0;
        transform.position = _world;
    }
}