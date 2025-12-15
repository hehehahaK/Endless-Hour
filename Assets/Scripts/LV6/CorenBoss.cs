using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CorenBoss : MonoBehaviour
{

    public Transform playerTransform;
    // Health parameters
    public int maxHealth = 100;
    public int currentHealth;
    public float phase2Threshold = 0.35f; // 35% health to enter phase 2
    //Combat parameters
    public float attackDamage = 10f;
    public float meleeAttackCooldown = 3f;
    private float meleeAttackTimer = 0f;
    // Components
    public Animator anim;
    public Rigidbody2D rb;
    public CorenStateMachine stateMachine;
    // the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        stateMachine = GetComponent<CorenStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        // UpdateMeleeAttack();
    }
    // phase 1 attacks

    public void TimeBoltBarrage()
    {
        Debug.Log("Coren Boss Phase 1 Attack 1 executed.");
        // implement (refine)
    }
    public void ClockHandSweep()
    {
        Debug.Log("Coren Boss Phase 1 Attack 3 executed.");
        // implement (refine)

    }
    public void TimeProjectiles()
    {
        Debug.Log("Coren Boss Phase 1 Attack 4 executed.");
        // implement (refine)
    }
    // phase 2 attacks
    public void TemporalCloning()
    {
        Debug.Log("Coren Boss Phase 2 Attack 1 executed.");
        // implement (refine)
    }
    public void TimeFreezeTrap()
    {
        Debug.Log("Coren Boss Phase 2 Attack 2 executed.");
        // implement (refine)
    }
    public void DiveBomb()
    {
        Debug.Log("Coren Boss Phase 2 Attack 4 executed.");
        // implement (refine)
    }

    private void UpdateMeleeAttack()
    {
        meleeAttackTimer -= Time.deltaTime;
        if (meleeAttackTimer <= 0f)
        {
            anim.SetTrigger("MeleeAttack");
            meleeAttackTimer = meleeAttackCooldown;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Coren took " + damage + " damage. Health: " + currentHealth);

        if (currentHealth <= maxHealth * phase2Threshold && currentHealth > 0)
        {
            SceneManager.LoadScene("LV6Scene2"); // Change to your Phase 2 scene name
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Coren died!");
        gameObject.SetActive(false);
    }

}