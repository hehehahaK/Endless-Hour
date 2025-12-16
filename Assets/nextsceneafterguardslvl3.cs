using UnityEngine;
using UnityEngine.SceneManagement;

public class nextsceneafterguardslvl3 : MonoBehaviour
{

    public GameObject guard1; 
    public GameObject guard2; 


    public int sceneIndexToLoad; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            if (guard1 == null && guard2 == null)
            {
                Debug.Log("Loading next level");
                SceneManager.LoadScene(sceneIndexToLoad);
            }
            else
            {
                
                Debug.Log("The targets are still alive, u need to kill them so u can leave.");
            }
        }
    }
}