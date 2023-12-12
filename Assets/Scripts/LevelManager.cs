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
    public string[] answers;
    public float levelTime;
    public GameState gameState;
    public bool isAlive=false;


    void Start()
    {
        questionPrefabNames = new string[] { "SpheresFallingPrefab", "ObjectsFallingPrefab", "SpheresLeftToRight" };
        floor = GameObject.Find("stageFloor");
        answers = new string[4];
        for (int i = 0; i < 4; i++)
        {
            answers[i] = "";
        }
        levelTime = 0;
    }
    //update is that function that updates every frame
    void Update()
    {
        levelTime+=Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.Space)) {
            if (!isAlive){
                currQuestionPrefab = Resources.Load(questionPrefabNames[0]) as GameObject;
                if(currQuestionPrefab == null) {
                    Debug.Log("NO");
                }
                startLevel(State.slowMode, 0);
                floor.SetActive(false);
                isAlive = true;
                Debug.Log(answers[0] + " , " + answers[1] + " , " + answers[2] + " , " + answers[3]);

            }
            else
            {
                currQuestionPrefab = null;
                floor.SetActive(true);
                Destroy(currQuestionInstance);
                isAlive = false;
            }
        }
        if (levelTime == 5)
        {
            Debug.Log(answers[0] + " , " + answers[1] + " , " + answers[2] + " , " + answers[3]);
            // updateButtonsText();
            ++levelTime;
        }
        else if (levelTime == 661)
        {
            levelTime = 1;
        }
        else
        {
            levelTime++;
        }
    }
    public void startLevel(State state, float difficulty)
    {
        levelTime=0;
        currQuestionInstance = Instantiate(currQuestionPrefab, new Vector3(-10f, 0, 0), Quaternion.identity);
    }
    public void updateButtonsText()
    {
        button1.setStatus(answers[0]==null, Command.answer1, answers[0]);
        button2.setStatus(answers[1]==null, Command.answer2, answers[1]);
        button3.setStatus(answers[2]==null, Command.answer3, answers[2]);
        button4.setStatus(answers[3]==null, Command.answer4, answers[3]);
    }
    public void setAnswers(string[] ans)
    {
        answers = ans;
   
    }
    public string[] getAnswers()
    {
        return answers;
    }
    public float submitAnswer(int answer)
    {
        return 0;
    }
}

