using System.Runtime.CompilerServices;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    //backing field
    private static TimeManager _instance;
    public float TimeSpeed { get; }

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
    
    
    
    
}
