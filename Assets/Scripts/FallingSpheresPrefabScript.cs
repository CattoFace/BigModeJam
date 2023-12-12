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
    public int relativeDifficulty;
    Rigidbody rb;
    Renderer sphereRenderer;
    public float maxVelocity = 18f;
    private Color winColor;
    private int winColorIndex;
    private Color[] colors;
    public LevelManager lvlmgr;
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
        //note: when working with integers, Random.Range is (minInclusive,maxExclusive)
        winColorIndex = Random.Range(0, 8);
        //if relDif=0, easiest. relDif=2, hardest (bigger relDef means less spheres of winning colors in relation
        //to avg amount of spheres of each color
        relativeDifficulty = Random.Range(0, 4);
        colors = new Color[] { Color.red, Color.green, Color.blue, Color.yellow, Color.black, Color.white, Color.cyan, new Color(0.5f, 0f, 0.5f) };
        winColor = colors[winColorIndex];
        Debug.Log(winColorIndex+" , Difficultiy: "+relativeDifficulty);
        lvlmgr = Camera.main.GetComponent<LevelManager>();
        counter = 0;
        SpawnSpheres();
    }
    void SpawnSpheres()
    {
        int j = 0;
        for (int i = 0; i < numSpheres; i++)
        {
            Vector3 spawnLocation = new Vector3(Random.Range(-13f, -7f), Random.Range(6f,12f), Random.Range(-4f, 4f));
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
            if (counter == 22+relativeDifficulty)
            {
                j++;
                counter = 0;
            }
        }
    }
}
