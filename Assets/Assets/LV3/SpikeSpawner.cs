using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spikeRollerPrefab;
    public Transform spawnPoint;

    private bool hasSpawned = false;
    //3shan yrun once per trigger only 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag=="Player" && !hasSpawned){
            Instantiate(spikeRollerPrefab, spawnPoint.position, spawnPoint.rotation);
            hasSpawned = true;
        }
    }
}
