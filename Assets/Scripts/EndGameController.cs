using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndGameController : MonoBehaviour
{

    UIDisplay _UIDisplay;
    Settings _Settings;
    GameManager _GameManager;

    private EndGameController() { }
    private static EndGameController instance;

    public static EndGameController GetInstance()
    {
        if (instance == null)
            instance = new EndGameController();
        return instance;
    }

    // Выполняется ли условие победы
    bool winWithAmountOfFood;
    bool winWithAmountOfCitizen;
    bool winWithRaidSurvived;
    
    // Проиграна ли игра
    bool isLost = true;


    private void Start()
    {
        instance = this;
        _UIDisplay = UIDisplay.GetInstance();
        _Settings = Settings.GetInstance();
        _GameManager = GameManager.GetInstance();
    }

    void Update()
    {
        if (!isLost)
        {
            CheckEndGameConditions();

            if (isLost)
            {
                OnGameEnd();
                _GameManager.EndGame(true);
                UIMoving.GetInstance().ShowLoseEndGame();
            }
            else if (winWithAmountOfFood & winWithAmountOfCitizen & winWithRaidSurvived)
            {
                OnGameEnd();
                _GameManager.EndGame(false);
                UIMoving.GetInstance().ShowWinEndGame();
            }
        }   

    }

    internal void OnGameStart() 
    {
        isLost = false;

        winWithAmountOfFood = !_Settings.winWithAmountOfFoodOn;
        winWithAmountOfCitizen = !_Settings.winWithAmountOfCitizenOn;
        winWithRaidSurvived = !_Settings.winWithRaidSurvivedOn;

    }

    private void OnGameEnd()
    {
        isLost = true;
    }
    

    void CheckEndGameConditions()
    {
        if (_Settings.foodAmountToWin <= _GameManager.currentAmountOfFood) winWithAmountOfFood = true;
        else if (_Settings.winWithAmountOfFoodOn) winWithAmountOfFood = false;

        if (_Settings.citizenAmountToWin <= _GameManager.currentAmountOfCitizens) winWithAmountOfCitizen = true;
        else if (_Settings.winWithAmountOfCitizenOn) winWithAmountOfCitizen = false;
        
        if (_Settings.raidsSurvivedToWin <= _GameManager.currentRaid) winWithRaidSurvived = true;
        else if (_Settings.winWithRaidSurvivedOn) winWithRaidSurvived = false;

        if (_GameManager.currentAmountOfFood < 0 || _GameManager.currentAmountOfWarriors < 0)
            isLost = true;
    }


}
