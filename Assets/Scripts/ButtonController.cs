using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum animationState{
    up,
    gone,
    pressingDown,
    pressingUp,
    disappearingBody,
    disappearingCase,
    appearingBody,
    appearingCase
}
public class ButtonController : MonoBehaviour
{
    private animationState state = animationState.gone;
    private float animationTime=0;
    private Vector3 currentOffset = new Vector3(0,0,0);
    public int buttonID;
    public GameState gameState;
    public GameObject buttonCase;
    public GameObject buttonBody;
    public Vector3 caseUp  = new Vector3(0,0.1f,0);
    public Vector3 bodyDown = new Vector3(0,0.2f,0);
    public Vector3 bodyUp = new Vector3(0,0.4f,0);
    public float downDuration = 0.5f;
    public float upDuration = 0.5f;
    public float caseDisappearingDuration = 1;
    public float bodyDisappearingDuration = 1;
    public float caseAppearingDuration = 1;
    public float bodyAppearingDuration = 1;

    // Start is called before the first frame update
    void Start()
    {
        state = animationState.appearingCase;
    }

    bool LerpComponent(GameObject target, Vector3 start, Vector3 end, float duration){
        animationTime+=Time.deltaTime;
        float ratio = Mathf.SmoothStep(0,1,animationTime/duration);
        target.transform.localPosition = Vector3.Lerp(start, end, ratio);
        return ratio>=0.99;
    }

    void OnMouseOver(){
        print("test");
        if(Input.GetMouseButtonDown(0)){
            click();
        }
    }

    // click the button
    public void click(){

            state = animationState.pressingDown;
            gameState.ButtonPressed(buttonID);
            animationTime=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(""+buttonID)){
            click();
        }
        bool done;
        switch(state){
            case animationState.appearingCase:
                done = LerpComponent(buttonCase, Vector3.zero, caseUp, caseAppearingDuration);
                if(done){
                    state=animationState.appearingBody;
                    animationTime=0;
                }
                break;
            case animationState.appearingBody:
                done = LerpComponent(buttonBody, Vector3.zero, bodyUp, bodyAppearingDuration);
                if(done){
                    state=animationState.up;
                    animationTime=0;
                }
                break;
            case animationState.disappearingCase:
                done = LerpComponent(buttonCase, caseUp, Vector3.zero, caseDisappearingDuration);
                if(done){
                    state=animationState.gone;
                    animationTime=0;
                }
                break;
            case animationState.disappearingBody:
                done = LerpComponent(buttonBody, bodyDown, Vector3.zero, bodyDisappearingDuration);
                if(done){
                    state=animationState.disappearingCase;
                    animationTime=0;
                }
                break;
            case animationState.pressingDown:
                done = LerpComponent(buttonBody, bodyUp, bodyDown, downDuration);
                if(done){
                    state=animationState.pressingUp;
                    animationTime=0;
                }
                break;
            case animationState.pressingUp:
                done = LerpComponent(buttonBody, bodyDown, bodyUp, upDuration);
                if(done){
                    state=animationState.up;
                    animationTime=0;
                }
                break;
            case animationState.up:
                break;
        }
    }
}
