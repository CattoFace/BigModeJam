using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpheresFallingPrefab : MonoBehaviour
{
    public int numSpheres = 40;
    public float minSize = 0.1f;
    public float maxSize = 5f;
    public float scaleChangeAmount;
    Rigidbody rb;
    public float maxVelocity = 12f;
    public string winningColor;
    //public GameObject spherePrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SpawnSpheres();
        }   
    }
    
    void SpawnSpheres()
    {
        for(int i=0; i<numSpheres; i++)
        {
            Vector3 spawnLocation = new Vector3(Random.Range(-14f, -6f), 5f, Random.Range(-4, 4));
            //GameObject sphere = Instantiate(spherePrefab, spawnLocation, Quaternion.identity);
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = spawnLocation;
            sphere.AddComponent<Rigidbody>();
            rb = sphere.GetComponent<Rigidbody>();
            rb.drag = Random.Range(0.3f, 0.75f);
            sphere.transform.localScale *= Random.Range(1.1f, 1.5f);
            sphere.AddComponent<FallingBall>();
            sphere.layer = 7;   

        }
    }
}
