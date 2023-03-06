using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructBox : MonoBehaviour
{
    public Color startColor;
    public Color endColor;
    public float life;
    public bool isRigidbody = false;

    private Material material;
    private int maxlife = 15;
    private SoundManager soundManager;
    Rigidbody rb;

    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        life = Mathf.Clamp(life, 1, maxlife);
        material = GetComponent<MeshRenderer>().material;

        SetColor();

        if (isRigidbody)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.mass = 0.01f;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
        {
            life -= 0.5f;
            if (life > 0)
                SetColor();
            else
            {
                soundManager.PlaySound(SoundManager.Sounds.Destroy);
                Destroy(this.gameObject);
            }
        }
    }


    void SetColor()
    {
        material.color = Color.Lerp(endColor, startColor, (float)(life - 1) / (float)(maxlife - 1));
    }
}
