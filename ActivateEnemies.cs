using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemies : MonoBehaviour
{
    public float waitSpawnTime;

    public void FixedUpdate()
    {
        if (this.gameObject.activeSelf == false)
        {
            StartCoroutine(Spawn());
        }
    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSecondsRealtime(waitSpawnTime);
        this.gameObject.SetActive(true);
    }
}
