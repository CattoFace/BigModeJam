using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{

    public RigidBody rb;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponenet<RigidBody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
