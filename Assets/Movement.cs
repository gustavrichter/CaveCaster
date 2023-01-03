using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public void MoveLeft()
    {
        Camera.main.transform.Translate(-1, 0, 0);

    }
    public void MoveRight()
    {
        Camera.main.transform.Translate(1, 0, 0);
    }
    public void MoveForward()
    {
         Camera.main.transform.Translate(0, 0, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
