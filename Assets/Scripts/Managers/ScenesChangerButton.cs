using UnityEngine;

/*
    placed in ui button on click event to change scenes
*/

public class ScenesChangerButton : MonoBehaviour
{
    public void ChangeScene(string sceneName) 
    {
        ScenesManager.ScenesManagerInstance.LoadScene(sceneName);
    }
}
