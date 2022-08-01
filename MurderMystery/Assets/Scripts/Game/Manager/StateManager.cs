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
      
    }
    
    //Getters
    public GameStates GetCurrentGameState() =>  gameState;
   
    //Setters
    public void BranchAChosen() //function called when we click on the First option 
    {
        gameState = GameStates.SceneA;
        GameManager.Instance.InBranchState();
        GameManager.Instance.HideOptions();
    }

    public void BranchBChosen() //function called when we click on the Second option 
    {
        gameState = GameStates.SceneB;
        GameManager.Instance.InBranchState();
        GameManager.Instance.HideOptions();
    }

    public void ReturnToMain() => gameState = GameStates.Scene; //Takes the control back to the main branch

    public void SetState(int state)
    {
        switch (state)
        {
            case 0:  gameState = GameStates.Scene; break;
            case 1: gameState = GameStates.SceneA; break;
            case 2: gameState = GameStates.SceneB; break;
            case 3: gameState = GameStates.EndA; break;
            case 4: gameState = GameStates.EndASceneA; break;
            case 5: gameState = GameStates.EndASceneB; break;
            case 6: gameState = GameStates.EndB; break;
            case 7: gameState = GameStates.EndBSceneA; break;
            case 8: gameState = GameStates.EndBSceneB; break;
            case 9: gameState = GameStates.EndC; break;
            case 10: gameState = GameStates.EndCSceneA; break;
            case 11: gameState = GameStates.EndCSceneA; break;

            default:  return;
        }
    }
    

}
