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

    [SerializeField] GameObject citizenCreationPanel;
    [SerializeField] GameObject warriorCreationPanel;
    [SerializeField] Button newCitizenCreationButton;
    [SerializeField] Button newWarriorCreationButton;


    private UIDisplay() { }
    private static UIDisplay instance;

    public static UIDisplay GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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

    internal void AllTimersUpdate(int newFoodTime, int feedingTime, int raidTime, int newCitizenTime, int newWarriorTime)
    {
        NewFoodTimerUpdate(newFoodTime);
        FeedingTimerUpdate(feedingTime);
        RaidTimerUpdate(raidTime);
        NewCitizenTimerUpdate(newCitizenTime);
        NewWarriorTimerUpdate(newWarriorTime);
    }

    //Отображение текущего количества
    internal void FoodUpdate(int newAmount) { foodCounter.text = newAmount.ToString();}
    internal void CitizensUpdate(int newAmount) { citizenCounter.text = newAmount.ToString();}
    internal void WarriorsUpdate(int newAmount) { warriorsCounter.text = newAmount.ToString();}
    internal void RaidUpdate(int newAmount) {raidersCounter.text = newAmount.ToString();}


    // Отображение время в процентах
    internal void NewFoodTimerUpdate(int time) { newFoodTimer.fillAmount = time / 100f;} 
    internal void RaidTimerUpdate(int time) { raidTimer.fillAmount = time / 100f;}
    internal void FeedingTimerUpdate(int time) { feedingTimer.fillAmount = time / 100f;}
    
    internal void NewCitizenTimerUpdate(int time) { newCitizenTimer.fillAmount = time / 100f;}
    internal void NewWarriorTimerUpdate(int time) { newWarriorTimer.fillAmount = time / 100f;}


    internal void ChangeCitizenCreationInterface()
    {
        if (citizenCreationPanel.activeSelf)
        {
            NewCitizenTimerUpdate(100);
            citizenCreationPanel.SetActive(false);
            newCitizenCreationButton.gameObject.SetActive(true);
            Debug.Log("Status Citizen Creation Changed (Start)");
        }
        else
        {
            citizenCreationPanel.SetActive(true);
            newCitizenCreationButton.gameObject.SetActive(false);
            Debug.Log("Status Citizen Creation Changed (End)");
        }
    }

    internal void ChangeWarriorCreationInterface()
    {
        if (warriorCreationPanel.activeSelf)
        {
            NewWarriorTimerUpdate(100);
            warriorCreationPanel.SetActive(false);
            newWarriorCreationButton.gameObject.SetActive(true);
        }
        else
        {
            warriorCreationPanel.SetActive(true);
            newWarriorCreationButton.gameObject.SetActive(false);
        }
            Debug.Log("Status Warrior Creation Changed");
    }

    internal void SendWarningMessage(string message)
    {
        
    }

}
