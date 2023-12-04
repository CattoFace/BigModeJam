using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public int score;

    public GameObject currQuestionPrefab;
    private GameObject toDest;

    void Start()
    {

    }
    //update is that function that updates every frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) {
            doIt();
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            Destroy(toDest);
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
