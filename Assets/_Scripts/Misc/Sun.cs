using Unity.VisualScripting;
using UnityEngine;

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

    private float timeSinceLastRay;

    public float MaxDistance;
    public bool DebugMode;

    private Vector3 start
    {
        get
        {
            return transform.position;
        }
    }

    private Vector3 generalDirection
    {
        get
        {
            return (transform.forward);
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

    private void RayRoutineEntry()
    {
        if (timeSinceLastRay <= 1f / _raysPerSecond)
        {
            timeSinceLastRay += Time.deltaTime;
            return;
        }

        RayRoutineRecursive();

    }

    private async void RayRoutineRecursive()
    {
        if (timeSinceLastRay <= 1f / _raysPerSecond)
        {
            return;
        }

        SendRay();
        timeSinceLastRay -= 1f / _raysPerSecond;
        RayRoutineRecursive();
    }


    private void SendRay()
    {
        Vector3 direction = generalDirection; // generalDirection while testing
        Ray ray = new(start, direction);
        Debug.DrawRay(start, direction);
        Physics.Raycast(ray, out RaycastHit hitInfo, MaxDistance);

        Debug.Log("Ray has been sent from " + start + " in direction " + direction);
    }

}
