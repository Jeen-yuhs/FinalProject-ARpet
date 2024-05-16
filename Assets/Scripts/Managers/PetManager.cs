using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PetManager : MonoBehaviour
{
    public static PetManager instance;

    public Button feedButton, playButton, danceButton, _restartGameButton;
    public GameObject gameboardCharacter;
    public GameObject xr;
    public GameObject gameoverPanel;

    private Animator petAnimation;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Debug.LogWarning("More than one PetManager in the Scene");
    }

    private void OnEnable()
    {
        PlacementOnMesh_Character.CharacterPlaced += StartAfterPlacement;
    }

    private void OnDisable()
    {
        PlacementOnMesh_Character.CharacterPlaced -= StartAfterPlacement;
    }

    private void StartAfterPlacement(NeedsController controller1, PetController controller2)
    {
        petAnimation = controller1.GetComponent<Animator>(); 

        feedButton.onClick.RemoveAllListeners();
        playButton.onClick.RemoveAllListeners();
        danceButton.onClick.RemoveAllListeners();

        
        feedButton.onClick.AddListener(() => 
        { 
            controller1.ChangeFood(10);
            Feed();
        });

        playButton.onClick.AddListener(() => { controller1.ChangeHappiness(10); });
        
        danceButton.onClick.AddListener(() => 
        {
            controller1.ChangeEnergy(10);
            Dance();
        });

        _restartGameButton.onClick.AddListener(RestartGame);
    }

    private void Dance()
    {
        petAnimation.SetTrigger("Dance");    
    }
    private void Feed()
    {
        petAnimation.SetTrigger("Feed");
    }  


    public void Die()
    {
        Debug.Log("Dead");
        gameoverPanel.gameObject.SetActive(true); // gameover
        Destroy(gameboardCharacter);
        Destroy(xr);// game over                
    }

    private void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
