using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SunRayCalculations))]
public class Sun : MonoBehaviour
{
    [SerializeField] private float _raysPerSecond;
    [SerializeField] private float _energyPerSecond;
    private float _energyPerRay
    {
        get
        {
            if (_energyPerRay == 0)
            {
                return 0f;
            }

            return _energyPerSecond / _energyPerRay;
        }
    }

    private float _timeSinceLastRay;

    public float MaxDistance;
    public bool DebugMode;
    private int _sentRayAmount;

    private SunRayCalculations _sunRayCalculations
    {
        get
        {
            return _sunRayCalculations ??= GetComponent<SunRayCalculations>();
        }

        set { _sunRayCalculations = value; }
    }

    private void Start()
    {
        // Debug.Log("generalDirection " + generalDirection);
        // Debug.Log("transform.rotation.eulerAngles "+transform.rotation.eulerAngles);
    }

    private void Update()
    {
        RayRoutineEntry();
    }

    private void RayRoutineEntry()
    {
        if (_timeSinceLastRay <= 1f / _raysPerSecond)
        {
            _timeSinceLastRay += Time.deltaTime;
            return;
        }

        _timeSinceLastRay = RayRoutineRecursive(_timeSinceLastRay);
    }

    private async float RayRoutineRecursive(float timeSinceLastRay)
    {
        if (timeSinceLastRay <= 1f / _raysPerSecond)
        {
            return timeSinceLastRay;
        }

        SendRay();
        timeSinceLastRay -= 1f / _raysPerSecond;
        return RayRoutineRecursive(timeSinceLastRay);
    }

    private async void SendRay()
    {
        Vector3 direction = generalDirection; // generalDirection while testing
        Ray ray = new(start, direction);
        Debug.DrawRay(start, direction);
        Physics.Raycast(ray, out RaycastHit hitInfo, MaxDistance);
        RaycastCommand.ScheduleBatch

        Debug.Log("Ray sent");
        // Debug.Log("Ray has been sent from " + start + " in direction " + direction);
    }

}
