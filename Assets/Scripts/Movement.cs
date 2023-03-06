using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [Header("Control Settings!")]
    private float speed;
    public float maxAngularVelocity;
    public int spaceAmount;
   
    private Rigidbody rb;
    private bool isRigitbody;
    private float jumpHeight = 6f;
    private bool isOnGround = true;
    public float directionH;
    public float directionV;
    private GameManager gameManager;
    public Vector3 originalPosition;
    AudioSource audioSource;

    void Start()
    {
        if(isRigitbody=TryGetComponent<Rigidbody>(out rb))
            rb.maxAngularVelocity = maxAngularVelocity;
        
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        audioSource=GetComponent<AudioSource>();
        originalPosition= new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    private void jump()
    {
        if (spaceAmount > 0)
        {
            if(GameObject.FindGameObjectWithTag("Player") == this.gameObject && gameManager.time <= 0 && isRigitbody && Input.GetKeyDown(KeyCode.Joystick1Button2) && isOnGround)
            {
                rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
                audioSource.Play();
                spaceAmount--;
            }

            if (GameObject.FindGameObjectWithTag("Player2") == this.gameObject && gameManager.time <= 0 && isRigitbody && Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
                audioSource.Play();
                spaceAmount--;  
            }
        }
    }
  

    void Update()
    {

        int respawn=-100;
        if (SceneManager.GetActiveScene().buildIndex == 1)
            respawn = -45;

        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            respawn = -5;
            spaceAmount = 1;
        }

        if (transform.position.y <= respawn)
        { 
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.transform.position = originalPosition;
        }

        jump();
    }

    private void FixedUpdate()
    {
        if (gameManager.time <= 0 && GameObject.FindGameObjectWithTag("Player") == this.gameObject)
        {
            if (isRigitbody && (directionH = Input.GetAxis("Horizontal")) != 0)
                rb.AddTorque(0, 0, -directionH * speed * Time.fixedDeltaTime);

            if (isRigitbody && (directionV = Input.GetAxis("Vertical")) != 0)
                rb.AddTorque(directionV * speed * Time.fixedDeltaTime, 0, 0);
        }

        if (GameObject.FindGameObjectWithTag("Player2") == this.gameObject && gameManager.time <= 0)
        {
            if (isRigitbody && Input.GetKey(KeyCode.A))
                rb.AddTorque(0, 0, 1f * speed * Time.fixedDeltaTime);

            else if (isRigitbody && Input.GetKey(KeyCode.D))
                rb.AddTorque(0, 0, -1f * speed * Time.fixedDeltaTime);

            else if (isRigitbody && Input.GetKey(KeyCode.W))
                rb.AddTorque(1f * speed * Time.fixedDeltaTime, 0, 0);

            else if (isRigitbody && Input.GetKey(KeyCode.S))
                rb.AddTorque(-1f * speed * Time.fixedDeltaTime, 0, 0);

        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")|| other.gameObject.CompareTag("DestructFloor")|| other.gameObject.CompareTag("Fly"))
            isOnGround = true;

        if (other.gameObject.CompareTag("Fly"))
        {
            rb.AddForce(0, 8, 0, ForceMode.Impulse);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("DestructFloor") || collision.gameObject.CompareTag("Fly"))
            isOnGround = true;

        if (collision.gameObject.CompareTag("Fly"))
            rb.AddForce(0, 8, 0, ForceMode.Impulse);
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("DestructFloor") || other.gameObject.CompareTag("Fly"))
            isOnGround = false;

        if (other.gameObject.CompareTag("Fly"))
            rb.AddForce(0, 8, 1, ForceMode.Impulse);
    }
}
