using UnityEngine;
using System.Collections;

public class TrapdoorController : MonoBehaviour
{
  
    public Transform bridge;     
    public Transform rope;     


    public float openSpeed = 2f; 
    public float targetAngle = 90f;
    public float ropeShrinkSize = 0.2f; 

    private bool isOpening = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpening)
        {
            isOpening = true;
            StartCoroutine(AnimateMechanism());
            
            GetComponent<BoxCollider2D>().enabled = false; 
        }
    }

    private IEnumerator AnimateMechanism()
    {
        Quaternion startRotation = bridge.rotation;
        // to see and calc where the goal where we end up should be
        Quaternion endRotation = Quaternion.Euler(0, 0, startRotation.eulerAngles.z + targetAngle);
        
        Vector3 startScale = rope.localScale;
        Vector3 endScale = new Vector3(startScale.x, ropeShrinkSize, startScale.z);

        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed;

            
            bridge.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            
            rope.localScale = Vector3.Lerp(startScale, endScale, t);

            yield return null;
        }
    }
}