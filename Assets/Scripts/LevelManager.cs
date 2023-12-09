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
    private GameObject currQuestionInstance;
    public bool fastMode;
    public string[] questionPrefabNames;
    private GameObject floor;
    public string[] answers;
    public int timer;


    void Start()
    {
        questionPrefabNames = new string[] { "SpheresFallingPrefab", "ObjectsFallingPrefab", "SpheresLeftToRight" };
        floor = GameObject.Find("stageFloor");
        answers = new string[4];
        for (int i = 0; i < 4; i++)
        {
            answers[i] = "";
        }
        timer = 0;
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
                Debug.Log(answers[0] + " , " + answers[1] + " , " + answers[2] + " , " + answers[3]);

            }
            else
            {
                currQuestionPrefab = null;
                floor.SetActive(true);
                Destroy(currQuestionInstance);
                isAlive = false;
                /*for(int i = 0; i < 4; i++)
                {
                    answers[i] = "get necked ";
                }*/
            }
        }
        if (timer == 5)
        {
            Debug.Log(answers[0] + " , " + answers[1] + " , " + answers[2] + " , " + answers[3]);
            timer++;
        }
        else if (timer == 460)
        {
            timer = 0;
        }
        else
        {
            timer++;
        }
    }
    public void summonPrefab(State state, float difficulty)
    {
        currQuestionInstance = Instantiate(currQuestionPrefab, new Vector3(-10f, 0, 0), Quaternion.identity);
    }
    public void updateButtonsText()
    {
        return;
    }
    public void setAnswers(string[] ans)
    {
        answers = ans;
    }
    public string[] getAnswers()
    {
        return answers;
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

