using UnityEngine;

public class EnemyCollisionFeedback : MonoBehaviour
{
    [SerializeField] private string soundName = "EnemyHit";
    [SerializeField] private GameObject hitParticlePrefab;

    [SerializeField] private float shakeDuration = 0.25f;
    [SerializeField] private float shakeStrength = 0.3f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy")) return;

        SoundManager.Instance.Play(soundName);

        Vector2 hitPoint = col.ClosestPoint(transform.position);
        Instantiate(hitParticlePrefab, hitPoint, Quaternion.identity);
        if (CameraShake.Instance != null)
        {
            CameraShake.Instance.Shake(shakeDuration, shakeStrength);
        }
    }
}