
using UnityEngine;
using UnityEngine.Assemblies;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchCheck : MonoBehaviour
{
    public bool MouseAsTouch = true;

    public Camera _mainCamera;
    public GameObject Sphere;

    private float _scaleAmount = 150f;
    public Material[] Ballcolor;
    public int colorIndex = 0;

    void Start()
    {
        Sphere.GetComponent<MeshRenderer>().material = Ballcolor[colorIndex];
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
        foreach (var touch in Touch.activeTouches)
        {
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                CheckTouch(touch.screenPosition);
            }
        }
    }

    void CheckTouch(Vector2 position)
    {
        Ray ray = _mainCamera.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                colorIndex = Random.Range(0, Ballcolor.Length);
                GameObject newSphere = Instantiate(Sphere, hit.point + hit.normal * 0.5f, Quaternion.identity);

                newSphere.transform.localScale = Vector3.one * _scaleAmount;
            }
        }
    }
}
