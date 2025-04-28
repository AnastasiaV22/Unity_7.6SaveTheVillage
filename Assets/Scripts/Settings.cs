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

    // ��������� 
    internal int currentDifficulty { get; private set; } = 1;

    // ��������� ������

    internal float volumeMusic = 0.5f;

    // �������� ����

    internal float currentGameSpeed { get; private set; } = 1;

    // ������� ������

        //�������� ������� ������
    internal bool winWithAmountOfFoodOn { get; private set; } = false;
    internal bool winWithAmountOfCitizenOn { get; private set; } = false;
    internal bool winWithRaidSurvivedOn { get; private set; } = true;

        //���������� �� ��������� ��� ������
    internal int foodAmountToWin { get; private set; } = 100;
    internal int citizenAmountToWin { get; private set; } = 30;
    internal int raidsSurvivedToWin { get; private set; } = 10;


    // ��������� �������� �������� � ������
    internal int defaultAmountOfFood { get; private set; } = 5;
    internal int defaultAmountOfCitizens { get; private set; } = 2;
    internal int defaultAmountOfWarriors { get; private set; } = 2;
    internal int defaultAmountOfRaiders { get; private set; } = 1;

    // ��������� ��������� ����� ������
    internal int defaultCitizenCost { get; private set; } = 1;
    internal int defaultWarriorCost { get; private set; } = 3;

    //���������� ���������� �� ���
    internal int defaultAmountNewCitizens { get; private set; } = 1;
    internal int defaultAmountNewWarriors { get; private set; } = 2;


    // ��������� ����� � ��������� ������� 
    internal int defaultFoodFromCitizen { get; private set; } = 2;
    internal int defaultFeedingOfWarrior { get; private set; } = 1;

    // ��������� �������� ��������
    internal int defaultFoodTimer { get; private set; } = 10;
    internal int defaultFeedingTimer { get; private set; } = 12;
    internal int defaultNewWarriorTimer { get; private set; } = 4;
    internal int defaultNewCitizenTimer { get; private set; } = 5;
    internal int defaultRaidTimer { get; private set; } = 20;



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

    internal void SetMusicVolume( int newValue)
    {
        volumeMusic = newValue;
    }

    public void ChangeGameSpeed()
    {
        if (Time.timeScale == 3)
            Time.timeScale = 1;
        else
            Time.timeScale += 1;

        currentGameSpeed = Time.timeScale;

    }
    public void SetDefaultGameSpeed() { Time.timeScale = currentGameSpeed = 1; }
}
