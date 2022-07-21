using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    private float angle;
    private float angleCounter;
    private float targetAngle;
    private float speed;
   
    void Awake() 
    {
        angle = transform.localRotation.eulerAngles.z;
        ResetPosition();
    }
       
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void ResetPosition()
    {
        angleCounter = 0f;
        targetAngle = Random.Range(Random.Range(-30,-5),Random.Range(5,30));
        speed = Random.Range(0.05f,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion target;

        target = Quaternion.Euler(transform.localRotation.eulerAngles.x,transform.localRotation.eulerAngles.y,angle + targetAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime *speed);
        angleCounter += 0.007f;

        if(angleCounter >= 2.0f){
           ResetPosition();
        }
    }
}
