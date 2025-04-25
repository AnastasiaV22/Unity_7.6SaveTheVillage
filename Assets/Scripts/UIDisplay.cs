using Unity.VisualScripting;
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

    [SerializeField] Text warningMessage;
    [SerializeField] Text SummeryText;

    //Настройки 
    [SerializeField] Toggle winFoodToggle;
    [SerializeField] Toggle winUnitsToggle;
    [SerializeField] Toggle winRaidsToggle;

    //Игра
    [SerializeField] Toggle foodAchivmentToggle;
    [SerializeField] Toggle citizenAchivmentToggle;
    [SerializeField] Toggle raidsAchivmentToggle;


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

    private void Update()
    {
        if (warningMessage.color.a > 0)
            warningMessage.color = new Color(warningMessage.color.r, warningMessage.color.g, warningMessage.color.b, warningMessage.color.a - 0.01f);
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

        citizenCreationPanel.SetActive(false);
        warriorCreationPanel.SetActive(false);
        newCitizenCreationButton.gameObject.SetActive(true);
        newWarriorCreationButton.gameObject.SetActive(true);
    }

    internal void AllTimersUpdate(int newFoodTime, int feedingTime, int raidTime, int newCitizenTime, int newWarriorTime)
    {
        NewFoodTimerUpdate(newFoodTime);
        FeedingTimerUpdate(feedingTime);
        RaidTimerUpdate(raidTime);
        NewCitizenTimerUpdate(newCitizenTime);
        NewWarriorTimerUpdate(newWarriorTime);
    }

    //Начальное отображение целей игры
    internal void AllAchivmentsToDefault()
    {
        Debug.Log("AcheventsDefaultUpdate");
        float x = foodAchivmentToggle.gameObject.transform.localPosition.x;
        float z = foodAchivmentToggle.gameObject.transform.localPosition.z;
        foodAchivmentToggle.gameObject.transform.localPosition = new Vector3(x, 25f, z);
        citizenAchivmentToggle.gameObject.transform.localPosition = new Vector3(x, 25f, z);
        raidsAchivmentToggle.gameObject.transform.localPosition = new Vector3(x, 25f, z);


        float y = 0f;

        ChangeWinSettings();

        if (Settings.GetInstance().winWithAmountOfFoodOn)
        {
            Debug.Log("AcheventsFoodTrue");
            foodAchivmentToggle.gameObject.SetActive(true);
            foodAchivmentToggle.interactable = false;
            foodAchivmentToggle.isOn = false;

            foodAchivmentToggle.gameObject.transform.localPosition += new Vector3(0, y, 0);
            ShowAchivmentWinCondition(foodAchivmentToggle.GetComponentInChildren<Text>(), Settings.GetInstance().defaultAmountOfFood, Settings.GetInstance().foodAmountToWin);
            y -= 50f;
        }
        else
        {

            Debug.Log("AcheventsFoodFalse");
            foodAchivmentToggle.gameObject.SetActive(false);
        }

        if (Settings.GetInstance().winWithAmountOfCitizenOn)
        {

            Debug.Log("AcheventsUnitsTrue");
            citizenAchivmentToggle.gameObject.SetActive(true);
            citizenAchivmentToggle.interactable = false;
            citizenAchivmentToggle.isOn = false;
            citizenAchivmentToggle.gameObject.transform.localPosition += new Vector3(0, y, 0);
            ShowAchivmentWinCondition(citizenAchivmentToggle.GetComponentInChildren<Text>(), Settings.GetInstance().defaultAmountOfCitizens, Settings.GetInstance().citizenAmountToWin);
            y -= 50f;
        }
        else
        {
            Debug.Log("AcheventsUnitsFalse");
            citizenAchivmentToggle.gameObject.SetActive(false);
        }

        if (Settings.GetInstance().winWithRaidSurvivedOn)
        {
            Debug.Log("AcheventsRaidsTrue");
            raidsAchivmentToggle.gameObject.SetActive(true);
            raidsAchivmentToggle.interactable = false;
            raidsAchivmentToggle.isOn = false;
            raidsAchivmentToggle.gameObject.transform.localPosition += new Vector3(0, y, 0);
            ShowAchivmentWinCondition(raidsAchivmentToggle.GetComponentInChildren<Text>(), 0, Settings.GetInstance().raidsSurvivedToWin);
            y -= 50f  ;
        }
        else
        {
            Debug.Log("AcheventsRaidsFalse");
            raidsAchivmentToggle.gameObject.SetActive(false);
        }
    }

    //Отображение текущего количества
    internal void FoodUpdate(int newAmount) { if (newAmount < 0) newAmount = 0; foodCounter.text = newAmount.ToString();
        if (foodAchivmentToggle.gameObject.activeSelf)
            ShowAchivmentWinCondition(foodAchivmentToggle.GetComponentInChildren<Text>(), newAmount, Settings.GetInstance().foodAmountToWin);
    }
    internal void CitizensUpdate(int newAmount) { citizenCounter.text = newAmount.ToString();
        if (citizenAchivmentToggle.gameObject.activeSelf)
            ShowAchivmentWinCondition(citizenAchivmentToggle.GetComponentInChildren<Text>(), newAmount, Settings.GetInstance().citizenAmountToWin);
    
    }
    internal void WarriorsUpdate(int newAmount) { if (newAmount < 0) newAmount = 0; warriorsCounter.text = newAmount.ToString();}
    internal void RaidUpdate(int newAmount) {raidersCounter.text = newAmount.ToString();}

    internal void CurrentRaidNumberUpdate(int currentRaid)
    {
        if (raidsAchivmentToggle.gameObject.activeSelf)
            ShowAchivmentWinCondition(raidsAchivmentToggle.GetComponentInChildren<Text>(), currentRaid, Settings.GetInstance().raidsSurvivedToWin);

    }

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
            Debug.Log("Status Citizen Creation Changed (End)");
        }
        else
        {
            citizenCreationPanel.SetActive(true);
            newCitizenCreationButton.gameObject.SetActive(false);
            Debug.Log("Status Citizen Creation Changed (Start)");
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
        warningMessage.text = message;

        Color newColor = new Color(188, 0, 0, 1);
        warningMessage.color = newColor;
    }

    internal void ShowGameSummery(string summery)
    {
        SummeryText.text = summery;
    }

    public void ChangeWinSettings()
    {
        Settings.GetInstance().SetWinSetings(winFoodToggle.isOn, winUnitsToggle.isOn, winRaidsToggle.isOn);
            
    }


    private void ShowAchivmentWinCondition(Text fieldToUpdate, int newValue, int neededValue)
    {
        fieldToUpdate.text = $"{newValue}/{neededValue}";
        
        // Отображение выполняется или нет победное условие
        if (newValue >= neededValue)
            fieldToUpdate.GetComponentInParent<Toggle>().isOn = true;
        else
            fieldToUpdate.GetComponentInParent<Toggle>().isOn = false;
    }


}
