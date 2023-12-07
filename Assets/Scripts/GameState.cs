using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum State{
    mainMenu,
    paused,
    fastMode,
    slowMode
}

public enum Command{
    answer1,
    answer2,
    answer3,
    answer4,
    pause,
    resume,
    startFast,
    startSlow,
    instructions,
    quit
}

public class GameState : MonoBehaviour
{
    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    public GameObject light4;
    public LevelManager levelManager;
    private State state = State.mainMenu;
    public TMP_Text screenText;
    public ButtonController button1;
    public ButtonController button2;
    public ButtonController button3;
    public ButtonController button4;
    public GameObject fastPanel;
    public GameObject slowPanel;
    public Slider timeLeftSlider;
    public TMP_Text scoreText;
    public TMP_Text timeSurvivedText;
    public float difficulty=0;
    public float difficultyIncreaseRate=1;
    public float score=0;
    public float timeAlive=0;
    void set_lights(bool toSet){
        light1.SetActive(toSet);
        light2.SetActive(toSet);
        light3.SetActive(toSet);
        light4.SetActive(toSet);
    }

    public void ButtonPressed(Command command){
        switch(command){
            case Command.answer1:
                handleAnswer(levelManager.submitAnswer(1));
                break;
            case Command.answer2:
                handleAnswer(levelManager.submitAnswer(2));
                break;
            case Command.answer3:
                handleAnswer(levelManager.submitAnswer(3));
                break;
            case Command.answer4:
                handleAnswer(levelManager.submitAnswer(4));
                break;
            case Command.pause:
                openPauseMenu();
                break;
            case Command.resume:
                resumeGame();
                break;
            case Command.startFast:
                startFastGamemode();
                break;
            case Command.startSlow:
                startSlowGamemode();
                break;
            case Command.instructions:
                showInstructions();
                break;
            case Command.quit:
                if(state==State.mainMenu){
                    #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
                    #else
                        Application.Quit();
                    #endif
                }else{
                    openMainMenu();
                }
                break;

        }
    }

    void Start(){
        openMainMenu();
    }

    public void openMainMenu(){
        set_lights(false);
        state = State.mainMenu;
        screenText.text = "Main Menu";
        button1.command = Command.startSlow;
        button1.textBox.text = "Normal Mode";
        button2.command = Command.startFast;
        button2.textBox.text = "Survival Mode";
        button3.command = Command.instructions;
        button3.textBox.text = "Instructions";
        button4.command = Command.quit;
        button4.textBox.text = "Quit";
        slowPanel.SetActive(false);
        fastPanel.SetActive(false);
    }

    void showInstructions(){
        //TODO
    }
    void startSlowGamemode(){
        state = State.slowMode;
        slowPanel.SetActive(true);
        score=1;
        difficulty=0.1f;
        //TODO
    }
    void startFastGamemode(){
        state = State.fastMode;
        fastPanel.SetActive(true);
        score=1;
        difficulty=0.1f;
        //TODO
    }
    void resumeGame(){
        //TODO
    }
    void openPauseMenu(){
        //TODO
    }
    void handleAnswer(float result){
        score += result;
        difficulty+=difficultyIncreaseRate;
        levelManager.summonPrefab(state, difficulty);
    }

    void Update()
    {
        if(state==State.fastMode){
            timeAlive+=Time.deltaTime;
            timeSurvivedText.text = timeAlive.ToString("n2")+"s";
            score-=difficulty*Time.deltaTime;;
            timeLeftSlider.value = score;
        }
    }
}
