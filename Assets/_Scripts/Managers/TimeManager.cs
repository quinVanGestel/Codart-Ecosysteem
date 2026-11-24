using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    //backing field
    private static TimeManager _instance;
    public float TimeScale { get; }                                                                                                 
    public float GoFaster;
    public float amount = 3f;

    public static TimeManager Instance
    {
        
        //when you try fetching the data in prop then return the backing field
        get => _instance;
        set
        {
            //make sure there's nothing in the prop fill it (make sure there's none already in there)
            if (TimeManager.Instance != null)
            {
                return;
            }
            
            _instance = value;
        }
    }

    void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        
    }

    public void Fasterplsss()
    {
       // if ()
       // {
            GoFaster = TimeScale * amount;
      //  }
    }
}
