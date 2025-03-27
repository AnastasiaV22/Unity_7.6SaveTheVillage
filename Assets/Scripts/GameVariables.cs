
public class GameVariables
{
  
    private GameVariables() 
    {
        SetDefaultSettings();
    }
    private static GameVariables currentGameVariables;
    public static GameVariables GetCurrentGameVariables()
    {
        if (currentGameVariables == null)
            currentGameVariables = new GameVariables();

        return currentGameVariables;
    }


    //private int currentGameDifficulty = 2;

    // текущее количество ресурсов и юнитов
    internal int currentAmountOfFood { get; set; }
    internal int currentAmountOfWarriors { get; set; }
    internal int currentAmountOfCitizens { get; set; }
    internal int currentAmountOfRaiders { get; set; }


    // Начальные значения ресурсов и юнитов
    private int defaultAmountOfFood { get; } = 5;
    private int defaultAmountOfCitizens { get; } = 2;
    private int defaultAmountOfWarriors { get; } = 1;
    private int defaultAmountOfRaiders { get; } = 1;

    // Начальная стоимость найма юнитов
    private int defaultCitizenCost { get; } = 1;
    private int defaultWarriorCost { get; } = 2;

    // Начальные значения таймеров
    private int defaultFoodTimer { get; } = 60;
    private int defaultNewWarriorTimer { get; } = 60;
    private int defaultNewCitizenTimer { get; } = 60;
    private int defaultRaidTimer { get; } = 120;

    // Установка начальных значений
    internal void SetDefaultSettings()
    {
        currentAmountOfFood = defaultAmountOfFood;
        currentAmountOfWarriors = defaultAmountOfWarriors;
        currentAmountOfRaiders = defaultAmountOfRaiders;
    }



}
