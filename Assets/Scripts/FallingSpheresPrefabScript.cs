using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpheresF : MonoBehaviour
{
    //numspheres need to be s.t. numSpheres MOD 8 = 2, 200 is a good choice
    public int numSpheres = 200;
    public float minSize = 0.1f;
    public float maxSize = 5f;
    public float scaleChangeAmount;
    Rigidbody rb;
    Renderer sphereRenderer;
    public float maxVelocity = 18f;
    private Color winColor;
    private int winColorIndex;
    private Color[] colors;
    private int counter;
    // Start is called before the first frame update
    void Start()
    {
        activateQ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void activateQ()
    {
        winColorIndex = Random.Range(0, 7);
        colors = new Color[] { Color.red, Color.blue, Color.green, Color.yellow, Color.black, Color.white, Color.cyan, new Color(0.5f, 0f, 0.5f) };
        winColor = colors[winColorIndex];
        Debug.Log(winColorIndex);
        counter = 0;
        SpawnSpheres();
    }
    void SpawnSpheres()
    {
        int j = 0;
        for (int i = 0; i < numSpheres; i++)
        {
            Vector3 spawnLocation = new Vector3(Random.Range(-14f, -6f), 5f, Random.Range(-4, 4));
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = spawnLocation;
            sphere.transform.SetParent(transform);
            sphere.AddComponent<Rigidbody>();
            rb = sphere.GetComponent<Rigidbody>();
            rb.drag = Random.Range(0.5f, 0.75f);
            sphereRenderer = sphere.GetComponent<Renderer>();
            //if j is under 7 means we're still spawning 7 spheres of each color(0-7 indices)
            if (j <= 7)
            {
                sphereRenderer.material.color = colors[j];
            }
            else
            {
                sphereRenderer.material.color = colors[winColorIndex];
            }
            sphere.transform.localScale *= Random.Range(0.8f, 1.212f);
            sphere.AddComponent<FallingBall>();
            sphere.layer = 7;
            counter++;
            if (counter == 23)
            {
                j++;
                counter = 0;
            }
        }
    }
}
