
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Follow : MonoBehaviour
{
    public bool MouseAsTouch = true;
    public Camera _mainCamera;

    private float _fixedHeight = 160f;

    private MeshRenderer _meshRenderer;

    private float _lastTapTime;
    public float DoubleTapTime = 0.3f;

    private Vector3 _offset;
    private bool _recalibrated = false;

    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;
    }

    void OnEnable()
    {
        _mainCamera = Camera.main;
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

    void FixedUpdate()
    {
        if (Touch.activeTouches.Count == 0)
        {
            _meshRenderer.enabled = false;
            return;
        }

        var touch = Touch.activeTouches[0];

        // Double tap detecteren
        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
        {
            if (Time.time - _lastTapTime <= DoubleTapTime)
            {
                Recalibrate(touch.screenPosition);
            }

            _lastTapTime = Time.time;
        }

        Ray ray = _mainCamera.ScreenPointToRay(touch.screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            _meshRenderer.enabled = true;

            Vector3 targetPosition = new Vector3(
                hit.point.x,
                _fixedHeight,
                hit.point.z
            );

            if (_recalibrated)
                targetPosition += _offset;

            transform.position = targetPosition;
        }
        else
        {
            _meshRenderer.enabled = false;
        }
    }

    void Recalibrate(Vector2 touchPosition)
    {
        Ray ray = _mainCamera.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = new Vector3(
                hit.point.x,
                _fixedHeight,
                hit.point.z
            );

            _offset = transform.position - targetPosition;
            _recalibrated = true;

            Debug.Log("Cursor recalibrated!");
        }
    }
}
