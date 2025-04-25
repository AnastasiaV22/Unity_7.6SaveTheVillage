using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Settings
{
    private Settings() { }
    private static Settings instance;

    public static Settings GetInstance()
    {
        if (instance == null)
            instance = new Settings();

        return instance;
    }

    // Сложность 
    internal int currentDifficulty { get; private set; } = 1;

    // Громкость музыки

    internal int volumeMusic = 50;

    // Условия победы

        //активные условия победы
    internal bool winWithAmountOfFoodOn { get; private set; } = true;
    internal bool winWithAmountOfCitizenOn { get; private set; } = false;
    internal bool winWithRaidSurvivedOn { get; private set; } = true;
        //необходимо по параметру для победы
    internal int foodAmountToWin { get; private set; } = 50;
    internal int citizenAmountToWin { get; private set; } = 15;
    internal int raidsSurvivedToWin { get; private set; } = 5;


    // Начальные значения ресурсов и юнитов
    internal int defaultAmountOfFood { get; private set; } = 5;
    internal int defaultAmountOfCitizens { get; private set; } = 2;
    internal int defaultAmountOfWarriors { get; private set; } = 2;
    internal int defaultAmountOfRaiders { get; private set; } = 1;

    // Начальная стоимость найма юнитов
    internal int defaultCitizenCost { get; private set; } = 1;
    internal int defaultWarriorCost { get; private set; } = 3;

    //количество нанимаемых за раз
    internal int defaultAmountNewCitizens { get; private set; } = 1;
    internal int defaultAmountNewWarriors { get; private set; } = 2;


    // Начальные траты и получение пшеницы 
    internal int defaultFoodFromCitizen { get; private set; } = 2;
    internal int defaultFeedingOfWarrior { get; private set; } = 1;

    // Начальные значения таймеров
    internal int defaultFoodTimer { get; private set; } = 10;
    internal int defaultFeedingTimer { get; private set; } = 15;
    internal int defaultNewWarriorTimer { get; private set; } = 5;
    internal int defaultNewCitizenTimer { get; private set; } = 5;
    internal int defaultRaidTimer { get; private set; } = 30;



    internal void SetWinSetings(bool winWithFood, bool winWithUnits, bool winWithRaids)
    {
        winWithAmountOfFoodOn = winWithFood;
        winWithAmountOfCitizenOn = winWithUnits;
        winWithRaidSurvivedOn = winWithRaids;
        Debug.Log("Win settings: food = " + winWithAmountOfFoodOn + " Units = " + winWithAmountOfCitizenOn + " RaidSurvived = " + winWithRaidSurvivedOn);

    }


    public void SetDifficultySettings(int difficulty)
    {

        if (difficulty == 2)
        {

        }
        if (difficulty == 3)
        {

        }
        else
        {
            foodAmountToWin = 50;
            citizenAmountToWin = 15;
            raidsSurvivedToWin = 10;
        }
    }

}
