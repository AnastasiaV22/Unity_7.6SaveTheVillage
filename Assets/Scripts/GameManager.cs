using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    UIDisplay _UIDisplay;
    TimerManager _timerManager;
    Settings _settings;
    int difficulty = 1;  // Стандартная сложность

    private GameManager() {}
    private static GameManager instance;

    public static GameManager GetInstance()
    {

        return instance;
    }

    private void Awake()
    {

        instance = this;

    }

    bool gameIsEnded = true;

    // текущее количество ресурсов и юнитов
    [SerializeField] internal int currentAmountOfFood { get; private set; }
    [SerializeField] internal int currentAmountOfWarriors { get; private set; }
    [SerializeField] internal int currentAmountOfCitizens { get; private set; }
    [SerializeField] internal int currentAmountOfRaiders { get; private set; }

    internal int currentRaid { get; private set; } = 0;

    // Начальная стоимость найма юнитов
    private int defaultCitizenCost;
    private int defaultWarriorCost;

    //количество нанимаемых за раз
    private int defaultAmountNewCitizens;
    private int defaultAmountNewWarriors;

    // Начальные траты и получение пшеницы 
    private int defaultFoodFromCitizen;
    private int defaultFeedingOfWarrior;

    // Начальные значения
    private int defaultFoodTimer;
    private int defaultFeedingTimer;
    private int defaultNewWarriorTimer;
    private int defaultNewCitizenTimer;
    private int defaultRaidTimer;

    // Статистика
    private int avarageFoodAmount;
    private int avarageWarriorsAmount;
    private int avarageCitizenAmount;
    private int avarageRaidsAmount;

    private void Start()
    {

        //Создание объектов менеджератаймеров и отображениедисплея (синглтоны)
        _timerManager = TimerManager.GetTimers();
        _UIDisplay = UIDisplay.GetInstance();
        _settings = Settings.GetInstance();
    }

    void Update()
    {
        if (!gameIsEnded)
        {
            if (UIMoving.GetInstance().IsPauseButtonClick() && _timerManager.gameIsOn)
            {
                _timerManager.PauseGame();
            }
            else if (!UIMoving.GetInstance().IsPauseButtonClick() && !_timerManager.gameIsOn) {
                _timerManager.ContinueGame();
            }
        }

        if (_timerManager.gameIsOn)
        {

            // По окончанию таймера добавление еды
            if (_timerManager.foodTimer.timerEnded)
            {
                avarageFoodAmount += currentAmountOfCitizens * defaultFoodFromCitizen;
                Debug.Log("food amount = " + currentAmountOfFood + "(+" + (currentAmountOfCitizens * defaultFoodFromCitizen) + ")");
                ChangeAmountOfFood(currentAmountOfCitizens * defaultFoodFromCitizen);
            }

            // По окончанию таймера кормление армии
            if (_timerManager.feedingTimer.timerEnded)
            {
                Debug.Log("After feeding food amount = " + currentAmountOfFood + "(-" + (currentAmountOfWarriors * defaultFeedingOfWarrior) + ")");
                ChangeAmountOfFood(-currentAmountOfWarriors * defaultFeedingOfWarrior);
            }

            // По окончанию таймера рейд и характеристики нового рейда
            if (_timerManager.raidTimer.timerEnded)
            {

                Debug.Log("Raid № "+ (currentRaid+1) + " After raid warriors amount = " + currentAmountOfWarriors + "(-" + currentAmountOfRaiders + ")");
                ChangeAmountOfWarriors(-currentAmountOfRaiders);
                currentRaid++;
                ChangeAmountOfRaiders(Mathf.RoundToInt(Mathf.Sqrt(_timerManager.currentDay() * currentRaid)));
                if (currentAmountOfWarriors >= 0)
                    avarageRaidsAmount++;
                _UIDisplay.CurrentRaidNumberUpdate(currentRaid);
            }

            // По окончанию таймера создание новых жителей
            if (_timerManager.newCitizenTimer.timerEnded)
            {
                avarageCitizenAmount += defaultAmountNewCitizens;
                ChangeAmountOfCitizen(defaultAmountNewCitizens);
                UIDisplay.GetInstance().ChangeCitizenCreationInterface();
                UIDisplay.GetInstance().CitizensUpdate(currentAmountOfCitizens);
                Debug.Log("New Amount of Cittizen = " + currentAmountOfCitizens + " (+" + defaultAmountNewCitizens + ")"); 
                

            }

            // По окончанию таймера создание новых войнов
            if (_timerManager.newWarriorTimer.timerEnded)
            {
                avarageWarriorsAmount += defaultAmountNewWarriors;
                ChangeAmountOfWarriors(defaultAmountNewWarriors);
                UIDisplay.GetInstance().ChangeWarriorCreationInterface();
                UIDisplay.GetInstance().WarriorsUpdate(currentAmountOfWarriors);
                Debug.Log("New Amount of Warriors = " + currentAmountOfWarriors + " (+" + defaultAmountNewWarriors + ")");
            }
            _timerManager.CheckRestartTimers();


            _UIDisplay.AllTimersUpdate(_timerManager.foodTimer.CurrentTimeProcent(), _timerManager.feedingTimer.CurrentTimeProcent(), _timerManager.raidTimer.CurrentTimeProcent(), _timerManager.newCitizenTimer.CurrentTimeProcent(), _timerManager.newWarriorTimer.CurrentTimeProcent());
            _timerManager.UpdateGameTimer(Time.deltaTime);
        }
    }

    // Начало новой игры, возвращение начальных значений
    public void NewGameStart()
    {
        
        gameIsEnded = false;
        currentRaid = 0;
        //Начальные настройки в зависимости от сложности 
        if (difficulty == 1)
        {
            SetDefaultSettings();

            avarageRaidsAmount = 0;
            avarageFoodAmount = 0;
            avarageWarriorsAmount = 0;
            avarageCitizenAmount = 0;
        }
        _timerManager.NewGameStart(defaultFoodTimer, defaultFeedingTimer, defaultRaidTimer, defaultNewCitizenTimer, defaultNewWarriorTimer);
        EndGameController.GetInstance().OnGameStart();

        //Отображение всех счетчиков и таймеров в интерфейсе
        _UIDisplay.AllCountersUpdate(currentAmountOfFood, currentAmountOfCitizens, currentAmountOfWarriors, currentAmountOfRaiders);
        _UIDisplay.AllTimersToFullUpdate();
        _UIDisplay.AllAchivmentsToDefault();
    }

    internal void EndGame(bool isLost)
    {
        _timerManager.GameEnd();
        gameIsEnded = true;
        string summary = "";

        if (currentAmountOfWarriors < 0)
        {
            summary = "Деревня разграблена :( \n";
            currentAmountOfWarriors = 0;
        }
        else if (currentAmountOfFood < 0)
        {
            summary = "В деревне начался голод ... \n";
            currentAmountOfFood = 0;
        }

        

        summary += $" --- ИТОГИ --- \n" +            
            $"Время игры: { Mathf.Round(_timerManager.gameTime/60)} мин {Mathf.Round(_timerManager.gameTime) % 60} сек \n" +
            $"Пшеницы собрано: {avarageFoodAmount} \n " +
            $"Жителей в деревне: {avarageCitizenAmount} \n" +
            $"Войнов нанято: {avarageWarriorsAmount} \n" +
            $"Рейдов пережито: {avarageRaidsAmount} \n" +
            $"\n" +
            $"{"Условия победы: "}";

        if (Settings.GetInstance().winWithAmountOfFoodOn)
        {
            if (Settings.GetInstance().foodAmountToWin <= currentAmountOfFood)
                summary += "\n + ";
            else
                summary += "\n - ";
            summary += $"пшеницы запасено: {currentAmountOfFood}/{Settings.GetInstance().foodAmountToWin}";
        }
            
        if (Settings.GetInstance().winWithAmountOfCitizenOn)
        {
            if (Settings.GetInstance().citizenAmountToWin <= currentAmountOfCitizens + currentAmountOfWarriors)
                summary += "\n + ";
            else
                summary += "\n - ";
            summary += $"людей в поселении: { currentAmountOfCitizens}/{ Settings.GetInstance().citizenAmountToWin}";
        }
        
        if (Settings.GetInstance().winWithRaidSurvivedOn)
        {
            if (Settings.GetInstance().raidsSurvivedToWin <= currentRaid)
                summary += "\n + ";
            else
                summary += "\n - ";
            summary += $"рейдов пережито: {currentRaid}/{Settings.GetInstance().raidsSurvivedToWin}";

        }

        if (isLost)
            UIMoving.GetInstance().ShowLoseEndGame();
        else
            UIMoving.GetInstance().ShowWinEndGame();
        _UIDisplay.ShowGameSummery(summary);

    }
    internal void SetDefaultSettings()
    {
            // Начальные значения ресурсов и юнитов
        currentAmountOfFood = _settings.defaultAmountOfFood;
        currentAmountOfWarriors = _settings.defaultAmountOfWarriors;
        currentAmountOfCitizens = _settings.defaultAmountOfCitizens;
        currentAmountOfRaiders = _settings.defaultAmountOfRaiders;


        // Начальная стоимость найма юнитов
        defaultCitizenCost = _settings.defaultCitizenCost;
        defaultWarriorCost = _settings.defaultWarriorCost;

        //количество нанимаемых за раз
        defaultAmountNewCitizens = _settings.defaultAmountNewCitizens;
        defaultAmountNewWarriors = _settings.defaultAmountNewWarriors;


        // Начальные траты и получение пшеницы 
        defaultFoodFromCitizen = _settings.defaultFoodFromCitizen;
        defaultFeedingOfWarrior = _settings.defaultFeedingOfWarrior;

        // Начальные значения таймеров
        defaultFoodTimer = _settings.defaultFoodTimer;
        defaultFeedingTimer = _settings.defaultFeedingTimer;
        defaultNewWarriorTimer = _settings.defaultNewWarriorTimer;
        defaultNewCitizenTimer = _settings.defaultNewCitizenTimer;
        defaultRaidTimer = _settings.defaultRaidTimer;

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
            {
                ChangeAmountOfFood(-defaultCitizenCost);
                UIDisplay.GetInstance().ChangeCitizenCreationInterface();
            }
        }
        else
        {
            UIDisplay.GetInstance().SendWarningMessage("Не достаточно еды");
        }

    }

    public void CreateNewWarrior()
    {
        if (currentAmountOfFood >= defaultWarriorCost)
        {
            if (_timerManager.CreateNewWarrior()) { 
                ChangeAmountOfFood(-defaultWarriorCost);
                UIDisplay.GetInstance().ChangeWarriorCreationInterface();
            }
        }
        else
        {
            UIDisplay.GetInstance().SendWarningMessage("Не достаточно еды");
        }
    }




}
