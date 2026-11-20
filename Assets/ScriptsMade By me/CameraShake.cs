using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private float duration;
    private float strength;
    private float timeLeft;

    private void Awake()
    {
        Instance = this;
    }

    public void Shake(float shakeDuration, float shakeStrength)
    {
        duration = shakeDuration;
        strength = shakeStrength;
        timeLeft = shakeDuration;
    }

    private void LateUpdate()
    {
        if (timeLeft <= 0f) return;

        float fade = timeLeft / duration;
        Vector2 offset = Random.insideUnitCircle * strength * fade;
        transform.position += new Vector3(offset.x, offset.y, 0f);

        timeLeft -= Time.unscaledDeltaTime;
    }
}