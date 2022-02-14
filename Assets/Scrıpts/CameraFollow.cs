using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 offset;
    public float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = playerTransform.position + offset;
        //transform.position = Vector3.SmoothDamp(transform.position, newPosition, smoothFactor);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
