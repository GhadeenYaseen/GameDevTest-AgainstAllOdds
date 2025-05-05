
/*
    define data to save here
*/

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
