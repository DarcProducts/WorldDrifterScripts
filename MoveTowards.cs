using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    private Vector2 playerDirection;
    public GameObject playerPosition;

    public float followSpeed;

    private void LateUpdate()
    {
        playerDirection.x = this.transform.position.x - playerPosition.transform.position.x;
        playerDirection.y = this.transform.position.y - playerPosition.transform.position.y;

        this.transform.position = Vector2.MoveTowards(this.transform.position, playerPosition.transform.position, followSpeed * Time.deltaTime);
    }
}
