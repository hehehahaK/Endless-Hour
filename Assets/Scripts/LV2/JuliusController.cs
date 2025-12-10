using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuliusController : MonoBehaviour
{
    public GameObject skullProjectilePrefab;
    public GameObject spellProjectilePrefab;
    public GameObject redDotSkull;
    public int maxHealth = 100;
    //public GameObject deathEffect;
    public float currentHealth;
    public float moveSpeed = 4f;
    public float attackCooldown = 10f;
    //public GameObject attackEffect;
    //public Transform attackPoint;
    protected float AttackTimer = 0f;
    protected bool isAttacking;
    public Transform player;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected bool isDead = false;
    public Animator anim;
    public float AttackThreshold = 0.35f;
    protected PlayerSuperclass playerController;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        isAttacking = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerController = player.GetComponent<PlayerSuperclass>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth == 0) isDead = true;
        if (!isAttacking)
        {

            StartCoroutine(DoAttacks());
        }
        if (isDead)
        {
            Die();
        }

    }
    IEnumerator DoAttacks()
    {
        isAttacking = true;
        if (currentHealth > AttackThreshold * maxHealth)
        {
            yield return StartCoroutine(SummonSkulls());
        }
        else
        {
            SpawnSpell();
            yield return new WaitForSeconds(2f);
        }
    }
    //phase 1
    IEnumerator SummonSkulls()
    {
        Debug.Log("Start Skull Attack!");

        float attackDuration = 3f;
        float timeBetweenSkulls = 0.6f;
        float timer = 0f;
        while (timer < attackDuration)
        {
            SpawnSkull();
            //attack lasts 3 seconds
            yield return new WaitForSeconds(timeBetweenSkulls);
            timer += timeBetweenSkulls;
        }
        Debug.Log("Skull Attack Finished!");
        isAttacking = false;
    }
    // phase 2
    /*IEnumerator SummonSpells()
    {
        float attackDuration=3f;
    float timeBetweenWheels=0.6f;
    float timer=0f;
        Debug.Log("Start Spell Attack!");
    while (timer<attackDuration){
        SpawnSpell();
        yield return new WaitForSeconds(timeBetweenWheels);
    timer+=timeBetweenWheels;
    }
        Debug.Log("Spell Attack Finished!");
    }
    */
    void SpawnSkull()
    {
        float minX = Mathf.Min(transform.position.x, player.position.x);
        float maxX = Mathf.Max(transform.position.x, player.position.x);
        float randomX = Random.Range(minX, maxX);
        float spawnHeight = 6.73f;
        Vector3 spawnPos = new Vector3(randomX, transform.position.y + spawnHeight, 0f);
        Vector3 redPos = new Vector3(randomX, 30.61f, 0f);
        int randValue = Random.Range(0, 6);
        float zval = 0f;
        switch (randValue)
        {
            case 0:
                zval = 0f;
                break;
            case 1:
                zval = 35f;
                break;
            case 2:
                zval = 70f;
                break;
            case 3:
                zval = 180f;
                break;
            case 4:
                zval = 225f;
                break;
            case 5:
                zval = 300f;
                break;
        }
        anim.SetTrigger("doSkull");
        GameObject Skull = Instantiate(skullProjectilePrefab, spawnPos, Quaternion.Euler(0f, 0f, zval));
        GameObject redDotSkullPos = Instantiate(redDotSkull, redPos, Quaternion.Euler(0f, 0f, 0f));
        Skull.GetComponent<Skull>().player = player;
    }
    void SpawnSpell()
    {
        float offsetX = Random.Range(-5f, 5f); // 
        Vector3 spawnPos = new Vector3(transform.position.x + offsetX, transform.position.y, 0f);

        GameObject wheel = Instantiate(spellProjectilePrefab, spawnPos, Quaternion.Euler(0f, 0f, 0f));
        wheel.GetComponent<WheelLV2>().player = player;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerController.isAttacking)
            {
                TakeDamage(playerController.AttackDamage); // takes damage from player attack, should also do damage if they're attacking
            }

        }
    }
    public void TakeDamage(int damage)
    {

        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }
    private void Die()
    {
        isDead = true;

        rb.velocity = Vector2.zero;

        Destroy(gameObject, 0.1f);
    }

    /*Julius logic? how should i make his logic? 
    brain says to do this : -> this is how the fight will go, 
    summon attack1 until HP goes below certain threshold, 
    after which he goes into the summon attack 2
    */
}