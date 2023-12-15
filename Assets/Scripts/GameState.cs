using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum State{
    mainMenu,
    paused,
    fastMode,
    slowMode,
    instructions,
    gameOver
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
    quit,
    none
}

public class GameState : MonoBehaviour
{
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
    public Slider livesLeftSlider;
    public TMP_Text levelText;
    public TMP_Text timeSurvivedText;
    public TMP_Text questionTimeLeftValue;
    public TMP_Text questionTimeLeftText;
    public int level=0;
    public float difficulty=0;
    public float difficultyIncreaseRate=0;
    float health=0;
    float timeAlive=0;
    bool paused = false;
    string button1BackupText;
    string button2BackupText;
    bool button3BackupStatus;
    bool button4BackupStatus;
    bool lightsStateBackup;
    public float questionTimeLeft;
    public RectTransform screen;
    public GameObject separator;

    public void setLights(bool toSet){
        separator.SetActive(!toSet);
    }

    public void ButtonPressed(Command command){
        switch(command){
            case Command.answer1:
                if(!paused){
                    handleAnswer(levelManager.submitAnswer(1));
                }
                break;
            case Command.answer2:
                if(!paused){
                    handleAnswer(levelManager.submitAnswer(2));
                }
                break;
            case Command.answer3:
                if(!paused){
                    handleAnswer(levelManager.submitAnswer(3));
                }
                break;
            case Command.answer4:
                if(!paused){
                    handleAnswer(levelManager.submitAnswer(4));
                }
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
            case Command.none:
                break;
        }
    }

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        openMainMenu();
    }

    public void openMainMenu(){
        paused=false;
        levelManager.stopLevel();
        screen.sizeDelta = new Vector2(0.32f, 0.188f);
        setLights(false);
        state = State.mainMenu;
        screenText.text = "Find The Big Mode";
        button1.setStatus(true, Command.startSlow, "Normal Mode");
        button2.setStatus(true, Command.startFast, "Survival Mode");
        button3.setStatus(true, Command.instructions, "Instructions");
        button4.setStatus(true, Command.quit, "Quit");
        slowPanel.SetActive(false);
        fastPanel.SetActive(false);
    }

    void showGameOver(bool fast){
        state = State.gameOver;
        levelManager.stopLevel();
        setLights(false);
        if(fast){
            fastPanel.SetActive(false);
            screenText.text = "Game Over!\nYou survived "+timeAlive.ToString("n2")+"s";
        }else{
            slowPanel.SetActive(false);
            screenText.text = "Game Over!\nYou reached level "+level.ToString();
        }
        button1.setStatus(true, Command.quit, "Main Menu");
        button2.turnOff();
        button3.turnOff();
        button4.turnOff();
    }
    void showInstructions(){
        screen.sizeDelta = new Vector2(0.32f, 0.3f);
        state = State.instructions;
        screenText.text = "<size=40%>In each question you have to select the mode among the items you see.\nIn normal mode, you will be given a set amount of time for each question before the lights go out and you can answer.\nIn Survival Mode, your time will count down and you will be given more when you answer questions correctly.\n\nMode = The most common item";
        button1.turnOff(true);
        button2.turnOff(true);
        button3.turnOff();
        button4.setStatus(true, Command.quit, "Back");

    }
    public void setQuestion(string questionText, string ans1, string ans2, string ans3, string ans4){
        screenText.enabled = state==State.fastMode;
        screenText.text = "<size=80%>"+questionText;
        button1.setStatus(state==State.fastMode && !ans1.Equals(""), Command.answer1, ans1);
        button2.setStatus(state==State.fastMode && !ans2.Equals(""), Command.answer2, ans2);
        button3.setStatus(state==State.fastMode && !ans3.Equals(""), Command.answer3, ans3);
        button4.setStatus(state==State.fastMode && !ans4.Equals(""), Command.answer4, ans4);
    }
    public void showSlowQuestion(){
        setLights(false);
        screenText.enabled = true;
        button1.turnOn();
        button2.turnOn();
        button3.turnOn();
        button4.turnOn();
    }
    void startSlowGamemode(){
        state = State.slowMode;
        slowPanel.SetActive(true);
        level=1;
        health=3;
        levelText.text= level.ToString();
        livesLeftSlider.value = health;
        difficulty=1;
        difficultyIncreaseRate = 1.05f;
        levelManager.startLevel(state, difficulty);


    }
    void startFastGamemode(){
        state = State.fastMode;
        fastPanel.SetActive(true);
        level=1;
        health=1;
        difficulty=(1.0f/30.0f);
        difficultyIncreaseRate = 1.05f;
        setLights(true);
        levelManager.startLevel(state, difficulty);
    }
    void resumeGame(){
        paused = false;
        setLights(lightsStateBackup);
        button1.command = Command.answer1;
        button1.textBox.text = button1BackupText;
        button2.command = Command.answer2;
        button2.textBox.text = button2BackupText;
        if(button3BackupStatus){
            button3.turnOn();
        }
        if(button4BackupStatus){
            button4.turnOn();
        }
    }
    void openPauseMenu(){
        paused = true;
        lightsStateBackup = separator.activeSelf;
        button1BackupText = button1.textBox.text;
        button2BackupText = button2.textBox.text;
        button3BackupStatus = button3.command!=Command.none;
        button4BackupStatus = button4.command!=Command.none;
        setLights(false);
        button1.setStatus(true, Command.resume, "Resume");
        button2.setStatus(true, Command.quit, "Main Menu");
        button3.turnOff();        
        button4.turnOff();
    }
    void handleAnswer(float result){
        level+=1;
        if(state==State.slowMode){
            if(result<0){
                health-=1;
            }
            health = Mathf.Min(health,3);
            levelText.text= level.ToString();
            livesLeftSlider.value = health;
            if(health<=0){
                level-=1; // since we raised it by 1 earlier but lost the game
                showGameOver(false);
                return;
            }
        }else{
            health += result;
            health = Mathf.Min(health, 1);
        }
        difficulty*=difficultyIncreaseRate;
        levelManager.startLevel(state, difficulty);
    }

    void Update()
    {
        if(!paused){
            if(state==State.fastMode){
                timeAlive+=Time.deltaTime;
                timeSurvivedText.text = timeAlive.ToString("n2")+"s";
                health-=difficulty*Time.deltaTime;;
                timeLeftSlider.value = health;
                if(health<=0){
                    showGameOver(true);
                }
            }else if(state==State.slowMode){
                questionTimeLeftValue.enabled = questionTimeLeft>=0;
                questionTimeLeftText.enabled = questionTimeLeft>=0;
                questionTimeLeftValue.text = questionTimeLeft.ToString("n1")+"s";
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape) && state!=State.mainMenu && state!=State.instructions && state!=State.gameOver){
            if(paused){
                resumeGame();
            }else{
                openPauseMenu();
            }
        }
    }
}
