using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 distance;
    public float lookUp;
    public float lerpAmount;
    public float turnSpeed = 4.0f;

    private Vector3 offset;
    private GameObject ball;

    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 90;

        if (this.gameObject.name == "Main Camera")
            ball = GameObject.FindGameObjectWithTag("Player");

        if(this.gameObject.name == "Camera2")
            ball = GameObject.FindGameObjectWithTag("Player2");

        offset = new Vector3(ball.transform.position.x, ball.transform.position.y, ball.transform.position.z);
    }


    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, ball.transform.position + distance, lerpAmount * Time.deltaTime);
        transform.LookAt(ball.transform.position);
        transform.Rotate(-lookUp, 0, 0);
    }
}
