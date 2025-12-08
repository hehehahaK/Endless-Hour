using UnityEngine;

public class TriggerForwarder : MonoBehaviour
{
    public MonoBehaviour parentScript;
    private System.Reflection.MethodInfo triggerMethod;

    void Start()
    {
        if (parentScript != null)
        {
            triggerMethod = parentScript.GetType().GetMethod("OnChildTriggerEnter");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerMethod != null)
        {
            triggerMethod.Invoke(parentScript, new object[] { other });
        }
    }
}