using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MinigameUIController : MonoBehaviour
{    
    public static MinigameUIController instance;
    //public event Action MiniGameStarted;
    //public event Action MiniGameEnded;

    public GameObject minigame;
    public TMP_Text scoreText, timerText;
    public MinigameEndPanelController minigameEndUI; 
    public bool victory;

    [SerializeField] private Button _minigameButton;

    private int score;
    private float timeRemaining;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Debug.LogWarning("More than one MinigameUIController in the Scene");

        _minigameButton.onClick.AddListener(StartMinigame);
    }

    private void StartMinigame()
    {
        gameObject.SetActive(true);
        //������� ������ ��������
        //GameObject prefab =default;
        //var a = Instantiate(prefab, transform);//������ ��������

        //MiniGameStarted?.Invoke();
    }

    public virtual void ChangeScore(int amount)
    {
        score += amount;        
        UpdateScore(score);
    }


    public void UpdateScore(int score) 
    {
        this.score = score;
        if (score >= 3) FinishMiniGame(score, timeRemaining);
        scoreText.text = "Score: " + score;
    }

    public void UpdateTimer(float timer)
    {
        timeRemaining = timer;
        timerText.text = string.Format("Time Left: {0}", timer);
    }

    public void FinishMiniGame(int score, float timeRemaining) 
    {
        Destroy(minigame);
        minigameEndUI.gameObject.SetActive(true);       
        minigameEndUI.Intialize(score, timeRemaining, timeRemaining >0);
        //MiniGameEnded?.Invoke();
     // PetController.GetComponent<NeedsController>().ChangeHappiness(20); // needs reference to needscontroller assigned to 3d pet clone???3    
    }

    public void LoseMiniGame() 
    {
        Destroy(minigame);
        
        minigame.SetActive(false);


      //PetController.GetComponent<NeedsController>().ChangeHappiness(10);// needs reference to needscontroller assigned to 3d pet clone???3
        minigameEndUI.gameObject.SetActive(true);
        minigameEndUI.Intialize(score, timeRemaining, false);
        //MiniGameEnded?.Invoke();
    }

    private void OnDestroy()
    {
        _minigameButton.onClick.RemoveListener(StartMinigame);
    }
}
