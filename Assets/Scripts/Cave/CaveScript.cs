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
    public Action EnemyHasBeenSlain = delegate { };
    // Start is called before the first frame update
    private void Awake()
    {
        m_EnemiesOnFloor = new List<GameObject>();
        m_EnemyScripts = new List<EnemyScript>();
    }
    void Start()
    {
        //StartCoroutine(DelayedSpawnEnemies(3.0f));
    }

    public void StopStage()
    {
        FinishStage();
    }

    public void PauseEnemies()
    {
        for (int i = 0; i < m_EnemiesOnFloor.Count; i++)
        {
            m_EnemyScripts[i].paused = true;
        }

        //StopAllCoroutines();
        
    }

    public void ResumeEnemies()
    {
        for (int i = 0; i < m_EnemiesOnFloor.Count; i++)
        {
            m_EnemyScripts[i].paused = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //IEnumerator DelayedSpawnEnemies(float waitTime)
    //{
    //    yield return new WaitForSeconds(waitTime);
    //    SpawnEnemy();
    //}

    public void SpawnEnemy()
    {
        if (m_EnemiesOnFloor.Count > 0)
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
    public void SpawnEnemyTutorial() {

        m_EnemiesOnFloor.Add(Instantiate(m_Enemies[0]) as GameObject);//spawn fireslime
        m_EnemiesOnFloor[0].transform.SetParent(spawnPoints[0].transform, false);
        m_EnemyScripts.Add(m_EnemiesOnFloor[0].GetComponent<EnemyScript>());
        m_EnemyScripts[0].m_id = 0;
        m_EnemyScripts[0].EnemyDeath += RemoveEnemy;
        m_EnemyScripts[0].addCooldown(100.0f);

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
        Debug.Log("Removing Enemy: " + id);
        m_EnemyScripts[id].EnemyDeath -= RemoveEnemy;
        m_EnemiesOnFloor[id].gameObject.SetActive(false);
        m_numberOfEnemies--;
        EnemyHasBeenSlain();
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

    public List<GameObject> getEnemies()
    {
        return m_EnemiesOnFloor;
    }
}
