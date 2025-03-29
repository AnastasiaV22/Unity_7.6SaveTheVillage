using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
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

    private UIDisplay() { }
    private static UIDisplay instance;

    public static UIDisplay GetInstance()
    {
        if (instance == null)
        {
            instance = new UIDisplay();
        }
        return instance;
    }

    void Start()
    {
        if (instance == null)
            instance = new UIDisplay();
    }


    internal void AllCountersUpdate(int amountOfFood, int amountOfCitizens, int amountOfWarriors, int amountOfRaiders)
    {
        FoodUpdate(amountOfFood);
        CitizensUpdate(amountOfCitizens);
        WarriorsUpdate(amountOfWarriors);
        RaidUpdate(amountOfRaiders);

    }

    internal void AllTimersToFullUpdate()
    {
        RaidTimerUpdate(100);
        NewFoodTimerUpdate(100);
        FeedingTimerUpdate(100);
        NewCitizenTimerUpdate(100);
        NewWarriorTimerUpdate(100);
    }

    //Отображение текущего количества
    internal void FoodUpdate(int newAmount) { foodCounter.text = newAmount.ToString();}
    internal void CitizensUpdate(int newAmount) { citizenCounter.text = newAmount.ToString();}
    internal void WarriorsUpdate(int newAmount) { warriorsCounter.text = newAmount.ToString();}
    internal void RaidUpdate(int newAmount) {raidersCounter.text = newAmount.ToString();}


    // Отображение время в процентах
    internal void RaidTimerUpdate(int time) { raidTimer.fillAmount = time / 100;}
    internal void NewFoodTimerUpdate(int time) { newFoodTimer.fillAmount = time / 100;} 
    internal void FeedingTimerUpdate(int time) { feedingTimer.fillAmount = time / 100;}
    
    internal void NewCitizenTimerUpdate(int time) { newCitizenTimer.fillAmount = time / 100;}
    internal void NewWarriorTimerUpdate(int time) { newWarriorTimer.fillAmount = time / 100;}

}
