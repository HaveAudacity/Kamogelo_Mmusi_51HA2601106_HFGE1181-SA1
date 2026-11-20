using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance;

    [SerializeField] private CanvasGroup fadeGroup;
    [SerializeField] private float fadeDuration = 0.5f;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
        fadeGroup.alpha = 1f;
    }

    private void Start()
    {
        StartCoroutine(Fade(1f, 0f));
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeAndLoad(sceneName));
    }

    private IEnumerator FadeAndLoad(string sceneName)
    {
        yield return Fade(0f, 1f);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator Fade(float from, float to)
    {
        fadeGroup.blocksRaycasts = true;
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            fadeGroup.alpha = Mathf.Lerp(from, to, t / fadeDuration);
            yield return null;
        }
        fadeGroup.alpha = to;
        fadeGroup.blocksRaycasts = to > 0f;
    }
}