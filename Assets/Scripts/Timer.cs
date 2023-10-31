using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimerDirection { CountUp, CountDown }

public class Timer : GameBehaviour
{
    public TimerDirection timerDirection;
    public float startTime = 0;
    float currentTime;
    bool isTiming = false;
    float timeLimit = 0;
    bool hasTimeLimit = false;

    private void Start()
    {
        StartTimer(0, timerDirection);
    }
    void Update()
    {
        if (!isTiming)
            return;

        //if the timerDirection == TimerDirection.CountUp, increment the current time, else decrement the current time
        currentTime = timerDirection == TimerDirection.CountUp ? currentTime += Time.deltaTime : currentTime -= Time.deltaTime;

        if (currentTime < 0) { currentTime = 0; StopTimer(); }

        _UI.UpdateTime(currentTime);
    }

    /// <summary>
    /// Starts the timer
    /// </summary>
    /// <param name="_startTime">The start time of the timer. Defaults to 0</param>
    /// <param name="_direction">The start direction of the timer. Defaults to count up</param>
    public void StartTimer(float _startTime = 0, TimerDirection _direction = TimerDirection.CountUp)
    {
        timerDirection = _direction;
        startTime = _startTime;
        isTiming = true;
    }

    /// <summary>
    /// Starts the timer
    /// </summary>
    /// <param name="_startTime">What time to start at</param>
    /// <param name="_timeLimit">What time limit</param>
    /// <param name="_hasTimeLimit">Use a time limit</param>
    /// <param name="_direction">The start direction of the timer. Defaults to count up</param>
    public void StartTimer(float _startTime = 0, float _timeLimit = 0, bool _hasTimeLimit = true, TimerDirection _direction = TimerDirection.CountUp)
    {
        hasTimeLimit = _hasTimeLimit;
        startTime = _startTime;
        timeLimit = _timeLimit;
        isTiming = true;
    }

    /// <summary>
    /// Resumes the timer
    /// </summary>
    public void ResumeTimer()
    {
        isTiming = true;
    }

    /// <summary>
    /// Pauses the timer
    /// </summary>
    public void PauseTimer()
    {
        isTiming = false;
    }

    /// <summary>
    /// Stops the timer
    /// </summary>
    public void StopTimer()
    {
        isTiming = false;
    }

    /// <summary>
    /// Increment our timer
    /// </summary>
    /// <param name="_increment">The amount to increment our timer</param>
    public void IncrementTimer(float _increment)
    {
        currentTime += _increment;
    }

    /// <summary>
    /// Decrement our timer
    /// </summary>
    /// <param name="_decrement">The amount to decrement our timer</param>
    public void DecrementTimer(float _decrement)
    {
        currentTime -= _decrement;
    }

    /// <summary>
    /// Checks if time has expired
    /// </summary>
    /// <returns>If time has expired</returns>
    public bool TimeExpired()
    {
        if (!hasTimeLimit)
            return false;

        return timerDirection == TimerDirection.CountUp ? currentTime > timeLimit : currentTime < timeLimit;
    }

    /// <summary>
    /// Gets the current time
    /// </summary>
    /// <returns>The current time</returns>
    public float GetTime()
    {
        return currentTime;
    }

    /// <summary>
    /// Checks if we are timing or not
    /// </summary>
    /// <returns>If we are timing</returns>
    public bool IsTiming()
    {
        return isTiming;
    }

    /// <summary>
    /// Changes the direction of the timer
    /// </summary>
    /// <param name="_direction">The direction to change to</param>
    public void ChangeTimerDirection(TimerDirection _direction)
    {
        timerDirection = _direction;
    }
}