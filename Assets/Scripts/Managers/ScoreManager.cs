using TMPro;
using UnityEngine;

/*
    handle score updation, and saving and loading
*/

public class ScoreManager : MonoBehaviour
{
    [HideInInspector] public static ScoreManager scoreManagerInstance {get; private set;}

    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI highScore;
    //[SerializeField] private TextMeshProUGUI leaderBoard;

    [SerializeField] private int amount;

    [HideInInspector] public int _currentScore=0, _highScore=0;

    private void Awake() 
    {
        if(currentScore != null) currentScore.text ="";

        if(scoreManagerInstance != null && scoreManagerInstance != this)
        {   
            Destroy(gameObject);
        }
        else
        {
            scoreManagerInstance = this;
        }
    }

    public void UpdateScore()
    {
        _currentScore += amount;

        if(currentScore != null) currentScore.text = "Score" + _currentScore.ToString();
        
        if(_highScore < _currentScore)
        {
            _highScore = _currentScore;
        }
    }

    // on button click
    public void SaveScore()
    {
        SaveManager.SaveScore(this);
        LoadScore();
    }

    // on button click
    public void LoadScore()
    {
        GameData data = SaveManager.LoadGameData();

        if(highScore != null) highScore.text =data.highScore.ToString();
    }
}
