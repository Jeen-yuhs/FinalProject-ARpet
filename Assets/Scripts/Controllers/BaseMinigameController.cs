using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class BaseMinigameController : MonoBehaviour
{
    protected int score;
    protected float timer;

    protected virtual void Awake() 
    {
        if (timer == 0) 
        {
            ChangeTimer(30);
        }
    }

    protected virtual void ChangeScore(int amount) 
    {
        score += amount;
        MinigameUIController.instance.UpdateScore(score);
    }

    protected virtual void ChangeTimer(float change) 
    {
        timer += change;
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
        MinigameUIController.instance.FinishMiniGame(score, timer);
        Destroy(gameObject);
    }
}
