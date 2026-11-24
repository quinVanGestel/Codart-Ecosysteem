using UnityEngine;

public class PlantTest : Plant
{
    [RangeAttribute(0, 100)]
    [SerializeField] private float _efficiency;


}
