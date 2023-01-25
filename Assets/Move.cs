using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private CaveScript m_caveScript;
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private float playerRotationY;

    private void Start()
    {
        m_caveScript = GameObject.FindGameObjectWithTag("Cave").transform.GetComponent<CaveScript>();
        if (!m_caveScript)
        {
            Debug.Log(gameObject.name + " cavescript not found");
        }

    }
    private void Update()
    {
        playerRotationY = Math.Abs(playerTransform.rotation.eulerAngles.y);
    }
    IEnumerator MoveRightAni()
    {
        for(int i = 0; i<25; i++) 
        { 
            yield return new WaitForSeconds(0.1f);
            transform.Translate(0f, 0f, 4f);
        }
        for (int j = 1; j <= 90; j++)
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
        AkSoundEngine.PostEvent("Player_walk", gameObject);
        StartCoroutine(MoveRightAni());
        StopCoroutine(MoveRightAni());
    }
    public void MoveForward()
    {
        AkSoundEngine.PostEvent("Player_walk", gameObject);
        //transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
        StartCoroutine(MoveForwardAni());
        StopCoroutine(MoveForwardAni());
    }



    public void MoveLeft()
    {
        AkSoundEngine.PostEvent("Player_walk", gameObject);
        StartCoroutine(MoveLeftAni());
        StopCoroutine(MoveLeftAni());
    }

  //MODIFIED BY GUSTAV[
    private void LetCaveSpawnEnemies()
    {
        int compareInt = UnityEngine.Random.Range(0, 10);
        //Debug.Log("compareInt = " + compareInt);
        if (compareInt < 9 )
        {
            if (checkIfOnStraightLine())
            {
                m_caveScript.SpawnEnemy(UnityEngine.Random.Range(2, 4));
            }
            else //when we're not oriented parallel to the y axis we can't use raycasts and have to hard code one enemy to be able to do combat
            {
                m_caveScript.SpawnEnemy(1);
            }
        }
    }
    
    private bool checkIfOnStraightLine()
    {
        Debug.Log("spawn enemies, angle: " + playerRotationY);

        if (playerRotationY > 359.0f && playerRotationY < 1.0f)
        {
            return true;
        }
        else if (playerRotationY > 179.0f && playerRotationY < 181.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
 //]

}
