using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusic : MonoBehaviour
{
    private SoundManager soundManager;
    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            soundManager.StopSound();
            soundManager.PlaySound(SoundManager.Sounds.Music);
            Destroy(this.gameObject);
        }
    }
}
