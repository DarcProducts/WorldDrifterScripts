using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionMech : MonoBehaviour
{
    public Rigidbody2D robot_Part1Prefab;
    public Rigidbody2D robot_Part2Prefab;
    public Rigidbody2D robot_Part3Prefab;
    public Rigidbody2D robot_Part4Prefab;

    private byte yOffset = 1;
    private byte xOffset = 1;

    public float thrust;

    private Vector2 direction;
    private float force;

    private void OnEnable()
    {
        RobotExplosion();
    }

    public void RobotExplosion()
    {
        direction.x = Random.Range(-xOffset * thrust, xOffset * thrust);
        direction.y = Random.Range(-yOffset * thrust, yOffset * thrust);

        robot_Part1Prefab.AddForce(direction, ForceMode2D.Impulse);
        robot_Part2Prefab.AddForce(-direction, ForceMode2D.Impulse);
        robot_Part3Prefab.AddForce(direction, ForceMode2D.Impulse);
        robot_Part4Prefab.AddForce(-direction, ForceMode2D.Impulse);
    }
}
