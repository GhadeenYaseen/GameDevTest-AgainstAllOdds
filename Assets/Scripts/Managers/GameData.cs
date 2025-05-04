using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int highScore;

    //construct
    public GameData (ScoreManager scoreManager)
    {
        highScore = scoreManager._highScore;
    }
}
