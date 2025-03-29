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
    private int defaultFoodTimer { get; } = 60;
    private int defaultFeedingTimer { get; } = 60;
    private int defaultNewWarriorTimer { get; } = 60;
    private int defaultNewCitizenTimer { get; } = 60;
    private int defaultRaidTimer { get; } = 120;

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
                ChangeAmountOfFood(currentAmountOfCitizens * defaultFoodForCitizen);
            }

            // �� ��������� ������� ��������� �����
            if (_timerManager.feedingTimer.timerEnded)
            {
                ChangeAmountOfFood(currentAmountOfWarriors * defaultFeedingOfWarrior);
            }

            // �� ��������� ������� ���� � �������������� ������ �����
            if (_timerManager.raidTimer.timerEnded)
            {
                currentAmountOfWarriors -= currentAmountOfRaiders;
                currentRaid++;
                ChangeAmountOfRaiders(Mathf.RoundToInt(Mathf.Sqrt(_timerManager.currentDay() * currentRaid)));
            }

            // �� ��������� ������� �������� ����� �������
            if (_timerManager.newCitizenTimer.timerEnded)
            {
                ChangeAmountOfCitizen(defaultAmountNewCitizens);
            }

            // �� ��������� ������� �������� ����� ������
            if (_timerManager.newCitizenTimer.timerEnded)
            {
                ChangeAmountOfWarriors(defaultAmountNewWarriors);
            }
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
