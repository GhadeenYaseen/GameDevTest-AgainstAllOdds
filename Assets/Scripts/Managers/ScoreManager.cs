using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [HideInInspector] public static ScoreManager scoreManagerInstance {get; private set;}

    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI leaderBoard;

    [SerializeField] private int amount;

    [HideInInspector] public int _currentScore, _highScore;

    private void Awake() 
    {
        currentScore.text ="";
        _currentScore = 0;
    }

    public void UpdateScore()
    {
        _currentScore += amount;

        currentScore.text = "Score" + _currentScore.ToString();
        
        if(_highScore < _currentScore)
        {
            _highScore = _currentScore;
            highScore.text=_highScore.ToString();
        }
    }

    // on button click
    public void SaveScore()
    {
        SaveManager.SaveScore(this);
    }

    // on button click
    public void LoadScore()
    {
        GameData data = SaveManager.LoadGameData();

        _highScore = data.highScore;
    }
}
