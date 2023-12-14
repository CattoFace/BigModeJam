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
    public GameObject currQuestionPrefab;
    private GameObject currQuestionInstance;
    public bool fastMode;
    public string[] questionPrefabNames;
    private GameObject floor;
    public string ans1;
    public string ans2;
    public string ans3;
    public string ans4;
    public float levelTime;
    public GameState gameState;
    private bool isAlive=false;


    void Start()
    {
        questionPrefabNames = new string[] { "SpheresFallingPrefab", "ObjectsFallingPrefab", "SpheresLeftToRight" };
        floor = GameObject.Find("stageFloor");
        ans1 = "";
        ans2 = "";
        ans3 = "";
        ans4 = "";
        levelTime = 0;
    }
    //update is that function that updates every frame
    void Update()
    {
        levelTime+=Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.Space)) {
            summonOrDestroyPrefab(1);
        }
        if (800*Time.deltaTime< levelTime && levelTime < 900*Time.deltaTime)
        {
            Debug.Log(ans1 + " , " + ans2 + " , " + ans3 + " , " + ans4);
            levelTime = 0;
        }

    }
    public void startLevel(State state, float difficulty)
    {
        levelTime=0;
        updateButtonsText();
        currQuestionInstance = Instantiate(currQuestionPrefab, new Vector3(-10f, 0, 0), Quaternion.identity);
    }
    public void updateButtonsText()
    {
        button1.setStatus(true, Command.answer1, ans1);
        button2.setStatus(true, Command.answer2, ans2);
        button3.setStatus(true, Command.answer3, ans3);
        button4.setStatus(true, Command.answer4, ans4);
    }
    public void setAnswers(string deliveredAnswer1,string deliveredAnswer2, string deliveredAnswer3, string deliveredAnswer4)
    {
        ans1 = deliveredAnswer1;
        ans2 = deliveredAnswer2;
        ans3 = deliveredAnswer3;
        ans4 = deliveredAnswer4;
   
    }
    public float submitAnswer(int answer)
    {
        return 0;
    }
    public void summonOrDestroyPrefab(int prefabIndex)
    {
        if (!isAlive)
        {
            currQuestionPrefab = Resources.Load(questionPrefabNames[prefabIndex]) as GameObject;
            if (currQuestionPrefab == null)
            {
                Debug.Log("ERROR, QUITTING");
                Application.Quit();
            }
            startLevel(State.slowMode, 0);
            floor.SetActive(false);
            isAlive = true;
        }
        else
        {
            currQuestionPrefab = null;
            floor.SetActive(true);
            Destroy(currQuestionInstance);
            isAlive = false;
            ans1 = "";
            ans2 = "";
            ans3 = "";
            ans4 = "";
        }
    }
}

