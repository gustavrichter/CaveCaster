using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CaveScript : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnPoints; //size 3
    private GameObject clonedEnemy;
    [SerializeField] private GameObject[] m_Enemies; //size 6
    private List<EnemyScript> m_EnemyScripts;
    private List<GameObject> m_EnemiesOnFloor;
    private int m_numberOfEnemies;
    public Action StageComplete = delegate { };
    public Action EnemiesAhead = delegate { };
    // Start is called before the first frame update
    private void Awake()
    {
        m_EnemiesOnFloor = new List<GameObject>();
        m_EnemyScripts = new List<EnemyScript>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        if (m_numberOfEnemies > 0)
        {
            CleanSpawn();
        }
        m_numberOfEnemies = UnityEngine.Random.Range(1, 4);
        for (int i = 0; i < m_numberOfEnemies; i++)
        {
            m_EnemiesOnFloor.Add(Instantiate(m_Enemies[UnityEngine.Random.Range(0, m_Enemies.Length)]) as GameObject);
            m_EnemiesOnFloor[i].transform.SetParent(spawnPoints[i].transform, false);
            m_EnemyScripts.Add(m_EnemiesOnFloor[i].GetComponent<EnemyScript>());
            m_EnemyScripts[i].m_id = i;
            m_EnemyScripts[i].EnemyDeath += RemoveEnemy;
        }
        EnemiesAhead();
    }

    void CleanSpawn()
    {
        for (int i = 0; i < m_EnemiesOnFloor.Count; i++)
        {
            RemoveEnemy(i);
        }
    }
    private void RemoveEnemy(int id)
    {
        m_EnemyScripts[id].EnemyDeath -= RemoveEnemy;
        m_EnemiesOnFloor[id].gameObject.SetActive(false);
        m_numberOfEnemies--;
        if(m_numberOfEnemies <= 0)
        {
            FinishStage();
        }
    }

    private void FinishStage()
    {
        for (int i = 0; i < m_EnemiesOnFloor.Count; i++)
        {
            Destroy(m_EnemiesOnFloor[i]);
        }
        m_EnemyScripts.Clear();
        m_EnemiesOnFloor.Clear();
        StageComplete();
    }
}
