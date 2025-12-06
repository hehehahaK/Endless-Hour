using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//textmesh for the health txt



public class Healthbar : MonoBehaviour
{
    // Start is called before the first frame update

    public Image healthbarfill;
    public float fillSpeed;
    private float lerpSpeed = 5f;
    public float currentFillAmount = 1f;
    private float targetFillAmount = 1f;
    public TextMeshProUGUI healthtxt;

    PlayerSuperclass playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerSuperclass>();
        float initialRatio = (float)playerHealth.health / playerHealth.maxHealth;
        this.targetFillAmount = initialRatio;
        this.currentFillAmount = initialRatio;
        
        healthtxt.text = "Health: " + playerHealth.health.ToString();
    }


    public void updateHealthBar(){
        
       if (playerHealth != null && playerHealth.maxHealth > 0)
        {
        this.targetFillAmount = (float)playerHealth.health / playerHealth.maxHealth;
        }
       // lazemmm float bec represents ratio in unity, the drag scroll bar thingy 
       
    }

    void Update()
    {
    // 1. Smoothly move the current amount towards the target amount
    currentFillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, Time.deltaTime * lerpSpeed);
    
    // 2. Apply the smoothed value to the UI image
    healthbarfill.fillAmount = currentFillAmount;

    healthtxt.text = "Health: " + playerHealth.health.ToString();
    }

}
