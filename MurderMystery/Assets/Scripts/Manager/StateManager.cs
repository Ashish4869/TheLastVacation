using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class Manages the states in the game
/// </summary>
public class StateManager : MonoBehaviour
{
    public GameStates gameState;

    private void Awake()
    {
        gameState = GameStates.Scene;
    }
    
    //Getters
    public GameStates GetCurrentGameState()
    {
        return gameState;
    }


    //Setters
    public void BranchAChosen() //function called when we click on the First option 
    {
        gameState = GameStates.SceneA;
        GameManager.Instance.HideOptions();
    }

    public void BranchBChosen() //function called when we click on the Second option 
    {
        gameState = GameStates.SceneB;
        GameManager.Instance.HideOptions();
    }

    public void ReturnToMain() //Takes the control back to the main branch
    {
        gameState = GameStates.Scene;
    }

}
