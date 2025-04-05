using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    UIDisplay _UIDisplay;
    TimerManager _timerManager;
    int difficulty = 1;  // ����������� ���������


    // ������� ���������� �������� � ������
    [SerializeField] internal int currentAmountOfFood { get; set; }
    [SerializeField] internal int currentAmountOfWarriors { get; set; }
    [SerializeField] internal int currentAmountOfCitizens { get; set; }
    [SerializeField] internal int currentAmountOfRaiders { get; set; }

    internal int currentRaid { get; private set; } = 0;

    // ��������� �������� �������� � ������
    private int defaultAmountOfFood { get; } = 5;
    private int defaultAmountOfCitizens { get; } = 2;
    private int defaultAmountOfWarriors { get; } = 1;
    private int defaultAmountOfRaiders { get; } = 1;

    // ��������� ��������� ����� ������
    private int defaultCitizenCost { get; } = 1;
    private int defaultWarriorCost { get; } = 2;

    //���������� ���������� �� ���
    private int defaultAmountNewCitizens { get; } = 1;
    private int defaultAmountNewWarriors { get; } = 1;


    // ��������� ����� � ��������� ������� 
    private int defaultFoodForCitizen { get; } = 2;
    private int defaultFeedingOfWarrior { get; } = 1;

    // ��������� �������� ��������
    private int defaultFoodTimer { get; } = 15;
    private int defaultFeedingTimer { get; } = 30;
    private int defaultNewWarriorTimer { get; } = 15;
    private int defaultNewCitizenTimer { get; } = 30;
    private int defaultRaidTimer { get; } = 60;

    private void Start()
    {
        //�������� �������� ����������������� � ������������������ (���������)
        _timerManager = TimerManager.GetTimers();
        _UIDisplay = UIDisplay.GetInstance();
        currentRaid = 0;

    }

    void Update()
    {
        if (_timerManager.gameIsOn)
        {

            // �� ��������� ������� ���������� ���
            if (_timerManager.foodTimer.timerEnded)
            {
                Debug.Log("food amount = " + currentAmountOfFood + "(+" + (currentAmountOfCitizens * defaultFoodForCitizen) + ")");
                ChangeAmountOfFood(currentAmountOfCitizens * defaultFoodForCitizen);
            }

            // �� ��������� ������� ��������� �����
            if (_timerManager.feedingTimer.timerEnded)
            {
                Debug.Log("After feeding food amount = " + currentAmountOfFood + "(-" + (currentAmountOfWarriors * defaultFeedingOfWarrior) + ")");
                ChangeAmountOfFood(-currentAmountOfWarriors * defaultFeedingOfWarrior);
            }

            // �� ��������� ������� ���� � �������������� ������ �����
            if (_timerManager.raidTimer.timerEnded)
            {
                Debug.Log("Raid � "+ currentRaid + " After raid warriors amount = " + currentAmountOfWarriors + "(-" + currentAmountOfRaiders + ")");
                ChangeAmountOfWarriors(-currentAmountOfRaiders);
                currentRaid++;
                ChangeAmountOfRaiders(Mathf.RoundToInt(Mathf.Sqrt(_timerManager.currentDay() * currentRaid)));

            }

            // �� ��������� ������� �������� ����� �������
            if (_timerManager.newCitizenTimer.timerEnded)
            {
                ChangeAmountOfCitizen(defaultAmountNewCitizens);
                UIDisplay.GetInstance().ChangeCitizenCreationInterface();
                UIDisplay.GetInstance().CitizensUpdate(currentAmountOfCitizens);
                Debug.Log("New Amount of Cittizen = " + currentAmountOfCitizens + " (+" + defaultAmountNewCitizens + ")"); 
                
            }

            // �� ��������� ������� �������� ����� ������
            if (_timerManager.newWarriorTimer.timerEnded)
            {
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

    // ������ ����� ����, ����������� ��������� ��������
    public void NewGameStart()
    {
        //��������� ��������� � ����������� �� ��������� 
        if (difficulty == 1)
        {
            SetDefaultSettings();
        }

        _timerManager.NewGameStart(defaultFoodTimer, defaultFeedingTimer, defaultRaidTimer, defaultNewCitizenTimer, defaultNewWarriorTimer);


        //����������� ���� ��������� � �������� � ����������
        _UIDisplay.AllCountersUpdate(currentAmountOfFood, currentAmountOfCitizens, currentAmountOfWarriors, currentAmountOfRaiders);
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
            {
                ChangeAmountOfFood(-defaultCitizenCost);
                UIDisplay.GetInstance().ChangeCitizenCreationInterface();
            }
        }
        else
        {
            UIDisplay.GetInstance().SendWarningMessage("�� ���������� ���");
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
            UIDisplay.GetInstance().SendWarningMessage("�� ���������� ���");
        }
    }


}
