using UnityEngine;


public class TimerManager : MonoBehaviour
{
    // Класс управляющий таймерами (запуск, проверка окончания работы и тд)

    public float gameTime { get; private set; } = 0f;
    public bool gameIsOn { get; private set; } = false;

    private float SecondsInDay = 100f; 

    private Timer foodTimer;
    private Timer raidTimer;
    private Timer newWarriorTimer;
    private Timer newCitizenTimer;

    private float defaultFoodTimer = 60f;
    private float defaultRaidTimer = 120f;
    private float defaultNewWarriorTimer = 60f;
    private float defaultNewCitizenTimer = 60f;

    void Start()
    {

    }

    void Update()
    {
        if (gameIsOn)
        {
            gameTime += Time.deltaTime;

            if (!raidTimer.timerIsOn && currentDay() > 2)
            {
             //   GameActions.NewRaidCreate();
                NextRaid();
            }

            if (!foodTimer.timerIsOn)
            {
            //    GameActions.
            }


        }


    }

    public void NewGameStart()
    {
        gameTime = 0f;
        gameIsOn = true;

        foodTimer = new Timer(defaultFoodTimer);
        foodTimer.StartTimer();

        raidTimer = new Timer(defaultRaidTimer);

        newCitizenTimer = new Timer(defaultNewCitizenTimer);
        newWarriorTimer = new Timer(defaultNewWarriorTimer);
    }

    public void GameEnd()
    {
        gameIsOn = false;

        // удалить обьекты таймеров
    }

    public void CreateNewCitizen()
    {
        if (newCitizenTimer.timerIsOn)
            newCitizenTimer.StartTimer();
    }

    public void CreateNewWarrior()
    {
        if (newWarriorTimer.timerIsOn)
            newWarriorTimer.StartTimer();
    }

    public void NextRaid()
    {
        raidTimer.SetDefaultTime(defaultRaidTimer * Random.Range(1,5)/currentDay());
        raidTimer.StartTimer(); 
    }
    
    public int currentDay()
    {
        return (int)(gameTime/SecondsInDay);
    }
    public void PauseGame()
    {
        gameIsOn = false;
        foodTimer.TimerOff();
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
        raidTimer.TimerOn();

        // Если таймер не работал до остановки,то оставшееся время в процентах -1
        if (newWarriorTimer.CurrentTimeProcent() != -1)
            newWarriorTimer.TimerOn();
        if (newCitizenTimer.CurrentTimeProcent() != -1)
            newCitizenTimer.TimerOn();
    }


}
