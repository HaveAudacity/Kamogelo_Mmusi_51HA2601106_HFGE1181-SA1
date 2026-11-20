using UnityEngine;

public class PlayMusicOnStart : MonoBehaviour
{
    [SerializeField] private string musicName = "Music";

    private void Start()
    {
        SoundManager.Instance.Play(musicName);
    }
}
