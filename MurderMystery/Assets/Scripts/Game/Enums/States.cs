/// <summary>
/// Holds all the possible scenes in the game
/// </summary>
public enum GameStates
{
    Scene, //Basic game scene , which the player will see in all cases
    SceneA, //branchA scene which player will see on choosing an A options
    SceneB, //branchB scene which player will see on choosing an B options
    EndA, //First ending branch
    EndASceneA, //branchA scene of EndA branch which player will see on choosing an A options
    EndASceneB, //branchB scene of EndA branch which player will see on choosing an B options
    EndB, //Second ending branch
    EndBSceneA, //branchA scene of EndB branch which player will see on choosing an A options
    EndBSceneB, //branchB scene of EndB branch which player will see on choosing an B options
    EndC, //Third ending branch
    EndCSceneA, //branchA scene of EndC branch which player will see on choosing an A options
    EndCSceneB //branchA scene of EndC branch which player will see on choosing an B options
}