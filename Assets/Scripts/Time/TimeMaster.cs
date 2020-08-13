using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMaster : MonoBehaviour
{
    private static TimeMaster _instance;

    private float timeMultiplier = 1;
    // How frequently the game checks for changes not related to events
    private float ticInterval = 0.5f;

    private Dictionary<TimeMultiplierLevelIds, float> timeLevels = new Dictionary<TimeMultiplierLevelIds, float>();

    // Events
    public delegate void TimeMultiplier(float timeMultiplier);
    public event TimeMultiplier OnTimeMultiplier;
    public delegate void TimePass(float timePassed);
    public event TimePass OnTimePassed;

    private float timePassed = 0;
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeMultipliers();
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > GetTicInterval())
        {
            OnTimePassed?.Invoke(timePassed * GetTimeMultiplier());
            timePassed = 0;
        }
    }

    public void InitializeMultipliers()
    {
        timeLevels.Add(TimeMultiplierLevelIds.normal, 1);
        timeLevels.Add(TimeMultiplierLevelIds.turbo, 24);
    }

    public static TimeMaster GetInstance() { return _instance; }

    public float GetTimeMultiplier() { return timeMultiplier; }
    public float GetTicInterval() { return ticInterval; }

    public void UpdateTimeMultiplier(TimeMultiplierLevelIds id) {
        if (timeLevels.ContainsKey(id))
            timeMultiplier = timeLevels[id];
        else
            timeMultiplier = 1;
        // Notifies susbscribers about new time mult
        OnTimeMultiplier?.Invoke(timeMultiplier);
    }
}
