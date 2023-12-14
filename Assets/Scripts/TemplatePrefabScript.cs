using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplatePrefabScript : MonoBehaviour
{
    LevelManager lvlmgr;
    public string questionText="Placeholder Question";
    public string answer1="Answer 1";
    public string answer2="Answer 2";
    public string answer3="Answer 3";
    public string answer4="Answer 4";
    public int correctAnswer=1;
    public float correctValue=1;
    public float incorrectValue=-1;
    public float time = 5;
    // Start is called before the first frame update
    void Start()
    {
        lvlmgr = Camera.main.GetComponent<LevelManager>();
        lvlmgr.setQuestion(questionText,answer1,answer2, answer3, answer4, correctAnswer, correctValue, incorrectValue, time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
