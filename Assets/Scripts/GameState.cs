using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    public GameObject light4;

    void set_lights(bool to_set){
        light1.SetActive(to_set);
        light2.SetActive(to_set);
        light3.SetActive(to_set);
        light4.SetActive(to_set);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ButtonPressed(int buttonID){
        print(buttonID);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X))
    {
       set_lights(! light1.activeSelf);
    }
    }
}
