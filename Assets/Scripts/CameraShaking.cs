using System.Collections;
using UnityEngine;

public class CameraShaking : MonoBehaviour
{
    public static CameraShaking Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Shake(float duration, float magnitude)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        Vector3 startLocalPos = transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            Vector2 offset =
                Random.insideUnitCircle * magnitude;

            transform.localPosition =
                startLocalPos + new Vector3(offset.x, offset.y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = startLocalPos;
    }
}