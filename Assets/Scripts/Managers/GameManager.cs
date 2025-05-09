using System.Collections.Generic;
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
    [HideInInspector] public bool isGameOver=false;

    private void Awake() 
    {
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

    public void GameOver(bool isWin)
    {
        isGameOver = true;
        //bgMusic.SetActive(false);

        SecondarySoundManager.secondarySoundManagerInstance.BackgroundSoundPlayer(false);
        
        ScoreManager.scoreManagerInstance.SaveScore();

        if(isWin)
        {
            WinState();
        }
        else
        {
            LoseState();
        }
    }

    public void UpdatePlayers()
    {
        playersCount --;

        if(playersCount <= 0 )
        {
            GameOver(false);
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
