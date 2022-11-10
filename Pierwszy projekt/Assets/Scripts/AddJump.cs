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
        if(other.CompareTag("Player"))
        {
            soundManager.PlaySound(SoundManager.Sounds.Bonus);
            if(SceneManager.GetActiveScene().buildIndex == 1)
                movement.spaceAmount++;
            if (SceneManager.GetActiveScene().buildIndex == 2)
                movement.originalPosition=new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

            Destroy(gameObject);
           
        }

        if (other.CompareTag("Player2"))
        {
            soundManager.PlaySound(SoundManager.Sounds.Bonus);
            if (SceneManager.GetActiveScene().buildIndex == 1)
                movement2.spaceAmount++;

            if (SceneManager.GetActiveScene().buildIndex == 2)
                movement2.originalPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

            Destroy(gameObject);
            
        }
     }

    
}
