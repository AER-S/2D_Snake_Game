using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelAdministrator : MonoBehaviour
{
    [SerializeField] private Level[] level = new Level[10];
    private List<FoodDistribution> levelFoodDistributions = new List<FoodDistribution>();
    private List<PowerUpDistribution> levelPowerUpDistributions = new List<PowerUpDistribution>();
    private List<ObstaclesDistribution> levelObstaclesDistributions = new List<ObstaclesDistribution>();
    private BaseObjective[] levelObjectives = new BaseObjective[3];

    private int levelIndex;
    private float foodSpawningTime;
    private float foodSpawingTimeRange;
    private float powerUpSpawingTime;
    private float powerUpSpawingTimeRange;
    private float levelTime;
    private bool levelOnGoing;
    private SnakeController snake;

    private InputMaster controls;

    private Vector2 grid = new Vector2(30, 14);
    private float foodTimeCounter;
    private float powerUpTimeCounter;
    private float levelTimeCounter;
    private List<Vector3> availablePositions = new List<Vector3>();
    
    public static event Action Pause = delegate {  };
    
    public static event Action Continue = delegate {  };
    
    public static event Action Finish = delegate {  };
    
    public static event Action Starting = delegate {  };
    
    
    private LevelAdministrator instance;
    public LevelAdministrator Instance => instance;
    //public LevelAdministrator instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Snake.Pause.performed += ct => PauseGame();
        Starting += InitializeLevel;
        Finish += FinishLevel;
        
    }

    private void OnDisable()
    {
        if (controls!=null)
        {
            controls.Snake.Pause.performed -= ct => PauseGame();
        }
        Starting += InitializeLevel;
        Finish -= FinishLevel;
    }

    private void Start()
    {
        levelIndex = PlayerPrefs.GetInt("levelToLoad", 0);
        InitializeLevel();
    }
    

    private void Update()
    {
        if (levelOnGoing)
        {
            if (foodTimeCounter>0)
            {
                RunTimer(ref foodTimeCounter);
            }
            else
            {
                HandleFoodDistribution();
            }

            if (powerUpTimeCounter>0)
            {
                RunTimer(ref powerUpTimeCounter);
            }
            else
            {
                HandlePowerUpsDistribution();
            }

            if (levelTimeCounter>0)
            {
                RunTimer(ref levelTimeCounter);
            }
            else
            {
                Finish.Invoke();
            }
        }
    }

    public void InitializeLevel()
    {
        Level theLevel = this.level[levelIndex];
        CopyList(levelFoodDistributions,theLevel.GetFoodDistributions());
        CopyList(levelPowerUpDistributions,theLevel.GetPowerUpDistributions());
        CopyList(levelObstaclesDistributions, theLevel.GetObstaclesDistributions());
        levelTime = theLevel.GetTime();
        foodSpawningTime = theLevel.GetFoodSpawningTime();
        foodSpawingTimeRange = theLevel.GetFoodSpawningTimeRange();
        ResetTimer(ref foodTimeCounter,foodSpawningTime,foodSpawingTimeRange);
        powerUpSpawingTime = theLevel.GetPowerUpSpawningTime();
        powerUpSpawingTimeRange = theLevel.GetPowerUpSpawningTimeRange();
        ResetTimer(ref powerUpTimeCounter, powerUpSpawingTime,foodSpawningTime);
        ResetTimer(ref levelTimeCounter, levelTime,0.1f);
        ResetAvailablePositions();
        snake = SnakeController.Instance;
        levelObjectives = level[levelIndex].GetObjectives();

        levelOnGoing = true;
    }

    void ResetAvailablePositions()
    {
        int withHalf = (int)grid.x / 2;
        int heightHalf = (int) grid.y / 2;
        availablePositions.Clear();
        for (int i = -withHalf; i <= withHalf; i++)
        {
            for (int j =-heightHalf ; j <= heightHalf; j++)
            {
                availablePositions.Add(new Vector3(i,j,0));
            }
            
        }
    }

    void FinishLevel()
    {
        snake.StopSnake();
    }

    void HandleFoodDistribution()
    {
        if (levelFoodDistributions.Count!=0)
        {
            int foodDistributionIndex =(snake.GetLength()<=4)?ChooseMassGainer():Random.Range(0, levelFoodDistributions.Count);
            FoodDistribution foodDistribution = levelFoodDistributions[foodDistributionIndex];
            SpawnItem(foodDistribution.GetFood());
            levelFoodDistributions[foodDistributionIndex].DecreaseQuantity();
            if (foodDistribution.GetQuantity()<=0)
            {
                levelFoodDistributions.Remove(foodDistribution);
            }
        }
        ResetTimer(ref foodTimeCounter,foodSpawningTime, foodSpawingTimeRange);
    }
    
    

    void HandlePowerUpsDistribution()
    {
        if (levelPowerUpDistributions.Count!=0)
        {
            int powerUpsDistributionIndex =Random.Range(0, levelPowerUpDistributions.Count);
            PowerUpDistribution powerUpDistribution = levelPowerUpDistributions[powerUpsDistributionIndex];
            SpawnItem(powerUpDistribution.GetPowerUp());
            levelPowerUpDistributions[powerUpsDistributionIndex].DecreaseQuantity();
            if (powerUpDistribution.GetQuantity()<= 0)
            {
                levelPowerUpDistributions.Remove(powerUpDistribution);
            }
        }
        ResetTimer(ref powerUpTimeCounter, powerUpSpawingTime, powerUpSpawingTimeRange);
    }
    void SpawnItem<T>(T _food) where T : MonoBehaviour
    {
        ResetAvailablePositions();
        RemoveSnakePositions();
        Vector3 spawnPosition = ChooseARandomAvailablePosition();
        Instantiate(_food, spawnPosition, Quaternion.identity);
    }

    int ChooseMassGainer()
    {
        List<FoodDistribution> gainers = new List<FoodDistribution>();
        foreach (FoodDistribution foodDistribution in levelFoodDistributions)
        {
            BaseFood food = foodDistribution.GetFood();
            if (food.GetFoodType()==FoodType.MassGainer)
            {
                gainers.Add(foodDistribution);
            }
        }

        FoodDistribution choosenFoodDistribution = gainers[Random.Range(0, gainers.Count)];
        return levelFoodDistributions.IndexOf(choosenFoodDistribution);
    }

    private Vector3 ChooseARandomAvailablePosition()
    {
        int range = availablePositions.Count;
        int index = Random.Range(0, range);
        return availablePositions[index];
    }

    private void RemoveSnakePositions()
    {
        List<SnakePartController> snakeParts = snake.GetSnakeParts();
        foreach (SnakePartController snakePart in snakeParts)
        {
            availablePositions.Remove(snakePart.transform.position);
        }
    }

    void ResetTimer(ref float _timer, float _resetValue, float _range)
    {
        _timer = _resetValue + Random.Range(-1f, 1f) * _range * 0.5f;
    }

    void RunTimer(ref float _timer)
    {
        _timer -= Time.deltaTime;
        
    }

    void CopyList<T>(List<T> _targetList, List<T> _originalList) where T: ICloneable,new()
    {
        foreach (T item in _originalList)
        {
            object newItem = item.Clone();
            _targetList.Add((T)newItem);
        }
    }

    void PauseGame()
    {
        Pause.Invoke();
    }

    void ContinueGame()
    {
        Continue.Invoke();
    }
}


