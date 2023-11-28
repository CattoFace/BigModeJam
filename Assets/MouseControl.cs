using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public float sensitivity = 1000;
    Vector3 curr_rotation = new Vector3(0,-90,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = -sensitivity* Input.GetAxis("Mouse Y");
        float horizontal = sensitivity* Input.GetAxis("Mouse X");
        curr_rotation[0]+=vertical;
        curr_rotation[1]+=horizontal;
        curr_rotation[0] = Mathf.Clamp(curr_rotation[0],-90,90);
        curr_rotation[1] = Mathf.Clamp(curr_rotation[1],-180,0);
        transform.eulerAngles = curr_rotation;
    }
}
