using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private CaveScript m_caveScript;
    [SerializeField]
    private Transform playerTransform;

    private void Start()
    {
        m_caveScript = GameObject.FindGameObjectWithTag("Cave").transform.GetComponent<CaveScript>();
        if (!m_caveScript)
        {
            Debug.Log(gameObject.name + " cavescript not found");
        }
    }
    IEnumerator MoveRightAni()
    {
        for(int i = 0; i<25; i++) 
        { 
            yield return new WaitForSeconds(0.1f);
            transform.Translate(0f, 0f, 4f);
        }
        for(int j = 1; j <= 90; j++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Rotate(Vector3.up, 1f);
        }
        LetCaveSpawnEnemies();
        //transform.localRotation = Quaternion.Euler(0f, j, 0f);
    }
    IEnumerator MoveLeftAni()
    {
        for (int i = 0; i < 25; i++)
        {
            yield return new WaitForSeconds(0.1f);
            transform.Translate(0f, 0f, 4f);
        }
        for (float j = 1; j <= 90; j++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Rotate(Vector3.up, -1f);
        }
        LetCaveSpawnEnemies();

        //transform.localRotation = Quaternion.Euler(0f, j, 0f);
    }
    IEnumerator MoveForwardAni()
    {
        for (int i = 0; i < 25; i++)
        {
            yield return new WaitForSeconds(0.1f);
            transform.Translate(0f, 0f, 4f);
        }
        LetCaveSpawnEnemies();

        //transform.localRotation = Quaternion.Euler(0f, j, 0f);
    }
    public void MoveRight()
    {
        //transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
        StartCoroutine(MoveRightAni());
        StopCoroutine(MoveRightAni());
    }
    public void MoveForward()
    {
        //transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
        StartCoroutine(MoveForwardAni());
        StopCoroutine(MoveForwardAni());
    }



    public void MoveLeft()
    {
        StartCoroutine(MoveLeftAni());
        StopCoroutine(MoveLeftAni());
    }

    private void LetCaveSpawnEnemies()
    {
        if(UnityEngine.Random.Range(1,3) == 1)
        {
            if (checkIfOnStraightLine())
            {
                m_caveScript.SpawnEnemy(UnityEngine.Random.Range(2, 3));
            }
            else
            {
                m_caveScript.SpawnEnemy(1);
            }
        }
    }

    private bool checkIfOnStraightLine()
    {
        float angle =Math.Abs(playerTransform.rotation.eulerAngles.y);// * 180.0f / MathF.PI;
        Debug.Log("spawn enemies, angle: " + angle);

        if (angle > 359.0f && angle < 1.0f)
        {
            return true;
        }
        else if(angle > 179.0f && angle < 181.0f)
        {
            return true;
        }
        else return false;
    }


}
