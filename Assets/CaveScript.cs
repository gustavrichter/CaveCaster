using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveScript : MonoBehaviour
{
    public GameObject enemy;
    public Transform spawnPoint;
    private GameObject clonedEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        clonedEnemy = Instantiate(enemy, spawnPoint);
        
    }
}
