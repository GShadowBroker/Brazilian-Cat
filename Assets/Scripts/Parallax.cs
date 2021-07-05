using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform[] parallaxList;
    private float[] scaleList;
    public float smoothing = 1f;

    private Transform camTransform;
    private Vector3 previousCamPos;

    private void Awake()
    {
        camTransform = Camera.main.transform;
    }

    private void Start()
    {
        previousCamPos = camTransform.position;
        scaleList = new float[parallaxList.Length];

        for (int i = 0; i < parallaxList.Length; i++)
        {
            scaleList[i] = -parallaxList[i].position.z;
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < parallaxList.Length; i++)
        {
            float xParallax = (previousCamPos.x - camTransform.position.x) * scaleList[i];
            float yParallax = (previousCamPos.y - camTransform.position.y) * scaleList[i];

            Vector3 targetPos = new Vector3(
                parallaxList[i].position.x + xParallax,
                parallaxList[i].position.y + yParallax,
                parallaxList[i].position.z
            );

            // fade into new position
            parallaxList[i].position = Vector3.Lerp(parallaxList[i].position, targetPos, smoothing * Time.fixedDeltaTime);
        }

        previousCamPos = camTransform.position;
    }
}
