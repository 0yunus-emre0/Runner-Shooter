using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static GameManager Instance{
        get{
            if(instance == null){
                instance = new GameObject("GameManager").AddComponent<GameManager>();
                instance.tag = "GameController";
            }
            return instance;
        }
    }
    #region Events
    public delegate void FireRateChangeHandler(float amount);
    public event FireRateChangeHandler OnFireRateChanged;
    public delegate void AttackRangeChangeHandler(float amount);
    public event AttackRangeChangeHandler OnAttackRangeChanged;
    public delegate void YearChangeHandler (int year);
    public event YearChangeHandler OnYearChanged;
    public delegate void LevelStatusHandler(bool levelStatus);
    public event LevelStatusHandler OnLevelChanged;
    #endregion

    public bool isGameFailed = false;
    public bool isLevelCompleted = false;
    public bool isGameStarted = false;
    public void SetAttackRange(float amount){
        OnAttackRangeChanged?.Invoke(amount);
    }
    public void SetFireRate(float amount){
        OnFireRateChanged?.Invoke(amount);
    }
    public void SetYear(int year){
        OnYearChanged?.Invoke(year);
    }
    public void SetLevelStatus(bool status){
        if(!status){
            isGameFailed = true;
        }
        else if(status){
            isLevelCompleted = true;    
        }
        OnLevelChanged?.Invoke(status);

    }

    private void OnEnable()
    {
        
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);

        }
        
        
    }
}
public enum GameModes{
    GamePlay,
    Paused
}
