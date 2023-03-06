using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AddJump : MonoBehaviour
{
    private Movement movement;
    private Movement movement2;
   

    private SoundManager soundManager;

    private void Start()
    {
       movement=GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
       movement2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Movement>();
       soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            soundManager.PlaySound(SoundManager.Sounds.Bonus);
            Destroy(gameObject);
            if (other.CompareTag("Player"))
                movement.spaceAmount++;
            else if (other.CompareTag("Player2"))
                movement2.spaceAmount++;

        }
    }
}
