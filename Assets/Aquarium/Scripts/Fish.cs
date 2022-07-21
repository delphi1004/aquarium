using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private bool turning;
    private float orgSpeed;
    private float rotaionSpeed;
    
    public float speed;
    public FishController controller;
    // Start is called before the first frame update
    void Start()
    {
        turning = false;
        speed = Random.Range(0.5f,1.5f);
        orgSpeed = speed;
        rotaionSpeed = controller.rotationSpeed;
    }

    void ApplyRules()
    {
        GameObject[] fishes = controller.allFish;
        Vector3 avgCenter = Vector3.zero;
        Vector3 avgAvoidance = Vector3.zero;
        float groupSpeed = 0.01f;
        float neighbourDistance;
        int totalGroup = 0;

        foreach (GameObject fish in fishes) {
            if(fish != gameObject){
                neighbourDistance = Vector3.Distance(fish.transform.position, transform.position);
                if(neighbourDistance <= controller.neighbourDistance){
                    avgCenter += fish.transform.position;
                    totalGroup++;

                    if(neighbourDistance < controller.avoidDistance){
                        //Debug.Log(neighbourDistance);
                        avgAvoidance += (transform.position - fish.transform.position);
                    }

                    Fish otherFish = fish.GetComponent<Fish>();
                    groupSpeed += otherFish.speed;
                }
            }
        }

        if(totalGroup > 0) {
            avgCenter = (avgCenter/totalGroup) + (controller.targetPos - transform.position);
            groupSpeed /= totalGroup;

            Vector3 direction = (avgCenter + avgAvoidance) - transform.position;
            if(direction != Vector3.zero){
                transform.rotation = Quaternion.Slerp(transform.rotation, 
                Quaternion.LookRotation(direction),
                controller.rotationSpeed * Time.deltaTime); 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Bounds bounds = new Bounds(controller.transform.position , controller.boundary *2);
        RaycastHit hit = new RaycastHit();
        Vector3 direction = Vector3.zero;

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 2;
        //Debug.DrawRay(transform.position, forward, Color.green);

        if(!bounds.Contains(transform.position)) {
            direction = controller.targetPos - transform.position;
            speed = orgSpeed + Random.Range(-0.3f,0.3f);
            turning = true;
        // } else if(Physics.Raycast(transform.position , forward,out hit)) {
        //     Debug.DrawRay(transform.position, forward, Color.red);
        //     direction = Vector3.Reflect(transform.forward,hit.normal);
        //     turning = true;
        } else{
            turning = false;
        }

        if(turning){
            transform.rotation =  Quaternion.Slerp(transform.rotation, 
                Quaternion.LookRotation(direction),
                controller.boundaryRotationSpeed * Time.deltaTime); 
        }else{
            if(Random.Range(0,100) < 1){
                // speed = Mathf.Lerp(orgSpeed, Random.Range(controller.minSpeed , controller.maxSpeed), Time.deltaTime * 50.0f);
                // Debug.Log(speed);
            }

            // if(Random.Range(0,100) < 20){
            //     ApplyRules();
            // }
        }

        ApplyRules();

        transform.Translate(0,0,Time.deltaTime * speed);
    }
}
