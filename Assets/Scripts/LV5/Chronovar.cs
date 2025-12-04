using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chronovar : MonoBehaviour
{
    // dragonstatws
    public int maxHealth = 100;
    public int currentHealth;
    public float moveSpeed = 3f;
    public float attackDamage = 10f;
    public Animator anim;
    public Rigidbody2D rb;
    public ChronovarStateMachine stateMachine;

    //thresholds
    public bool isEnraged = false;
    public float phase2Threshold = 0.6f; // 60% health
    public float phase3Threshold = 0.35f;  // 35% health

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        stateMachine = GetComponent<ChronovarStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
