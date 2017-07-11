using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
    public float xMin, xMax, zMin, zMax;
}

public enum Platform
{
    PC, Android
}


public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;
    public Platform platform;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private Rigidbody rb;
    private AudioSource audioSource;
    private float nextFire;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (platform == Platform.PC)
        {
            if (Input.GetKeyDown("space") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                audioSource.Play();
            }
        }
        
        if (platform == Platform.Android)
        {
            if (Input.touchCount > 0 && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                audioSource.Play();
            }
        }
    }

    void FixedUpdate()
    {
        if (platform == Platform.PC)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.velocity = movement * speed;

            rb.position = new Vector3(
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );

            rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * tilt);
        }

        if (platform == Platform.Android)
        {
            float moveHorizontal = Input.acceleration.x;
            float moveVertical = Input.acceleration.y;

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.velocity = movement * speed;

            rb.position = new Vector3(
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );

            rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * tilt);
        }
    }
}
