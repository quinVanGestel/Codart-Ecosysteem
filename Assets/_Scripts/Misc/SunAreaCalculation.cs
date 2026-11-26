using UnityEngine;

public class SunRayCalculations : MonoBehaviour
{
    [SerializeField] private Transform _bottomLeftAnchor;
    [SerializeField] private Transform _topRightAnchor;
    private Vector3 _lowerBounds
    {
        get
        {
            return _bottomLeftAnchor.position;
        }
    }

    private Vector3 _upperBounds
    {
        get
        {
            return _topRightAnchor.position;
        }
    }

    private Vector3 _origin
    {
        get
        {
            return transform.position;
        }
    }

    public Vector3 getNormalisedDirection()
    {
        return (_origin - getRandomPosition(_lowerBounds, _upperBounds)).normalized;
    }

    private Vector3 getRandomPosition(Vector3 lowerBound, Vector3 upperBound)
    {
        return new(
            Random.Range(lowerBound.x, upperBound.x),
            Random.Range(lowerBound.y, upperBound.y),
            Random.Range(lowerBound.z, upperBound.z)
        );
    }

}

