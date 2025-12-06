using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuliusController : MonoBehaviour
{
    public GameObject skullProjectilePrefab;
    public GameObject spellProjectilePrefab;

    public int maxHealth = 100;
    //public GameObject deathEffect;
    private float currentHealth;
    public float moveSpeed = 4f;
    public float attackCooldown = 10f;
    //public GameObject attackEffect;
    //public Transform attackPoint;
    private float AttackTimer = 0f;
    private bool isAttacking;
    public Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isDead = false;
    public Animator anim;
    public float AttackThreshold=  0.35f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth=maxHealth;
        isAttacking=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth==0f) isDead=false;
        if (!isAttacking){

            StartCoroutine(DoAttacks());
        }
        if (isDead){
            Destroy(this);
        }
    }
    IEnumerator DoAttacks(){
        isAttacking=true;
        if (currentHealth>AttackThreshold*maxHealth){
            yield return StartCoroutine(SummonSkulls());
        }
        else
        {
            yield return StartCoroutine(SummonSpells());
        }
    }
    //phase 1
IEnumerator SummonSkulls()
{
    Debug.Log("Start Skull Attack!");

float attackDuration=3f;
float timeBetweenSkulls=0.4f;
float timer=0f;
while (timer<attackDuration)
     {
        SpawnSkull();
        //attack lasts 3 seconds
    yield return new WaitForSeconds(3f);
    timer+=timeBetweenSkulls;
     }
    Debug.Log("Skull Attack Finished!");
    isAttacking=false;
}
// phase 2
IEnumerator SummonSpells()
{
    Debug.Log("Start Spell Attack!");

    yield return new WaitForSeconds(3f);

    Debug.Log("Spell Attack Finished!");
}
void SpawnSkull(){
float minX=Mathf.Min(transform.position.x,player.position.x);
float maxX=Mathf.Min(transform.position.x,player.position.x );
float randomX =Random.Range(minX,maxX);
float spawnHeight = 6.73f;
Vector3 spawnPos = new Vector3(randomX, transform.position.y + spawnHeight, 0f);
    Instantiate(skullProjectilePrefab, spawnPos, transform.rotation);

}
    /*Julius logic? how should i make his logic? 
    brain says to do this : -> this is how the fight will go, 
    summon attack1 until HP goes below certain threshold, 
    after which he goes into the summon attack 2
    */
}
