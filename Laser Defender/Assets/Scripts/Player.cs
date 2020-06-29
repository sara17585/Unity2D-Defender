using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    //Configuration Parameters

    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] int health = 500;
    [SerializeField] AudioClip deathSoundEffect;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projecttileFiringPeriod = 0.7f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;



    Coroutine firing;



    float xMin;
    float xMax;

    float yMin;
    float yMax;


  


    //public float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
       
        SetUpMoveBoundary();
        
    }

  

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
      
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firing =  StartCoroutine(FireContinously());
        }
        if (Input.GetButtonUp("Fire1"))
        {

            StopCoroutine(firing);
            //StopAllCoroutines();

        }
        
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;


        
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin , xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin , yMax );
     
        transform.position = new Vector2(newXPos, newYPos);
    }


    private void SetUpMoveBoundary()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    IEnumerator FireContinously()
    {
        {
            while (true)
            {
                var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
                yield return new WaitForSeconds(projecttileFiringPeriod);
            }
           // Destroy(laser, 1f);

        }
    }



     private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) { return; }
        Damage(other, damageDealer);
    }

    private void Damage(Collider2D other, DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0 )
        {
            FindObjectOfType<Level>().LoadGameOver();
            AudioSource.PlayClipAtPoint(deathSoundEffect, Camera.main.transform.position, deathSoundVolume);
            Destroy(gameObject);
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
