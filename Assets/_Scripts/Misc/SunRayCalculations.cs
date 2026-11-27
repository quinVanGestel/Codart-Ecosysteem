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

    public Vector3 _origin
    {
        get
        {
            return transform.position;
        }
    }

    public Vector3 getNormalisedDirection()
    {
        Vector3 rawResult = getRandomPosition(_lowerBounds, _upperBounds) - _origin;
        Vector3 normalisedResult = rawResult.normalized;
        // Debug.Log("getNormalisedDirection called. Origin: " + _origin + " rawResult: " + rawResult + " normalisedResult: " + normalisedResult);
        return normalisedResult;
    }

    private Vector3 getRandomPosition(Vector3 lowerBound, Vector3 upperBound)
    {
        Vector3 result = new(
            Random.Range(lowerBound.x, upperBound.x),
            Random.Range(lowerBound.y, upperBound.y),
            Random.Range(lowerBound.z, upperBound.z)
        );
        // Debug.Log("getRandomPosition test. lowerBound: " + lowerBound + " upperBound:" + upperBound + " result: " + result);

        return result;
    }

}

