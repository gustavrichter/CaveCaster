using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(Camera.main.transform);
    }
}
