using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMainScript : MonoBehaviour
{
    public GameObject backgroundWorldA;
    public GameObject underWaterBackground;
    public PlayerController playerController;
   
    public GameObject player;

    public GameObject mainCamera;

    private Vector3 oldCameraPosition;
    private Vector3 newCameraPosition;

    public float distanceFromPlayer;

    private float cameraShakeXMin = -0.1f;
    private float cameraShakeXMax = 0.1f;
    private float cameraShakeYMin = -0.1f;
    private float cameraShakeYMax = 0.1f;

    private float cameraShakeX;
    private float cameraShakeY;

    public bool shakeCamera;

    public float lowerCameraRate;
    public float maxCameraDistance;

    public Vector3 cameraDuringLowering;

    public bool loweringCamera = false;

    public void Awake()
    {
        newCameraPosition.z = this.transform.position.z;
        oldCameraPosition.z = this.transform.position.z;
        cameraDuringLowering.z = this.transform.position.z;
    }

    public void FixedUpdate()
    {
        oldCameraPosition.x = player.transform.position.x;
        oldCameraPosition.y = player.transform.position.y + distanceFromPlayer;
        newCameraPosition.x = cameraShakeX;
        newCameraPosition.y = cameraShakeY;
        cameraDuringLowering.x = oldCameraPosition.x;

        if (loweringCamera == false)
        {
            cameraDuringLowering.y = oldCameraPosition.y;
        }
    }

    public void Update()
    {
        if (Input.GetButton("S"))
        {
            if (playerController.isGroundedA || playerController.isGroundedB)
            {
                if (cameraDuringLowering.y >= player.transform.position.y - maxCameraDistance)
                {
                    LowerCamera();
                    loweringCamera = true;
                }
                else
                    NormalizeCamera();
            }
        }

        if (Input.GetButton("W"))
        {
            if (playerController.isGroundedA || playerController.isGroundedB)
            {
                if (cameraDuringLowering.y <= player.transform.position.y + maxCameraDistance + 1f)
                {
                    RaiseCamera();
                    loweringCamera = true;
                }
                else
                    NormalizeCamera();
            }
        }

        if (Input.GetButtonUp("S"))
        {
            loweringCamera = false;
            NormalizeCamera();
        }

        if (Input.GetButtonUp("W"))
        {
            loweringCamera = false;
            NormalizeCamera();
        }

        cameraShakeX = Random.Range(cameraShakeXMin, cameraShakeXMax) + oldCameraPosition.x;
        cameraShakeY = Random.Range(cameraShakeYMin, cameraShakeYMax) + oldCameraPosition.y;


        if (shakeCamera)
        {
            if (loweringCamera != true)
            {
                if (player.activeSelf == true)
                {
                    CameraShake();
                }
            }
        }
        else if (loweringCamera == true)
        {
            mainCamera.transform.position = cameraDuringLowering;
        }
        else
            this.transform.position = oldCameraPosition;

        if (playerController.isSubmergedWater || playerController.isSubmergedLava)
        {
            underWaterBackground.SetActive(true);
        }
        else
            underWaterBackground.SetActive(false);
    }

    public void CameraShake()
    {
        this.transform.position = newCameraPosition;
    }

    public void LowerCamera()
    {
        cameraDuringLowering.y = cameraDuringLowering.y - lowerCameraRate * Time.deltaTime;
    }

    public void NormalizeCamera()
    {
        mainCamera.transform.position = oldCameraPosition;
    }
    public void RaiseCamera()
    {
        cameraDuringLowering.y = cameraDuringLowering.y + lowerCameraRate * Time.deltaTime;
    }
}
