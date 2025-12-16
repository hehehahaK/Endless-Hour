using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class triggScene : MonoBehaviour
{
    public int sceneIndex; // scene index 
    private bool hasLoaded = false; // prevent multiple loads

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasLoaded && other.CompareTag("Player")) // change tag if needed
        {
            hasLoaded = true;
            StartCoroutine(LoadScene());
        }
    }

    private IEnumerator LoadScene()
    {
        // Optional delay before switching
        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene(sceneIndex);
    }
}
