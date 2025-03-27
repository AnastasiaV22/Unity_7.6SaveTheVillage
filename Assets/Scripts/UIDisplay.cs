using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{

    private UIDisplay() { }
    private static UIDisplay instance;

    public static UIDisplay GetInstance()
    {
        return instance;
    }

    public void Start()
    {
        instance = new UIDisplay();
    }

    // Класс для изменения отображаемой информации на экране


    [SerializeField] Text foodCounter;
    [SerializeField] Text citizenCounter;
    [SerializeField] Text warriorsCounter;
    [SerializeField] Text raidersCounter;

    [SerializeField] Image newFoodTimer;
    [SerializeField] Image feedingTimer;

    [SerializeField] Image raidTimer;
    [SerializeField] Image newCitizenTimer;
    [SerializeField] Image newWarriorTimer;

    internal void AllCountersUpdate()
    {
        FoodUpdate(GameVariables.GetCurrentGameVariables().currentAmountOfFood);
        CitizensUpdate(GameVariables.GetCurrentGameVariables().currentAmountOfCitizens);
        WarriorsUpdate(GameVariables.GetCurrentGameVariables().currentAmountOfWarriors);
        RaidUpdate(GameVariables.GetCurrentGameVariables().currentAmountOfRaiders);



    }
    internal void FoodUpdate(int newAmount)
    {
        foodCounter.text = newAmount.ToString();
    }

    internal void CitizensUpdate(int newAmount)
    {
        citizenCounter.text = newAmount.ToString();
    }

    internal void WarriorsUpdate(int newAmount)
    {
        warriorsCounter.text = newAmount.ToString();
    }

    internal void RaidUpdate(int newAmount)
    {
        raidersCounter.text = newAmount.ToString();
    }

    internal void RaidTimerUpdate(int time)
    {
        raidTimer.fillAmount = time / 100;
    }

    internal void NewCitizenTimerUpdate(int time)
    {
        newCitizenTimer.fillAmount = time / 100;
    }

    internal void NewWarriorTimerUpdate(int time)
    {
        newWarriorTimer.fillAmount = time / 100;
    }

    internal void NewFoodTimerUpdate(int time)
    {
        newFoodTimer.fillAmount = time / 100;
    }

    internal void FeedingTimerUpdate(int time)
    {
        feedingTimer.fillAmount = time / 100;
    }

}
