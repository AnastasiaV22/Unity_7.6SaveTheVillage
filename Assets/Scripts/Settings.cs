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



    // ������� ������

    //�������� ������� ������
    internal bool winWithAmountOfFoodOn { get; private set; } = true;
    internal bool winWithAmountOfUnitsOn { get; private set; } = false;
    internal bool winWithRaidSurvivedOn { get; private set; } = false;
        //���������� �� ��������� ��� ������
    internal int foodAmountToWin { get; private set; } = 50;
    internal int unitsAmountToWin { get; private set; } = 15;
    internal int raidsSurvivedToWin { get; private set; } = 10;



    void SetWinSetings(int settings) //  1 ������ �� ���, 2 �� ���������� ������, 3 �� ���������� ����
    {
        winWithAmountOfFoodOn = Convert.ToBoolean(settings / 100);
        winWithAmountOfUnitsOn = Convert.ToBoolean(settings / 10 % 10);
        winWithRaidSurvivedOn = Convert.ToBoolean(settings % 10);
        Debug.Log("Win settings: food = " + winWithAmountOfFoodOn + " Units = " + winWithAmountOfUnitsOn + " RaidSurvived = " + winWithRaidSurvivedOn);

    }


    public void SetDifficultySettings (int difficulty){

        if (difficulty == 2)
        {

        }
        if (difficulty == 3)
        {

        }
        else
        {
            
        }
    }


}
