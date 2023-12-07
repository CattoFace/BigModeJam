using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsFallingScript : MonoBehaviour
{
    public int numObjects = 100;
    public float minSize = 0.1f;
    public float maxSize = 5f;
    public float scaleChangeAmount;
    public int relativeDifficulty;
    Rigidbody rb;
    Renderer objectRenderer;
    public float maxVelocity = 18f;
    private GameObject winObject;
    private int winObjectIndex;
    private GameObject[] Objects;
    private int counter;
    public GameObject bed;
    public GameObject hockeyTable;
    public GameObject box;
    public GameObject coffeM;
    public GameObject table1;
    public GameObject chair1;
    public GameObject fridge;
    public GameObject dresser;
    public GameObject toyCar;
    public GameObject sofa;
    public GameObject table2;
    public GameObject microwave;
    public GameObject chair2;
    public GameObject lamp;
    public GameObject dbRack;
    public GameObject washingM;

    
    // Start is called before the first frame update
    void Start()
    {
        activated();
    }

    public void activated()
    {
        Objects = new GameObject[] {bed, hockeyTable, box, coffeM, table1,
                             chair1, fridge, dresser, toyCar, sofa,
                             table2, microwave, chair2, lamp, dbRack,
                             washingM};
        relativeDifficulty = 0;
        winObjectIndex = Random.Range(0, 16);
        winObject = Objects[winObjectIndex];
        counter = 0;
        SpawnObjects();
    }

    public void SpawnObjects()
    {
        GameObject tempObj;
        string objType;
        int j = 0;
        for (int i = 0; i < numObjects; i++)
        {
            Vector3 spawnLocation = new Vector3(Random.Range(-13f, -7f), Random.Range(6f, 12f), Random.Range(-4f, 4f));
            tempObj = Instantiate(Objects[j], spawnLocation, Quaternion.identity);
            Debug.Log("j = " + j);
            tempObj.transform.SetParent(transform);
            tempObj.AddComponent<Rigidbody>();
            rb = tempObj.GetComponent<Rigidbody>();
            rb.drag = Random.Range(0.5f, 0.7f);
            objectRenderer = tempObj.GetComponent<Renderer>();
            tempObj.transform.localScale *= Random.Range(1.2f, 1.4f);
            tempObj.AddComponent<FallingObject>();
            tempObj.layer = 7;
            objType = tempObj.name;
            tempObj.GetComponent<FallingObject>().setType(objType);
            //if j is under 16 means we're still spawning 12 objs of each type(0-15 indices)
            if (j > 15)
            {
                j = winObjectIndex;
                counter = 100; // so it doesnt intrupt
            }
            ++counter;
            if (counter == 6)
            {
                j++;
                counter = 0;
            }
        }
        return;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
