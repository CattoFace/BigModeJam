using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public int score;
    public bool isAlive = false;
    public GameObject currQuestionPrefab;
    private GameObject toDest;

    void Start()
    {

    }
    //update is that function that updates every frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) {
            if (!isAlive){
                doIt();
                isAlive = true;
            }
            else
            {
                Destroy(toDest);
                isAlive = false;    
            }
        }
    }
    void doIt()
    {
        toDest = Instantiate(currQuestionPrefab, new Vector3(-10f, 0, 0), Quaternion.identity);
    }
    public float submitAnswer(int answer)
    {
        return 0;
    }
}
