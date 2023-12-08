using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsFallingScript : MonoBehaviour
{
    public int numObjects;
    public float minSize = 0.1f;
    public float maxSize = 5f;
    public float scaleChangeAmount;
    public float  relativeDifficulty;
    Rigidbody rb;
    Renderer objectRenderer;
    public float maxVelocity = 18f;
    private GameObject winObject;
    private int winObjectIndex;
    private GameObject[] Objects;
    private int counter;
    public GameObject Bed;
    public GameObject hockeyTable;
    public GameObject box;
    public GameObject coffeM;
    public GameObject brownTable;
    public GameObject greenChair;
    public GameObject fridge;
    public GameObject dresser;
    public GameObject toyCar;
    public GameObject sofa;
    public GameObject grayTable;
    public GameObject microwave;
    public GameObject brownChair;
    public GameObject lamp;
    public GameObject plateRack;
    public GameObject washingMachine;
    public Vector3 commonScale;
    public int spawnDifficulty;
    private string[] answers;

    
    // Start is called before the first frame update
    void Start()
    {
        activated();
    }

    public void activated()
    {
        Objects = new GameObject[] {Bed, hockeyTable, box, coffeM, brownTable,
                             greenChair, fridge, dresser, toyCar, sofa,
                             grayTable, microwave, brownChair, lamp, plateRack,
                             washingMachine};
        relativeDifficulty = Random.Range(1.75f,2.9f);
        spawnDifficulty = Random.Range(0, 3);
        winObjectIndex = Random.Range(0, 16);
        winObject = Objects[winObjectIndex];
        commonScale = new Vector3(1.3f, 1.3f, 1.3f);
        if(winObjectIndex == 5 || winObjectIndex == 12) //case chair
        {
            if (nameof(winObject) == "greenChair")
            {
                answers = new string[] { "Green Chair", "Brown Chair" };
            }
            
        }
        answers = new string[] {nameof(winObject),nameof(Bed) };
        counter = 0;
        numObjects = 156;
        SpawnObjects();
        Debug.Log("winner: " + winObject.name + " , num objects: " + numObjects +
                ", difficulty: " + spawnDifficulty);
    }

    public void SpawnObjects()
    {
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
                    tempObj.transform.localScale *= 1.235f;
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
                    tempObj.transform.localScale *= 1.2f;
                    break;
                case 13:
                    tempObj.transform.localScale *= 2.9f;
                    break;
                case 14:
                    tempObj.transform.localScale *= 1.2f;
                    break;
                case 15:
                    tempObj.transform.localScale *= 0.8f;
                    break;
                default:
                    tempObj.transform.localScale = commonScale;
                    break;
            }
            if (j == winObjectIndex && j != 0 && j != 1 && j != 4 && j != 6 && j != 9 && j != 10 && j != 15)
            {
                tempObj.transform.localScale *= (3.65f - relativeDifficulty);
            }
            else
            {
                tempObj.transform.localScale *= 1.25f;
            }

            tempObj.GetComponent<FallingObject>().setType(objType);
            //end of section  
            ++counter;
            if (counter == 8 + spawnDifficulty)
            {
                j++;
                counter = 0;
            }
        }
        return;
    }

    public string[] getAnswersForButtons()
    {
        return null;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
