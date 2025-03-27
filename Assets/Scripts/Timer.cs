﻿using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Класс таймера

    internal bool timerIsOn { get; private set; } = false;
    private float currentTime = 0f;
    private float defaultTime = 60f;

    public Timer(float _newDefaultTime)
    {
        SetDefaultTime(_newDefaultTime);
    }

    void Update()
    {
        if (timerIsOn)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0f)
            {
                TimerOff();
            }

        }
    }

    public void SetDefaultTime(float newDefaultTime)
    {
        if (CurrentTimeSeconds() != -1) // Если таймер работает сейчас, то оставшееся время изменяяется так, что бы процентно время осталось то же
            currentTime = newDefaultTime*CurrentTimeProcent()/100;
        defaultTime = newDefaultTime;
    }


    public void StartTimer()
    {
        currentTime = defaultTime;
        timerIsOn = true;
    }

    public void TimerOn()
    {
        timerIsOn = true;
    }

    public void TimerOff()
    {
        timerIsOn = false;
    }

    //Оставшееся время таймера в секундах, если времени не осталось возвращает -1 
    public int CurrentTimeSeconds()
    {
        if (currentTime > 0)
            return Convert.ToInt32(MathF.Round(currentTime));
        else
            return -1;
    }

    //Оставшееся время таймера в процентах, При 0 < currentTime <= 0,5 возвращает 0, если времени не осталось возвращает -1
    public int CurrentTimeProcent()
    {
        if (currentTime > 0)
            return Convert.ToInt32(MathF.Round(currentTime / defaultTime * 100));
        else
            return -1;
    }
}
