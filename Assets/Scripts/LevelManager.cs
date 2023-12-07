using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelManager : MonoBehaviour
{
    public TMP_Text screenText;
    public ButtonController button1;
    public ButtonController button2;
    public ButtonController button3;
    public ButtonController button4;
    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    public GameObject light4;
    public GameObject fastPanel;
    public GameObject slowPanel;
    public int score;
    public bool isAlive = false;
    public GameObject currQuestionPrefab;
    private GameObject toDest;
    public bool fastMode;
    public string[] questionPrefabNames;
    private GameObject floor;

    void Start()
    {
        questionPrefabNames = new string[] { "SpheresFallingPrefab","ObjectsFallingPrefab","SpheresLeftToRight" };
        floor = GameObject.Find("stageFloor");
    }
    //update is that function that updates every frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) {
            if (!isAlive){
                currQuestionPrefab = Resources.Load(questionPrefabNames[1]) as GameObject;
                if(currQuestionPrefab == null) {
                    Debug.Log("NO");
                }
                summonPrefab(State.slowMode, 0);
                floor.SetActive(false);
                isAlive = true;
            }
            else
            {
                currQuestionPrefab = null;
                floor.SetActive(true);
                Destroy(toDest);
                isAlive = false;    
            }
        }
    }
    public void summonPrefab(State state, float difficulty)
    {
        toDest = Instantiate(currQuestionPrefab, new Vector3(-10f, 0, 0), Quaternion.identity);

    }
    public void activateGame(bool state) //state=false is slow mode, state=true is fast mode
    {
        //TODO
    }
    public float submitAnswer(int answer)
    {
        return 0;
    }
}

