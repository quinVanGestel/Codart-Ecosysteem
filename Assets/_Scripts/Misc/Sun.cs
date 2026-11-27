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

    private SunRayCalculations _sunRayCalculations; // Backing field, don't use this.
    private SunRayCalculations SunRayCalculations
    {
        get
        {
            _sunRayCalculations ??= GetComponent<SunRayCalculations>();
            return _sunRayCalculations;
        }
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

    // private int raysToSendThisFrame()
    // {
    //     return Mathf.FloorToInt(_raysPerSecond * Time.deltaTime);
    // }

    private void RayRoutineEntry()
    {
        //     int raysToSend = raysToSendThisFrame();
        //     while (raysToSend != 0)
        //     {
        //         SendRay();
        //         raysToSend--;
        //     }



        if (_timeSinceLastRay <= 1f / _raysPerSecond)
        {
            _timeSinceLastRay += Time.deltaTime;
            return;
        }

        _timeSinceLastRay = RayRoutineRecursive(_timeSinceLastRay);
    }

    private float RayRoutineRecursive(float timeSinceLastRay)
    {
        if (timeSinceLastRay <= 1f / _raysPerSecond)
        {
            return timeSinceLastRay;
        }

        SendRay();
        timeSinceLastRay -= 1f / _raysPerSecond;
        return RayRoutineRecursive(timeSinceLastRay);
    }

    private void SendRay()
    {
        Vector3 direction = SunRayCalculations.getNormalisedDirection();
        Vector3 origin = SunRayCalculations._origin;
        Ray ray = new(origin, direction);
        Debug.DrawRay(origin, direction);
        Physics.Raycast(ray, out RaycastHit hitInfo, MaxDistance);

        ProcessRaycastHit(hitInfo);

        Debug.Log("Ray sent");
        Debug.Log("Ray has been sent from " + origin + " to direction " + direction);
    }

    private void ProcessRaycastHit(RaycastHit hitInfo)
    {
        Interfaces.ISunRayable sunRayableObject = hitInfo.collider.gameObject.GetComponent<Interfaces.ISunRayable>();
        if (sunRayableObject == null)
        {
            return;
        }

        sunRayableObject.ReceiveEnergy(_energyPerRay);
    }


}
