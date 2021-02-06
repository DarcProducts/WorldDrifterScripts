using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffTrigger : MonoBehaviour
{
    public List<GameObject> gameObjects;

    public GameObject objectTarget;

    public GameObject viewerTarget;

    public float distanceFromTarget;

    public bool isViewerActive = false;

    void FixedUpdate()
    {
        if (Input.GetButton("L Shift"))
        {
            isViewerActive = true;
        }
        else
            isViewerActive = false;

        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject != null)
            {
                if (gameObject.transform.position.x <= objectTarget.transform.position.x + distanceFromTarget || gameObject.transform.position.y <= objectTarget.transform.position.y + distanceFromTarget)
                {
                    gameObject.SetActive(true);
                }
                if (gameObject.transform.position.x <= objectTarget.transform.position.x - distanceFromTarget || gameObject.transform.position.y <= objectTarget.transform.position.y - distanceFromTarget)
                {
                    gameObject.SetActive(true);
                }

                if (gameObject.transform.position.x >= objectTarget.transform.position.x + distanceFromTarget || gameObject.transform.position.y >= objectTarget.transform.position.y + distanceFromTarget)
                {
                    gameObject.SetActive(false);
                }

                if (gameObject.transform.position.x <= objectTarget.transform.position.x - distanceFromTarget || gameObject.transform.position.y <= objectTarget.transform.position.y - distanceFromTarget)
                {
                    gameObject.SetActive(false);
                }

                if (isViewerActive)
                {
                    if (gameObject.transform.position.x <= viewerTarget.transform.position.x + distanceFromTarget || gameObject.transform.position.y <= viewerTarget.transform.position.y + distanceFromTarget)
                    {
                        gameObject.SetActive(true);
                    }
                    if (gameObject.transform.position.x <= viewerTarget.transform.position.x - distanceFromTarget || gameObject.transform.position.y <= viewerTarget.transform.position.y - distanceFromTarget)
                    {
                        gameObject.SetActive(true);
                    }

                    if (gameObject.transform.position.x >= viewerTarget.transform.position.x + distanceFromTarget || gameObject.transform.position.y >= viewerTarget.transform.position.y + distanceFromTarget)
                    {
                        gameObject.SetActive(false);
                    }

                    if (gameObject.transform.position.x <= viewerTarget.transform.position.x - distanceFromTarget || gameObject.transform.position.y <= viewerTarget.transform.position.y - distanceFromTarget)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
