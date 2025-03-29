using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    UIDisplay _UIDisplay;
    TimerManager _timerManager;
    int difficulty = 1;  // Стандартная сложность


    // текущее количество ресурсов и юнитов
    [SerializeField] internal int currentAmountOfFood { get; set; }
    [SerializeField] internal int currentAmountOfWarriors { get; set; }
    [SerializeField] internal int currentAmountOfCitizens { get; set; }
    [SerializeField] internal int currentAmountOfRaiders { get; set; }

    internal int currentRaid { get; private set; } = 0;

    // Начальные значения ресурсов и юнитов
    private int defaultAmountOfFood { get; } = 5;
    private int defaultAmountOfCitizens { get; } = 2;
    private int defaultAmountOfWarriors { get; } = 1;
    private int defaultAmountOfRaiders { get; } = 1;

    // Начальная стоимость найма юнитов
    private int defaultCitizenCost { get; } = 1;
    private int defaultWarriorCost { get; } = 2;

    //количество нанимаемых за раз
    private int defaultAmountNewCitizens { get; } = 1;
    private int defaultAmountNewWarriors { get; } = 1;


    // Начальные траты и получение пшеницы 
    private int defaultFoodForCitizen { get; } = 2;
    private int defaultFeedingOfWarrior { get; } = 1;

    // Начальные значения таймеров
    private int defaultFoodTimer { get; } = 60;
    private int defaultFeedingTimer { get; } = 60;
    private int defaultNewWarriorTimer { get; } = 60;
    private int defaultNewCitizenTimer { get; } = 60;
    private int defaultRaidTimer { get; } = 120;

    private void Start()
    {
        //Создание объектов менеджератаймеров и отображениедисплея (синглтоны)
        _timerManager = TimerManager.GetTimers();
        _UIDisplay = UIDisplay.GetInstance();
        currentRaid = 0;

    }

    void Update()
    {
        if (_timerManager.gameIsOn)
        {
            // По окончанию таймера добавление еды
            if (_timerManager.foodTimer.timerEnded)
            {
                ChangeAmountOfFood(currentAmountOfCitizens * defaultFoodForCitizen);
            }

            // По окончанию таймера кормление армии
            if (_timerManager.feedingTimer.timerEnded)
            {
                ChangeAmountOfFood(currentAmountOfWarriors * defaultFeedingOfWarrior);
            }

            // По окончанию таймера рейд и характеристики нового рейда
            if (_timerManager.raidTimer.timerEnded)
            {
                currentAmountOfWarriors -= currentAmountOfRaiders;
                currentRaid++;
                ChangeAmountOfRaiders(Mathf.RoundToInt(Mathf.Sqrt(_timerManager.currentDay() * currentRaid)));
            }

            // По окончанию таймера создание новых жителей
            if (_timerManager.newCitizenTimer.timerEnded)
            {
                ChangeAmountOfCitizen(defaultAmountNewCitizens);
            }

            // По окончанию таймера создание новых войнов
            if (_timerManager.newCitizenTimer.timerEnded)
            {
                ChangeAmountOfWarriors(defaultAmountNewWarriors);
            }
        }
    }

    // Начало новой игры, возвращение начальных значений
    public void NewGameStart()
    {
        //Начальные настройки в зависимости от сложности 
        if (difficulty == 1)
        {
            SetDefaultSettings();
        }

        _timerManager.NewGameStart(defaultFoodTimer, defaultFeedingTimer, defaultRaidTimer, defaultNewCitizenTimer, defaultNewWarriorTimer);


        //Отображение всех счетчиков и таймеров в интерфейсе
        _UIDisplay.AllCountersUpdate(currentAmountOfFood, currentAmountOfWarriors, currentAmountOfCitizens, currentAmountOfRaiders);
        _UIDisplay.AllTimersToFullUpdate();
    }

    internal void SetDefaultSettings()
    {
        currentAmountOfFood = defaultAmountOfFood;
        currentAmountOfWarriors = defaultAmountOfWarriors;
        currentAmountOfCitizens = defaultAmountOfCitizens;
        currentAmountOfRaiders = defaultAmountOfRaiders;
    }

    internal void ChangeAmountOfFood(int food)
    {
        currentAmountOfFood += food;
        UIDisplay.GetInstance().FoodUpdate(currentAmountOfFood);
    }
    internal void ChangeAmountOfCitizen(int citizens)
    {
        currentAmountOfCitizens += citizens;
        UIDisplay.GetInstance().WarriorsUpdate(currentAmountOfWarriors);
    }
    internal void ChangeAmountOfWarriors(int warriors)
    {
        currentAmountOfWarriors += warriors;
        UIDisplay.GetInstance().WarriorsUpdate(currentAmountOfWarriors);
    }

    internal void ChangeAmountOfRaiders(int raiders)
    {
        currentAmountOfRaiders += raiders;
        UIDisplay.GetInstance().RaidUpdate(currentAmountOfRaiders);
    }


    public void CreateNewCitizen()
    {
        if (currentAmountOfFood >= defaultCitizenCost)
        {
            if (_timerManager.CreateNewCitizen())
                currentAmountOfFood -= defaultCitizenCost;
        }
    }
    public void CreateNewWarrior()
    {
        if (currentAmountOfFood >= defaultWarriorCost)
        {
            if (_timerManager.CreateNewWarrior())
                currentAmountOfFood -= defaultWarriorCost;
        }
    }


}
