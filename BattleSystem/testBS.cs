using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBS : MonoBehaviour
{
    public GameObject cylinder;
    public float speed;
    float maxScale = 70;
    void Update(){
        
        //activating 
        if(cylinder.transform.localScale.x < maxScale && cylinder.transform.localScale.z < maxScale){

            cylinder.transform.localScale += new Vector3(1 ,0 ,1) * speed * Time.deltaTime;
        }
        else{
            return;
        }

    }
}
