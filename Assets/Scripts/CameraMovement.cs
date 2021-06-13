using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float cameraSpeed;
    [SerializeField] float slowCameraSpeed;
    [SerializeField] float cameraSpeedChangeVel = 0.5f;
    [SerializeField] float maxFreeMoveX = 5f;
    [SerializeField] float maxFreeMoveY = 5f;
    [SerializeField] float slowCameraFollowZoneX = 2f;
    [SerializeField] float slowCameraFollowZoneY = 2f;
    [SerializeField] float cameraSizeSpeed = 2f;
    [SerializeField] [Range(0,-200)]float maxCamSize = -10f;
    [SerializeField] float massToSize = 0.02f;

    float originalPosZ;
    float playerMass;
    Vector3 playerPos;
    public bool moveToPlayer = false;
    Vector3 newPos;
    public float currentCameraSpeed;
    // Update is called once per frame
    private void Start()
    {
        originalPosZ = transform.position.z;
    }
    void Update()
    {
        playerPos = player.transform.position;
        //Detect if player outside camera zone
        newPos = new Vector3(playerPos.x, playerPos.y, transform.position.z);
        if (transform.position.x + maxFreeMoveX < playerPos.x | transform.position.x - maxFreeMoveX > playerPos.x |
            transform.position.y + maxFreeMoveY < playerPos.y | transform.position.y - maxFreeMoveY > playerPos.y)
        {
            Debug.Log("Player is outside camerazone");
            moveToPlayer = true;
            currentCameraSpeed += cameraSpeedChangeVel * Time.deltaTime;
            currentCameraSpeed = Mathf.Clamp(currentCameraSpeed, slowCameraSpeed, cameraSpeed);
        }
        //Moves camera to player
        if (moveToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPos, currentCameraSpeed * Time.deltaTime);
        }
        //Stop camera follow
        /*
        if (transform.position.x > playerPos.x - 0.1f & transform.position.x < playerPos.x + 0.1f &
            transform.position.y > playerPos.y - 0.1f & transform.position.y < playerPos.y + 0.1f)
        {
            Debug.Log("Camera is centered");
            moveToPlayer = false;
        }
        */
        //Slower Camera follow if inside slow zone
        if (transform.position.x > playerPos.x - slowCameraFollowZoneX & transform.position.x < playerPos.x + slowCameraFollowZoneX &
            transform.position.y > playerPos.y - slowCameraFollowZoneY & transform.position.y < playerPos.y + slowCameraFollowZoneY)
        {
            //Slowly changes camera speed
            currentCameraSpeed -= cameraSpeedChangeVel * Time.deltaTime;
            currentCameraSpeed = Mathf.Clamp(currentCameraSpeed, slowCameraSpeed, cameraSpeed);
        }


        //Move further when Player gets bigger

        playerMass = player.GetComponent<Rigidbody>().mass;

        float newCameraZ = -(playerMass * massToSize);
        newCameraZ = Mathf.Clamp(newCameraZ, maxCamSize, originalPosZ);
        var newCamPos = new Vector3(transform.position.x, transform.position.y, newCameraZ);
        transform.position = Vector3.MoveTowards(transform.position, newCamPos, cameraSizeSpeed * Time.deltaTime);
            
    }
}
