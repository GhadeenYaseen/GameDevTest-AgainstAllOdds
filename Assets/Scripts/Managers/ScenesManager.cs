using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System.Threading.Tasks;

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
            Destroy(this.gameObject);
        }
        else
        {
            ScenesManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void StartLoading(string sceneName) 
    {
        StartCoroutine(_LoadScene(sceneName));
    }

    IEnumerator _LoadScene(string scenmeName) 
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scenmeName);
        
        loadingCanvas.SetActive(true);

        while (! operation.isDone) 
        {
            yield return null;
        }

        loadingCanvas.SetActive(false);
    }

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

    private void Update() 
    {
        progressBar.value = Mathf.MoveTowards(progressBar.value, _target, 3 * Time.deltaTime);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
