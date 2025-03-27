using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    GameVariables gameVariables;
    TimerManager timerManager;
    int difficulty;

    internal void NewGameStart()
    {
        GameVariables.GetCurrentGameVariables().SetDefaultSettings();
        UIDisplay.GetInstance().AllCountersUpdate();
        timerManager
    }

    public void ChangeAmountOfFood(int food)
    {
        gameVariables.currentAmountOfFood += food;
        UIDisplay.GetInstance().FoodUpdate(gameVariables.currentAmountOfFood);
    }

    public void ChangeAmountOfWarriors(int warriors)
    {
        gameVariables.currentAmountOfWarriors += warriors;
        UIDisplay.GetInstance().WarriorsUpdate(gameVariables.currentAmountOfWarriors);
    }

    public void ChangeAmountOfRaiders(int raiders)
    {
        currentAmountOfRaiders += raiders;
    }

}
