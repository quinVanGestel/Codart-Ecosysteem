
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchCheck : MonoBehaviour
{
    public bool MouseAsTouch = true;

    public Camera _mainCamera;
    public GameObject Sphere;

    private float _scaleAmount = 50f;

    public Material[] Ballcolor;
    public int colorIndex = 0;

    void Start()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;
    }

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();

        // Alleen muis simuleren in de Unity Editor
#if UNITY_EDITOR
        if (MouseAsTouch)
            TouchSimulation.Enable();
#endif
    }

    void OnDisable()
    {
#if UNITY_EDITOR
        if (MouseAsTouch)
            TouchSimulation.Disable();
#endif

        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        foreach (var touch in Touch.activeTouches)
        {
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began ||
                touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                CheckTouch(touch.screenPosition, touch.finger.index);
            }
        }
    }

    void CheckTouch(Vector2 position, int fingerIndex)
    {
        if (_mainCamera == null || Sphere == null)
            return;

        Ray ray = _mainCamera.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Controleer ook child-colliders
            if (hit.transform == transform ||
                hit.transform.IsChildOf(transform))
            {
                if (Ballcolor == null || Ballcolor.Length == 0)
                    return;

                colorIndex = Mathf.Clamp(
                    fingerIndex, 0, Ballcolor.Length - 1
                );

                GameObject newSphere = Instantiate(
                    Sphere,
                    hit.point + Vector3.up * 136.8f,
                    Quaternion.identity
                );

                MeshRenderer renderer =
                    newSphere.GetComponentInChildren<MeshRenderer>();

                if (renderer != null)
                    renderer.material = Ballcolor[colorIndex];

                newSphere.transform.localScale =
                    Vector3.one * _scaleAmount;
            }
        }
    }
}
