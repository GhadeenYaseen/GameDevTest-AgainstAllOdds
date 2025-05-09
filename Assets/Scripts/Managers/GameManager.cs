using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/*
    handle winning and losing 
*/

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager gameManagerInstance {get; private set;}

    [SerializeField] private List<GameObject> playersList = new List<GameObject>();

    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    [SerializeField] private GameObject bgMusic;
    
    [HideInInspector] public int playersCount;

    private void Awake() 
    {
        Time.timeScale = 1;

        if(gameManagerInstance != null && gameManagerInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gameManagerInstance = this;
        }

        playersCount = playersList.Count;
    }

    private void OnEnable() 
    {
        Time.timeScale = 1;
    }

    public void GameOver(bool isWin)
    {
        bgMusic.SetActive(false);

        if(isWin)
        {
            WinState();
        }
        else
        {
            LoseState();
        }
        
        DOTween.KillAll();
        ScoreManager.scoreManagerInstance.SaveScore();
        Time.timeScale = 0;
    }

    public void UpdatePlayers()
    {
        playersCount --;

        if(playersCount <= 0 )
        {
            LoseState();
        }
    }

    void WinState()
    {
        winScreen.SetActive(true);
    }

    void LoseState()
    {
        loseScreen.SetActive(true);
    }
}
