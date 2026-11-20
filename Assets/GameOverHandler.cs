using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private string musicName = "Music";

    private bool handled = false;

    private void Update()
    {
        if (handled) return;

        if (GameStateManager.Instance.GetGameState() == GameState.GameOver)
        {
            handled = true;
            SoundManager.Instance.Stop(musicName);
            player.SetActive(false);
            Time.timeScale = 0f;
        }
    }
}