using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class MultiSlide : MonoBehaviour
{
    // Welke vinger heeft welk object vast?
    Dictionary<int, Transform> _heldObject = new Dictionary<int, Transform>();

    void OnEnable() => EnhancedTouchSupport.Enable();

    void Update()
    {
        foreach (var _activeTouch in Touch.activeTouches)
        {
            Vector3 _world = Camera.main.ScreenToWorldPoint(_activeTouch.screenPosition);
            _world.z = 0;
            int _finger = _activeTouch.finger.index;

            if (_activeTouch.phase == TouchPhase.Began)
            {
                // Raakt de vinger een object? Dan pakt hij het vast.
                var _touch = Physics2D.OverlapPoint(_world);
                if (_touch != null) _heldObject[_finger] = _touch.transform;
            }
            else if (_activeTouch.phase == TouchPhase.Ended || _activeTouch.phase == TouchPhase.Canceled)
            {
                _heldObject.Remove(_finger);
            }
            else if (_heldObject.TryGetValue(_finger, out var objects))
            {
                objects.position = _world;
            }
        }
    }
}