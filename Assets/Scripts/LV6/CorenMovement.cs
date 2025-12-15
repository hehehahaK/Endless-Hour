using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorenMovement : MonoBehaviour
{
    public Transform playerTransform;
    public float followDistance = 5f;
    public float followHeight = 2f;
    public float followSpeed = 3f;
    public float hoverRange = 2f;
    public float hoverSpeed = 2f;
    public float groundedDuration = 6f;
    public float airborneDuration = 8f;

    private float groundedTime = 0f;
    private float airborneTime = 0f;
    private Vector3 targetPosition;
    public bool isAirborne = false;
    private Animator anim;

    public GameObject diveBombMarkerPrefab;
    public float diveBombDamage = 20f;
    public float diveBombRadius = 2f;
    private GameObject diveBombMarker;

    void Start()
    {
        anim = GetComponent<Animator>();
        isAirborne = true;
    }

    void Update()
    {
        UpdateMovement();
        UpdateAirborneState();
    }

    private void UpdateMovement()
    {
        if (!isAirborne) return;

        Vector3 playerPos = playerTransform.position;
        Vector3 playerPosIgnoreY = new Vector3(playerPos.x, 0, playerPos.z);

        Vector3 backOffset = -playerTransform.forward * followDistance;
        Vector3 baseFollowPos = playerPosIgnoreY + backOffset + Vector3.up * followHeight;

        float hoverOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverRange;
        targetPosition = baseFollowPos + playerTransform.right * hoverOffset;

        Vector3 newPos = transform.position;
        newPos.x = Mathf.Lerp(newPos.x, targetPosition.x, followSpeed * Time.deltaTime);
        newPos.z = Mathf.Lerp(newPos.z, targetPosition.z, followSpeed * Time.deltaTime);
        transform.position = newPos;

        if (playerPos.x > transform.position.x)
        {
            transform.localScale = new Vector3(4, 3, 1);
        }
        else
        {
            transform.localScale = new Vector3(-4, 3, 1);
        }
    }

    private void UpdateAirborneState()
    {
        if (isAirborne)
        {
            airborneTime += Time.deltaTime;
            Vector3 newPos = transform.position;
            newPos.y = Mathf.Lerp(newPos.y, 3f, 2f * Time.deltaTime);
            transform.position = newPos;
            anim.SetBool("CorenGrounded", false);

            if (airborneTime >= airborneDuration)
            {
                isAirborne = false;
                airborneTime = 0f;
                groundedTime = 0f;
                SpawnDiveBombMarker();
            }
        }
        else
        {
            groundedTime += Time.deltaTime;
            Vector3 newPos = transform.position;
            newPos.y = Mathf.Lerp(newPos.y, -0.5f, 2f * Time.deltaTime);
            transform.position = newPos;
            anim.SetBool("CorenGrounded", true);

            if (newPos.y <= -0.1f && groundedTime < 5f) // Only trigger once at start
            {
                DealDiveBombDamage();
            }
            if (groundedTime >= groundedDuration)
            {
                isAirborne = true;
                groundedTime = 0f;
                airborneTime = 0f;
            }
        }
    }

    private void SpawnDiveBombMarker()
    {
        Vector3 playerPos = playerTransform.position;
        Vector3 markerPos = new Vector3(playerPos.x, -3.6f, 0);
        diveBombMarker = Instantiate(diveBombMarkerPrefab, markerPos, Quaternion.identity);
    }

    private void DealDiveBombDamage()
    {
        if (diveBombMarker != null)
        {
            Vector3 markerPos = diveBombMarker.transform.position;
            Collider2D[] hits = Physics2D.OverlapCircleAll(markerPos, diveBombRadius);
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    hit.GetComponent<Atreus6PlayerSuperclassa>().TakeDamage((int)diveBombDamage);
                }
            }
            Destroy(diveBombMarker);
        }
    }
}