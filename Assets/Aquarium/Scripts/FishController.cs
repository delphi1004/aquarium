using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    private BoxCollider boxCollider;

    public GameObject greenFish;
    public GameObject pinkFish;
    public int maxFish = 20;
    public GameObject[] allFish;
    public Vector3 boundary;
    public Vector3 targetPos;

    [Header("Fish settings")]
    [Range(0.1f,2.0f)]
    public float minSpeed;
    [Range(0.1f,5.0f)]
    public float maxSpeed;
    [Range(1.0f,10.0f)]
    public float avoidDistance;
    [Range(0.1f,5.0f)]
    public float rotationSpeed;
    [Range(1.0f,5.0f)]
    public float boundaryRotationSpeed;


    void Awake() 
    {
        minSpeed = 0.5f;
        maxSpeed = 1.0f;
        avoidDistance = 0.5f;
        rotationSpeed = 1f;
        boundaryRotationSpeed = 2.5f;
        boxCollider = GetComponent<BoxCollider>();
        boundary = new Vector3(boxCollider.size.x/2,boxCollider.size.y/2,boxCollider.size.z/2);
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 fishPosition;
        allFish = new GameObject[maxFish];

        for(int i=0;i<maxFish;i++){
            fishPosition = transform.position + new Vector3(Random.Range(-boundary.x,boundary.x),
            Random.Range(-boundary.y,boundary.y),
            Random.Range(-boundary.z,boundary.z));

            if(Random.Range(0,2) > 0.5f){
                allFish[i] = Instantiate(greenFish,fishPosition,Quaternion.identity);
            }else{
                allFish[i] = Instantiate(pinkFish,fishPosition,Quaternion.identity);
            }
            allFish[i].GetComponent<Fish>().controller = this;
        }

        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0,100) < 0.000002){
            targetPos = transform.position + new Vector3(Random.Range(-boundary.x,boundary.x),
                Random.Range(-boundary.y,boundary.y),
                Random.Range(-boundary.z,boundary.z));
        }
    }
}
