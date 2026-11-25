using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Follow : MonoBehaviour
{
    public bool MouseAsTouch = true;
    public Camera _mainCamera;
    private float _fixedHeight = 160f;

    private MeshRenderer _meshRenderer;

    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false; // Begin onzichtbaar
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

    void Update()
    {
        // Verberg de cursor zodra je het scherm loslaat
        if (Touch.activeTouches.Count == 0)
        {
            _meshRenderer.enabled = false;
            return;
        }

        Vector2 touchPosition = Touch.activeTouches[0].screenPosition;
        Ray ray = _mainCamera.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            _meshRenderer.enabled = true;

            // Gebruik X en Z van de raycast, maar houd Y altijd vast op 140
            transform.position = new Vector3(hit.point.x, _fixedHeight, hit.point.z);
        }
        else
        {
            _meshRenderer.enabled = false;
        }
    }
}