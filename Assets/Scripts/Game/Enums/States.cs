/// <summary>
/// Holds all the possible scenes in the game
/// </summary>
public enum GameStates
{
    Scene, //Basic game scene , which the player will see in all cases -  0
    SceneA, //branchA scene which player will see on choosing an A options  -- 1
    SceneB, //branchB scene which player will see on choosing an B options -- 2
    EndA, //First ending branch -- 3
    EndASceneA, //branchA scene of EndA branch which player will see on choosing an A options -- 4
    EndASceneB, //branchB scene of EndA branch which player will see on choosing an B options -- 5
    EndB, //Second ending branch -- 6
    EndBSceneA, //branchA scene of EndB branch which player will see on choosing an A options -- 7
    EndBSceneB, //branchB scene of EndB branch which player will see on choosing an B options -- 8
    EndC, //Third ending branch -- 9
    EndCSceneA, //branchA scene of EndC branch which player will see on choosing an A options -- 10
    EndCSceneB //branchA scene of EndC branch which player will see on choosing an B options -- 11
}