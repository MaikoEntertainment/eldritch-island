using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsMaster : MonoBehaviour
{
    private static StatisticsMaster _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }


    public static StatisticsMaster GetInstance() { return _instance; }

}
