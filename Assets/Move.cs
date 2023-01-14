using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
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

        //transform.localRotation = Quaternion.Euler(0f, j, 0f);
    }
    IEnumerator MoveForwardAni()
    {
        for (int i = 0; i < 25; i++)
        {
            yield return new WaitForSeconds(0.1f);
            transform.Translate(0f, 0f, 4f);
        }
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


}
