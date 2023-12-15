using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallForever : MonoBehaviour
{
    float startingHeight;
    void Start()
    {
     startingHeight =transform.position.y;
    }
    void Update()
    {
        if (transform.position.y<-2){
            transform.position = new Vector3(transform.position.x, startingHeight, transform.position.z);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
