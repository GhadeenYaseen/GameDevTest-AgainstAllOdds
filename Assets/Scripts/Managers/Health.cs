using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    main health system, deal with damage and death and its ui
*/

public class Health : MonoBehaviour
{
    [SerializeField] private List<Image> ui_Hearts = new List<Image>();

    private int _currentHealth;
    private bool _isWin;

    private void Awake() 
    {
        if(ui_Hearts != null)
        {
            _currentHealth = ui_Hearts.Count;
        }
        else
        {
            _currentHealth = 3;
        }
    }

    public void TakeDamage(GameObject gameObject)
    {
        _currentHealth --;

        SoundManager.PlaySound(SoundType.Hurt);

        try
        {
            ui_Hearts[_currentHealth].gameObject.SetActive(false);
        }
        catch (System.Exception)
        {
            Debug.Log("u r dead");
        }

        if(_currentHealth <= 0 )
        {
            if(gameObject.CompareTag("Enemy"))
            {
                //gameObject.SetActive(false);
                
                _isWin = true;
                
                GameManager.gameManagerInstance.GameOver(_isWin);
            }
            else if(gameObject.CompareTag("Player"))
            {
                gameObject.SetActive(false);

                //_isWin = false;

                GameManager.gameManagerInstance.UpdatePlayers();
                //GameManager.gameManagerInstance.GameOver(_isWin);
            }
        }
    }
}
