using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsFallingScript : MonoBehaviour
{
    public int numObjects;
    public float minSize = 0.1f;
    public float maxSize = 5f;
    public float scaleChangeAmount;
    public float relativeDifficulty;
    Rigidbody rb;
    public LevelManager lvlmgr;
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
    public string[] answers;


    // Start is called before the first frame update
    void Start()
    {
        activated();
        answers = new string[4];
    }

    public void activated()
    {
        Objects = new GameObject[] {Bed, hockeyTable, box, coffeM, brownTable,
                             greenChair, fridge, dresser, toyCar, sofa,
                             grayTable, microwave, brownChair, lamp, plateRack,
                             washingMachine};
        relativeDifficulty = Random.Range(1.75f, 2.9f);
        spawnDifficulty = Random.Range(0, 3);
        winObjectIndex = Random.Range(0, 16);
        winObject = Objects[winObjectIndex];
        lvlmgr = Camera.main.GetComponent<LevelManager>();
        commonScale = new Vector3(1.3f, 1.3f, 1.3f);
        counter = 0;
        answers=lvlmgr.getAnswers();
        setAnswersForButtons();
        numObjects = 156;
        SpawnObjects();
        lvlmgr.setAnswers(answers);
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

    int randomMinMaxExcl(int min, int max, int excl1,int excl2,int excl3)
    {
        int res;
        res = Random.Range(min, max);
        while(res == excl1 || res==excl2 || res == excl3)
        {
            res = Random.Range(min, max);
        }
        return res;
    }
    float randomMinMaxExcl(float min, float max, float excl1, float excl2, float excl3)
    {
        float res;
        res = Random.Range(min, max);
        while (res == excl1 || res == excl2 || res == excl3)
        {
            res = Random.Range(min, max);
        }
        return res;
    }

    public void setAnswersForButtons()
    { 
        int ans1, ans2, ans3;
        ans1 = randomMinMaxExcl(0, 16, winObjectIndex, -1, -1);
        ans2 = randomMinMaxExcl(0, 16, winObjectIndex, ans1, -1);
        ans3 = randomMinMaxExcl(0, 16, winObjectIndex, ans1, ans2);
        answers[0] = getName(Objects[winObjectIndex]);
        answers[1] = getName(Objects[ans1]);
        answers[2] = getName(Objects[ans2]);
        answers[3] = getName(Objects[ans3]);
    }

    string getName(GameObject obj)
    {
        switch (obj.name)
        {
            case "air_hockey_001":
                return "Air Hockey Table";
            case "bed_001":
                return "Bed";
            case "box_001":
                return "Box";
            case "coffee_machine_001":
                return "Coffee Machine";
            case "coffee_table_001":
                return "Brown Table";
            case "fridge_001":
                return "Fridge";
            case "dresser_001":
                return "Dresser";
            case "kitchen_chair_001":
                return "Green Chair";
            case "lamp_001":
                return "Lamp";
            case "lounge_chair_001":
                return "Brown Chair";
            case "microwave_oven_001":
                return "Microwave";
            case "office_table_001":
                return "Gray Table";
            case "sofa_001":
                return "Sofa";
            case "toy_001":
                return "Toy Car";
            case "training_item_002":
                return "Plate Rack";
            case "washing_machine_001":
                return "Washing Machine";
            default:
                return "";
        }
        return "";
    }
    // Update is called once per frame
    void Update()
    {

    }
}
