using System.Collections;
using UnityEngine;

public class HitStop6 : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    public static void Execute(float duration, float shakeAmount)
    {
        FindObjectOfType<HitStop6>().StartCoroutine(HitStopCoroutine(duration, shakeAmount));
    }

    private static IEnumerator HitStopCoroutine(float duration, float shakeAmount)
    {
        Time.timeScale = 0f;
        
        Camera cam = Camera.main;
        Vector3 originalPos = cam.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float randomX = Random.Range(-shakeAmount, shakeAmount);
            float randomY = Random.Range(-shakeAmount, shakeAmount);
            cam.transform.position = originalPos + new Vector3(randomX, randomY, 0);
            yield return null;
        }

        cam.transform.position = originalPos;
        Time.timeScale = 1f;
    }
}