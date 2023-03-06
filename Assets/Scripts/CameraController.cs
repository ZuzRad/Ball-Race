using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    
    public Vector3 distance;
    public float lookUp;
    public float lerpAmount;
    private GameObject ball;
    public float turnSpeed = 4.0f;

    private Vector3 offset;


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

        //offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;

        //// Debug.Log(offset);
        ////  Debug.Log(transform.position);
        //transform.position = ball.transform.position + offset;
        //transform.LookAt(ball.transform.position);


        transform.position = Vector3.Lerp(transform.position, ball.transform.position + distance, lerpAmount * Time.deltaTime); //kamera kt�ra p�ynnie przesuwa si� za pi�k�
        //transform.position = ball.transform.position + distance; //kamera na sta�e
        transform.LookAt(ball.transform.position);
        transform.Rotate(-lookUp, 0, 0);


    }
}
