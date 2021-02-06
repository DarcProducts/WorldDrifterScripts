using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public GameObject player;
    public GameObject playerRagDoll;
    public GameObject yourDeadPanel;
    public CameraFollow cameraFollow;
    public bool deathActivated = false;

    public void Death()
    {
        cameraFollow.followingPlayer = false;
        player.SetActive(false);
        playerRagDoll.SetActive(true);
        cameraFollow.followingRagDoll = true;
        yourDeadPanel.SetActive(true);
        deathActivated = true;
    }
}
