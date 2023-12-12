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
    public string ans1;
    public string ans2;
    public string ans3;
    public string ans4;
    private int counter;
    // Start is called before the first frame update
    void Start()
    {
        activateQ();
        
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
        ans1 = "";
        ans2 = "";
        ans3 = "";
        ans4 = "";
        setAnswers();
        counter = 0;
        SpawnSpheres();
    }
    void SpawnSpheres()
    {
        lvlmgr.setAnswers(ans1,ans2,ans3,ans4);
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
    int randomMinMaxExcl(int min, int max, int excl1, int excl2, int excl3)
    {
        int res;
        res = Random.Range(min, max);
        while (res == excl1 || res == excl2 || res == excl3)
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

    public void setAnswers()
    {
        int tempAns1, tempAns2, tempAns3;
        tempAns1 = randomMinMaxExcl(0, 8, winColorIndex, -1, -1);
        tempAns2 = randomMinMaxExcl(0, 8, winColorIndex, tempAns1, -1);
        tempAns3 = randomMinMaxExcl(0, 8, winColorIndex, tempAns1, tempAns2);
        ans1 = getName(winColorIndex);
        ans2 = getName(tempAns1);
        ans3 = getName(tempAns2);
        ans4 = getName(tempAns3);
    }

    public string getName(int colorIndex)
    {
        switch (colorIndex)
        {
            case 0:
                return "Red";
            case 1:
                return "Green";
            case 2:
                return "Blue";
            case 3:
                return "Yellow";
            case 4:
                return "Black";
            case 5:
                return "White";
            case 6:
                return "Cyan";
            case 7:
                return "Purple";
            default:
                break;
        }
        return "ERROR";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
