using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public CameraMainScript cameraMainScript;
    public GameObject targetObject;
    public GameObject playerRagDoll;
    private float distanceToTargetX;
    private float distanceToTargetY;
    private float distanceToRagDollX;
    private float distanceToRagDollY;
    public bool followingPlayer = true;
    public bool followingRagDoll = false;
    private Vector3 newCameraPosition;
    private Vector3 newCameraPosition2;

    public GameObject cameraA;
    public GameObject cameraB;
  
    void Start()
    {
        distanceToTargetX = transform.position.x - targetObject.transform.position.x;
        distanceToTargetY = transform.position.y - targetObject.transform.position.y;
        distanceToRagDollX = transform.position.x - playerRagDoll.transform.position.x;
        distanceToRagDollY = transform.position.y - playerRagDoll.transform.position.y;
    }

    void FixedUpdate()
    {
        if (cameraMainScript.shakeCamera == false)
        {
            if (followingPlayer == true)
            {
                CameraFollowPlayer();
            }
        }

        if (followingRagDoll)
        {
            CameraFollowRagDoll();
        }
    }

    void CameraFollowPlayer()
    {
        float targetObjectX = targetObject.transform.position.x;
        float targetObjectY = targetObject.transform.position.y;
        newCameraPosition.x = targetObjectX + distanceToTargetX;
        newCameraPosition.y = targetObjectY + distanceToTargetY;
        transform.position = newCameraPosition;
    }

    void CameraFollowRagDoll()
    {
        cameraA.SetActive(false);
        cameraB.SetActive(false);
        float playerRagDollX = playerRagDoll.transform.position.x;
        float playerRagDollY = playerRagDoll.transform.position.y;
        newCameraPosition2.x = playerRagDollX + distanceToRagDollX;
        newCameraPosition2.y = playerRagDollY + distanceToRagDollY;
        newCameraPosition2.z = -10;
        transform.position = newCameraPosition2;
    }
}
