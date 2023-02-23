using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Balls settings")]
    public GameObject ballPrefab;
    public Transform startPosition;

    public GameObject ballPrefab2;
    public Transform startPosition2;

    [Header("Time settings")]
    public TextMeshProUGUI timetext;
   // public TextMeshProUGUI timetext2;
   // public float time2;
    public float time;
    private float timeHelp;

    private Movement movement;
    private Movement movement2;

    public TextMeshProUGUI winText;
    public TextMeshProUGUI jumpText;
    public TextMeshProUGUI jumpText2;

    private SoundManager soundManager;


    private void Awake() //metoda która wykonuje siê pierwsza na scenie
    {
        Instantiate(ballPrefab, startPosition.position, Quaternion.identity);
        Instantiate(ballPrefab2, startPosition2.position, Quaternion.identity);
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void Start()
    {
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        movement2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Movement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        time-=Time.deltaTime;

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            jumpText.text = "";
            jumpText2.text = "";
        }
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            jumpText.text = "Jump: " + movement.spaceAmount;
            jumpText2.text = "Jump: " + movement2.spaceAmount;
        }
        
        timetext.text = "Time: " + Mathf.Clamp(Mathf.CeilToInt(time), 0, int.MaxValue).ToString();
        if (time <= 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("StartFloor"));
            timetext.text = "";
            
        }
        

    }

    public void Respawn(GameObject ball, Transform startPosition)
    {
        Destroy(ball);
        Instantiate(ball, startPosition.position, Quaternion.identity);
        
    }
}
