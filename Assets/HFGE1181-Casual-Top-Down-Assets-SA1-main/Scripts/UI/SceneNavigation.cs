using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void LoadScene()
    {
        if (SceneFader.Instance != null)
        {
            SceneFader.Instance.FadeToScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
