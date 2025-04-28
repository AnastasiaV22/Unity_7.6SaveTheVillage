using UnityEngine;


public class TimerManager
{
    // Класс управляющий таймерами (запуск, проверка окончания работы и тд)

    public float gameTime { get; private set; } = 0f; // Общее время прошедшее от начала игры
    public bool gameIsOn { get; private set; } = false;

    private float SecondsInDay = 20f; 

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


    public void CheckRestartTimers()
    {

        if (gameIsOn)
        {

            if (foodTimer.timerEnded && !foodTimer.timerIsOn)
            {
                Debug.Log("FoodTimerRestart");
                foodTimer.StartTimer();
            }

            if (feedingTimer.timerEnded && !feedingTimer.timerIsOn)
            {

                Debug.Log("FeedingTimerRestart");
                feedingTimer.StartTimer();
            }

            // Первый рейд через 3 дня 
            if (!raidTimer.timerIsOn)
            {
                if (currentDay() > 3)
                {

                    Debug.Log("NextRaidRestart");

                    NextRaid();
                }
                else
                {
                    raidTimer.SetDefaultTime(Settings.GetInstance().defaultRaidTimer*3);
                    raidTimer.StartTimer();
                }
            }

            if (newCitizenTimer.timerEnded)
            {
                newCitizenTimer.StartTimer();
                newCitizenTimer.TimerOff();
            }
            if (newWarriorTimer.timerEnded)
            {
                newWarriorTimer.StartTimer();
                newWarriorTimer.TimerOff();
            }

        }
    }

    public void UpdateGameTimer(float deltaTime)
    {
        gameTime += deltaTime;
       // Debug.Log("Current gametime = " +  gameTime);
    }

    public Timer CreateTimer(float defaultTime)
    {
        GameObject gameObject = new GameObject("TimerObject");
        Timer timer = gameObject.AddComponent<Timer>();
        timer.SetDefaultTime(defaultTime);
        return timer;
    }

    public void NewGameStart(float _defaultFoodTimer, float _defaultFeedingTimer, float _defaultRaidTimer, float _defaultNewCitizenTimer, float _defaultNewWarriorTimer)
    {
        MusicManager.GetInstance().StartGameSounds();

        gameTime = 0f;
        gameIsOn = true;

        
        foodTimer = CreateTimer(_defaultFoodTimer);
        foodTimer.StartTimer();
        

        feedingTimer = CreateTimer(_defaultFeedingTimer);
        feedingTimer.StartTimer();

       
        raidTimer = CreateTimer(_defaultRaidTimer);
        raidTimer.StartTimer();


        newCitizenTimer = CreateTimer(_defaultNewCitizenTimer);
        newWarriorTimer = CreateTimer(_defaultNewWarriorTimer);
    }

    public void GameEnd()
    {
        gameIsOn = false;


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
            
            raidTimer.SetDefaultTime(Settings.GetInstance().defaultRaidTimer); //Следующий рейд  
            raidTimer.StartTimer();
        }
    }
    
    public int currentDay()
    {
        return (int)(gameTime/SecondsInDay);
    }

    public void PauseGame()
    {
        MusicManager.GetInstance().PauseGameSounds();

        gameIsOn = false;
        Time.timeScale = 0;

    }

    public void ContinueGame()
    {
        MusicManager.GetInstance().StartGameSounds();
        gameIsOn = true;
        
        Time.timeScale = 1;
    }



}
