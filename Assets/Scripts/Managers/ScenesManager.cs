using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

/*
    Singleton scenes manager with loading screen
*/

public class ScenesManager : MonoBehaviour
{
    [HideInInspector] public static ScenesManager ScenesManagerInstance {get; private set;}
    
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private Slider progressBar;

    private float _target;

    private void Awake()
    {
        if (ScenesManagerInstance != null && ScenesManagerInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            ScenesManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // load scene, called when ui button calls for changing scenes (play, quit to menu)
    public async void LoadScene(string sceneName)
    {
        progressBar.value = 0;

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loadingCanvas.SetActive(true);

        do
        {
            await Task.Delay(1000);
            _target= scene.progress;

        } while (scene.progress < 0.9f);

        await Task.Delay(1000);

        scene.allowSceneActivation = true;
        loadingCanvas.SetActive(false);
    }

    // gradually fill up progress bar
    private void Update() 
    {
        progressBar.value = Mathf.MoveTowards(progressBar.value, _target, 3 * Time.deltaTime);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
