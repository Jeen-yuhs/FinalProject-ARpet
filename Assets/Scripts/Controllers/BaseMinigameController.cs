// Copyright 2022-2024 Niantic.
using UnityEngine;

public class BaseMinigameController : MonoBehaviour
{
    protected int score;
    public float timer;   
  
    protected void Start() 
    {
        if (timer == 0) 
        {
            ChangeTimer(30);
        }
    }
     
    protected virtual void ChangeTimer(float change) 
    {
        timer += change;
        if (MinigameUIController.instance == null) return;
        MinigameUIController.instance.UpdateTimer(timer);
    }

    protected virtual void Update() 
    { 
        ChangeTimer(-Time.deltaTime);
        if (timer < 0) 
        {
            MinigameUIController.instance.FinishMiniGame(score, timer);
            Destroy(gameObject, 1);
        }
    }

    protected virtual void GoalReached() 
    {
        if (MinigameUIController.instance == null) return;
        MinigameUIController.instance.FinishMiniGame(score, timer);
        Destroy(gameObject);
    }
}
