using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [Header("Control Settings!")]
   // [HideInInspector] //ukrywa publiczne
   [SerializeField] //dla prywatnych
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
    AudioSource audioSource;
    public Vector3 originalPosition;

    void Start()
    {
        if(isRigitbody=TryGetComponent<Rigidbody>(out rb))
        {

            rb.maxAngularVelocity = maxAngularVelocity;
        }
        
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        audioSource=GetComponent<AudioSource>();

        originalPosition= new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    private void jump()
    {
        
        if (spaceAmount > 0)
        {

            if (GameObject.FindGameObjectWithTag("Player") == this.gameObject)
            {
                if (gameManager.time <= 0) { 
                    if (isRigitbody && Input.GetKeyDown(KeyCode.Joystick1Button2))
                    {
                        if (isOnGround == true)
                        {
                            rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
                            audioSource.Play();
                            spaceAmount--;
                        }
                    }
                }
            }

            if (GameObject.FindGameObjectWithTag("Player2") == this.gameObject)
            {
                if (gameManager.time <= 0)
                {
                    if (isRigitbody && Input.GetKeyDown(KeyCode.Space))
                    {
                        if (isOnGround == true)
                        {
                            rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
                            audioSource.Play();
                            spaceAmount--;
                        }
                    }
                }
            }


        }
    }
  

    void Update()
    {

        

        int respawn=-100;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            respawn = -45;
        }
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            respawn = -5;
            spaceAmount = 1;
        }
        if (transform.position.y <= respawn)
        {
            //if(GameObject.FindGameObjectWithTag("Player") == this.gameObject)
            //    gameManager.Respawn(this.gameObject, gameManager.startPosition);
            //if (GameObject.FindGameObjectWithTag("Player2") == this.gameObject)
            //    gameManager.Respawn(this.gameObject, gameManager.startPosition2);
            
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.transform.position = originalPosition;
        }

        jump();
    }

    private void FixedUpdate()
    {
        //if (GameObject.FindGameObjectWithTag("Player") == this.gameObject)
        //{

        //    if (isRigitbody && Input.GetKey(KeyCode.LeftArrow))
        //    {
        //        rb.AddTorque(0, 0, 1f * speed * Time.fixedDeltaTime); //si³a obrotowa
        //    }
        //    if (isRigitbody && Input.GetKey(KeyCode.RightArrow))
        //    {
        //        rb.AddTorque(0, 0, -1f * speed * Time.fixedDeltaTime); //si³a obrotowa
        //    }
        //    if (isRigitbody && Input.GetKey(KeyCode.UpArrow))
        //    {
        //        rb.AddTorque(1f * speed * Time.fixedDeltaTime, 0, 0); //si³a obrotowa
        //    }
        //    if (isRigitbody && Input.GetKey(KeyCode.DownArrow))
        //    {
        //        rb.AddTorque(-1f * speed * Time.fixedDeltaTime, 0, 0); //si³a obrotowa
        //    }
        //}

        
        if (GameObject.FindGameObjectWithTag("Player") == this.gameObject)
        {
            if (gameManager.time <= 0)
            {
                if (isRigitbody && (directionH = Input.GetAxis("Horizontal")) != 0)//poruszanie po osiach - wsad stra³ki lub pad
                {
                    // if(this.gameObject==)
                        rb.AddTorque(0, 0, -directionH * speed * Time.fixedDeltaTime); //si³a obrotowa
                                                                                  //rb.AddForce(direction * Time.deltaTime * speed, 0, 0); //czysta si³a
                                                                                  // transform.Translate(direction * Time.deltaTime * speed, 0, 0, Space.World); //przesuwanie obiektu bez obracania
                }

                if (isRigitbody && (directionV = Input.GetAxis("Vertical")) != 0)
                {
                    rb.AddTorque(directionV * speed * Time.fixedDeltaTime, 0, 0);

                }

            }
        }


        if (GameObject.FindGameObjectWithTag("Player2") == this.gameObject)
            {
            if (gameManager.time <= 0)
            {
                if (isRigitbody && Input.GetKey(KeyCode.A))
                {
                    rb.AddTorque(0, 0, 1f * speed * Time.fixedDeltaTime); //si³a obrotowa
                }
                if (isRigitbody && Input.GetKey(KeyCode.D))
                {
                    rb.AddTorque(0, 0, -1f * speed * Time.fixedDeltaTime);
                }
                if (isRigitbody && Input.GetKey(KeyCode.W))
                {
                    rb.AddTorque(1f * speed * Time.fixedDeltaTime, 0, 0);
                }
                if (isRigitbody && Input.GetKey(KeyCode.S))
                {
                    rb.AddTorque(-1f * speed * Time.fixedDeltaTime, 0, 0);
                }
            }
        }

        



    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")|| other.gameObject.CompareTag("DestructFloor")|| other.gameObject.CompareTag("Fly"))
        {
            isOnGround = true;
        }
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
        {
            isOnGround = true;
        }
        if (collision.gameObject.CompareTag("Fly"))
        {
            rb.AddForce(0, 8, 0, ForceMode.Impulse);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("DestructFloor") || other.gameObject.CompareTag("Fly"))
        {
            isOnGround = false;
        }

        if (other.gameObject.CompareTag("Fly"))
        {
            rb.AddForce(0, 8, 1, ForceMode.Impulse);
        }
    }

}
