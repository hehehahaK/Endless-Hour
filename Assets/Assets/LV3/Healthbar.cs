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
    public Gradient colorgradientwowowoow;
    

    PlayerSuperclass playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerSuperclass>();
        float initialRatio = (float)playerHealth.health / playerHealth.maxHealth;
        this.targetFillAmount = initialRatio;
        this.currentFillAmount = initialRatio;
        
        healthbarfill.color = colorgradientwowowoow.Evaluate(initialRatio);
        
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
   
    currentFillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, Time.deltaTime * lerpSpeed);
    // makontsh 3yza ast3ml DGTweening, lesa n3ml install 3nd kolo w instructions w bta3, lallala heya math lerp
    
    // smooooottthhhhh
    healthbarfill.fillAmount = currentFillAmount;


    healthbarfill.color = colorgradientwowowoow.Evaluate(currentFillAmount);

    
    }

}
