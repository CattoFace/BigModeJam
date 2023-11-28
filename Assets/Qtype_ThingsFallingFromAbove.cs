using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingsFalling : MonoBehaviour
{
    public GameObject sphere;
    public int numSpheres = 10;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -1f)
        {
            Vector3 newPos = transform.position;
            newPos.y = 5f;
            newPos.z = Random.Range(-4f, 4f);
            newPos.x = Random.Range(-14f, -6f);
            transform.position = newPos;
        }
    }
}
