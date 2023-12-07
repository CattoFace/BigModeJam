using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsFallingScript : MonoBehaviour
{
    public int numObjects;
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
    public Vector3 commonScale;

    
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
        commonScale = new Vector3(1.3f, 1.3f, 1.3f);
        counter = 0;
        numObjects = 155;
        SpawnObjects();
        Debug.Log(winObject.name);

    }

    public void SpawnObjects()
    {
        float difficulty = Random.Range(1f, 1.75f);
        GameObject tempObj;
        string objType;
        int j = 0;
        for (int i = 0; i < numObjects; i++)
        {
            if (j > 15)
            {
                j = winObjectIndex;
                counter = 100;
            }
            Vector3 spawnLocation = new Vector3(Random.Range(-13f, -7f), Random.Range(6f, 12f), Random.Range(-4f, 4f));
            tempObj = Instantiate(Objects[j], spawnLocation, Quaternion.identity);
            tempObj.transform.SetParent(transform);
            tempObj.AddComponent<Rigidbody>();
            rb = tempObj.GetComponent<Rigidbody>();
            rb.drag = Random.Range(0.5f, 0.7f);
            objectRenderer = tempObj.GetComponent<Renderer>();
            tempObj.transform.localScale *= Random.Range(1.2f, 1.4f);
            tempObj.AddComponent<FallingObject>();
            tempObj.layer = 7;
            objType = tempObj.name;
            //section unique for this question cuz of diff sizes of objs
            switch (j)
            {
                case 2:
                    tempObj.transform.localScale *= 1.5f;
                    break;
                case 3:
                    tempObj.transform.localScale *= 1.65f;
                    break;
                case 5:
                    tempObj.transform.localScale *= 1.5f;
                    break;
                case 8:
                    tempObj.transform.localScale *= 3f;
                    break;
                case 11:
                    tempObj.transform.localScale *= 1.5f;
                    break;
                case 12:
                    tempObj.transform.localScale *= 1.5f;
                    break;
                case 13:
                    tempObj.transform.localScale *= 3f;
                    break;
                case 14:
                    tempObj.transform.localScale *= 1.3f;
                    break;
                case 15:
                    tempObj.transform.localScale *= 0.8f;
                    break;
                default:
                    tempObj.transform.localScale = commonScale;
                    break;
            }
            //apply difficulty
            tempObj.transform.localScale *= difficulty;
            tempObj.GetComponent<FallingObject>().setType(objType);
            //end of section  
            ++counter;
            if (counter == 8)
            {
                j++;
                counter = 0;
            }
        }
        Debug.Log(numObjects);
        return;
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
