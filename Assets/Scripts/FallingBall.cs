using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBall : MonoBehaviour
{
    public float minSize = 0.1f;
    public float maxSize = 5f;
    public float scaleChangeAmount;
    public Rigidbody rb;
    public float maxVelocity = 12f;
    // Start is called before the first frame update
    void Start()
    {
        scaleChangeAmount = Random.Range(0.3f, 0.6f);
        rb = GetComponent<Rigidbody>();
        rb.drag = Random.Range(0.45f, 0.9f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        
        if (transform.position.y < -3f)
        {
            //prep position to top of screen
            Vector3 newPos = transform.position;
            newPos.y = 10f;
            newPos.z = Random.Range(-4f, 4f);
            newPos.x = Random.Range(-14f, -6f);
            
            //rescale for more fun
            // Get the current scale
            Vector3 currentScale = transform.localScale;
            // Calculate the new scale with the specified change amount
            float newScaleX = Mathf.Clamp(currentScale.x + scaleChangeAmount, minSize, maxSize);
            float newScaleY = Mathf.Clamp(currentScale.y + scaleChangeAmount, minSize, maxSize);
            float newScaleZ = Mathf.Clamp(currentScale.z + scaleChangeAmount, minSize, maxSize);
            // Set the new scale of the sphere
            transform.localScale = new Vector3(newScaleX, newScaleY, newScaleZ);
            scaleChangeAmount = -scaleChangeAmount;
            rb.drag = Random.Range(0.5f, 3.3f);
            //update position to top of screen
            transform.position = newPos;
            
        }
    }
}
