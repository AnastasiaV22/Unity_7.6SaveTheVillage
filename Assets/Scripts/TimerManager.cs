using UnityEngine;


public class TimerManager: MonoBehaviour
{
    // Класс управляющий таймерами (запуск, проверка окончания работы и тд)

    public float gameTime { get; private set; } = 0f; // Общее время прошедшее от начала игры
    public bool gameIsOn { get; private set; } = false;

    private float SecondsInDay = 100f; 

    internal Timer foodTimer;
    internal Timer raidTimer;
    internal Timer feedingTimer;
    internal Timer newWarriorTimer;
    internal Timer newCitizenTimer;

    private TimerManager() {}
    private static TimerManager GameTimers;

    public static TimerManager GetTimers()
    {
        if (GameTimers == null)
            GameTimers = new TimerManager();
        return GameTimers;
    }


    void Update()
    {
        if (gameIsOn)
        {
            Debug.Log(gameTime);

            gameTime += Time.deltaTime;


            if (foodTimer.timerEnded && !foodTimer.timerIsOn)
            {
                foodTimer.StartTimer();
            }

            if (feedingTimer.timerEnded && !feedingTimer.timerIsOn)
            {
                feedingTimer.StartTimer();
            }

            // Первый рейд случается через 3 дня 
            if (!raidTimer.timerIsOn && currentDay() > 3 )
            {
                NextRaid();
            }

        }

    }

    public void NewGameStart(float _defaultFoodTimer, float _defaultFeedingTimer, float _defaultRaidTimer, float _defaultNewCitizenTimer, float _defaultNewWarriorTimer)
    {
        gameTime = 0f;
        gameIsOn = true;

        foodTimer = new Timer(_defaultFoodTimer);
        foodTimer.StartTimer();

        feedingTimer = new Timer(_defaultFeedingTimer);
        feedingTimer.StartTimer();

        raidTimer = new Timer(_defaultRaidTimer); 

        newCitizenTimer = new Timer(_defaultNewCitizenTimer);
        newWarriorTimer = new Timer(_defaultNewWarriorTimer);
    }

    public void GameEnd()
    {
        gameIsOn = false;

        // удалить обьекты таймеров


    }

    // возвращает true если началось создание нового жителя
    public bool CreateNewCitizen()
    {
        if (!newCitizenTimer.timerIsOn)
        {
            newCitizenTimer.StartTimer();
            return true;
        }

        return false;
    }

    // возвращает true если началось создание нового война
    public bool CreateNewWarrior()
    {
        if (!newWarriorTimer.timerIsOn) { 
            newWarriorTimer.StartTimer();
            return true;
        }

        return false;
    }

    public void NextRaid()
    {
        if (!raidTimer.timerIsOn)
        {
            raidTimer.SetDefaultTime(raidTimer.defaultTime * Random.Range(2,5)/currentDay()); //Следующий рейд  
            raidTimer.StartTimer();
        }
    }
    
    public int currentDay()
    {
        return (int)(gameTime/SecondsInDay);
    }

    public void PauseGame()
    {
        gameIsOn = false;
        foodTimer.TimerOff();
        feedingTimer.TimerOff();
        raidTimer.TimerOff();


        if (newWarriorTimer.timerIsOn)
            newWarriorTimer.TimerOff();
        if (newCitizenTimer.timerIsOn)
            newCitizenTimer.TimerOff();
    }


    public void ContinueGame()
    {
        gameIsOn = true;
        foodTimer.TimerOn();
        feedingTimer.TimerOn();
        raidTimer.TimerOn();

        // Если таймер не работал до остановки,то оставшееся время в процентах -1
        if (newWarriorTimer.CurrentTimeProcent() != -1)
            newWarriorTimer.TimerOn();
        if (newCitizenTimer.CurrentTimeProcent() != -1)
            newCitizenTimer.TimerOn();
    }


}
