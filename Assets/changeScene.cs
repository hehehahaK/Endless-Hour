using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public Chronovar chronovar;
    public int sceneIndex; // index  Settings

    private bool hasLoaded = false; // prevent multiple loads

    void Update()
    {
        if (chronovar == null && !hasLoaded)
        {
            hasLoaded = true;
            StartCoroutine(LoadScene());
        }
    }

    private IEnumerator LoadScene()
    {
        // small delay
        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene(sceneIndex);
    }
}
