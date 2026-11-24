using Unity.VisualScripting;
using UnityEngine;

public class Sun : MonoBehaviour
{
    // private float _energyPerSecondPerTile;
    [SerializeField] private float _raysPerSecond;
    [SerializeField] private float _energyPerSecond;
    private float _energyPerRay
    {
        get
        {


            return -1f;
        }
    }

    public bool Debug;


    private void FixedUpdate()
    {

    }

}
