using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public int score;
    public bool isAlive = false;
    public GameObject currQuestionPrefab;
    private GameObject toDest;
    public bool fastMode;
    public string[] questionPrefabNames;
    private GameObject floor;
    void Start()
    {
        questionPrefabNames = new string[] { "SpheresFallingPrefab" };
        floor = GameObject.Find("stageFloor");
    }
    //update is that function that updates every frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) {
            if (!isAlive){
                currQuestionPrefab = Resources.Load(questionPrefabNames[0]) as GameObject;
                if(currQuestionPrefab == null) {
                    Debug.Log("NO");
                }
                StartLevel(State.slowMode, 0);
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
    public void StartLevel(State state, float difficulty)
    {
        toDest = Instantiate(currQuestionPrefab, new Vector3(-10f, 0, 0), Quaternion.identity);
        toDest.transform.SetParent(transform);

    }
    public float submitAnswer(int answer)
    {
        return 0;
    }
}
