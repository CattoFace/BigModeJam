using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelManager : MonoBehaviour
{
    GameObject currQuestionInstance;
    bool fastMode;
    public string[] prefabNames = {};
    List<GameObject> prefabs = new List<GameObject>();
    int correctAnswer;
    float correctValue;
    float incorrectValue;
    float levelTime;
    bool levelActive;
    public GameState gameState;
    float diff=1;
    void Start()
    {
        foreach(string prefabName in prefabNames){
            prefabs.Add(Resources.Load(prefabName) as GameObject);
        }

    }

    void Update()
    {
        if(levelActive && !fastMode){
            levelTime-=Time.deltaTime;
            gameState.questionTimeLeft = levelTime;
            if(levelTime<=0){
                gameState.showSlowQuestion();
            }
        }
    }

    private GameObject selectPrefab(){
        return prefabs[Random.Range(0, prefabs.Count)];
    }

    public void startLevel(State state, float difficulty)
    {
        diff = difficulty;
        levelActive = true;
        if(state==State.fastMode){
            fastMode=true;
        }else{
            fastMode=false;
        }
        currQuestionInstance = Instantiate(selectPrefab(), Vector3.zero, Quaternion.identity);
    }
    public void setQuestion(string questionText, string ans1,string ans2, string ans3, string ans4, int correct, float cVal, float iVal, float time)
    {
        correctAnswer = correct;
        correctValue = cVal;
        incorrectValue = iVal;
        levelTime = time/diff;
        gameState.setQuestion(questionText, ans1,ans2,ans3,ans4);
        gameState.setLights(true);
   
    }
    public void stopLevel(){
        gameState.setLights(false);
        Destroy(currQuestionInstance);
        levelActive = false;
    }
    public float submitAnswer(int answer)
    {
        stopLevel();
        if(answer==correctAnswer){
            return correctValue;
        }else{
            return incorrectValue;
        }
    }
}

