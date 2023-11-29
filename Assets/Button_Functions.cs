using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Functions : MonoBehaviour
{
    public float moveDuration = 1.0f; // Duration of the animation in seconds
    private float upY; // Up Y position
    private float downY; // Down Y position
    private float upX;
    private float downX;
    private float stepX;
    private float stepY; 
    private float elapsedTime = 0f; // Time elapsed since the start of the animation
    public bool isUp;
    public bool isDown;
    public bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        isUp = false;
        isDown = true;
        isMoving=false;
        upY = transform.position.y + 0.1f;
        downY = transform.position.y ;
        upX = transform.position.x + 0.05f;
        downX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Q))
        {
         isMoving = true;
           elapsedTime = 0f;
        }
        if(transform.position.y==downY)
        {
            isUp = false;
            isDown=true;
        }
        if(transform.position.y==upY)
        {
            isUp = true;
            isDown = false;
        }
        if(upY > transform.position.y && transform.position.y > downY)
        {
            isMoving = true;
        }
        if (isMoving)
        {
            // Increment the elapsed time by Time.deltaTime
            elapsedTime += Time.deltaTime;

            // Calculate the interpolation parameter using Mathf.SmoothStep
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / moveDuration);


            // Interpolate the Y position between upY and downY
            if (!isDown)
            {
                 stepY = Mathf.Lerp(upY, downY, t);
                 stepX = Mathf.Lerp(upX, downX, t);
            }
            else
            {
                 stepY= Mathf.Lerp(downY, upY, t);
                 stepX = Mathf.Lerp(downX, upX, t);
            }
                // Update the object's position
            transform.position = new Vector3(stepX, stepY, transform.position.z);

            // Check if the animation is complete
            if (elapsedTime >= moveDuration)
            {
                // Optionally reset the timer or perform other actions
                isMoving = false;
            }
        }
    }
}
