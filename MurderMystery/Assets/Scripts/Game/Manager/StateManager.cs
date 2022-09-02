using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// This class Manages the states in the game
/// </summary>
public class StateManager : MonoBehaviour
{
    public GameStates gameState;
    
    //Getters
    public GameStates GetCurrentGameState() =>  gameState;

    public void BranchHasPerCurrentState(bool ISA)
    {
        if(ISA)
        {
            if (gameState == GameStates.Scene) gameState = GameStates.SceneA;
            if (gameState == GameStates.EndA) gameState = GameStates.EndASceneA;
            if (gameState == GameStates.EndB) gameState = GameStates.EndBSceneA;
            if (gameState == GameStates.EndC) gameState = GameStates.EndCSceneA;
        }
        else
        {
            if (gameState == GameStates.Scene) gameState = GameStates.SceneB;
            if (gameState == GameStates.EndA) gameState = GameStates.EndASceneB;
            if (gameState == GameStates.EndB) gameState = GameStates.EndBSceneB;
            if (gameState == GameStates.EndC) gameState = GameStates.EndCSceneB;
        }
       
    }
   
    //Setters
    public void BranchAChosen() //function called when we click on the First option 
    {
        int diver = GameManager.Instance.GetCurrentScene().GetDivergence();
        Debug.Log(diver);
        GameManager.Instance.UpdateDivergence(diver);
        BranchHasPerCurrentState(true);
        GameManager.Instance.InBranchState();
        GameManager.Instance.HideOptions();
    }

    public void BranchBChosen() //function called when we click on the Second option 
    {
        int diver = GameManager.Instance.GetCurrentScene().GetDivergence();
        GameManager.Instance.UpdateDivergence(-diver);
        BranchHasPerCurrentState(false);
        GameManager.Instance.InBranchState();
        GameManager.Instance.HideOptions();
    }

    public void EvaluateEndingBranch(int divergence)
    {
        if (Math.Abs(divergence) > 10)
        {
            if (divergence > 0)
            {
                gameState = GameStates.EndA;
            }
            else
            {
                gameState = GameStates.EndB;
            }

        }
        else
        {
            gameState = GameStates.EndC;
        }

        GameManager.Instance.ProcessNextScene();
    }

    public void ReturnToMain()
    {
        if(gameState == GameStates.SceneA || gameState == GameStates.SceneB) gameState = GameStates.Scene; //Takes the control back to the main branch
        if(gameState == GameStates.EndASceneA || gameState == GameStates.EndASceneB) gameState = GameStates.EndA; //Takes the control back to the End A branch
        if(gameState == GameStates.EndBSceneA || gameState == GameStates.EndBSceneB) gameState = GameStates.EndB; //Takes the control back to the End B branch
        if(gameState == GameStates.EndCSceneA || gameState == GameStates.EndCSceneB) gameState = GameStates.EndC; //Takes the control back to the End C branch
    }

    public void SetState(int state) //Sets the state from the values obtained from the file
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
