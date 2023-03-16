using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float time = 0;
    void Update()
    {
        time += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            gameObject.transform.position += new Vector3(0f, 0f, 1f);
            Debug.Log(time);
            time = 0;
        }
    }
}
