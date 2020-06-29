using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float health = 100;
    float ShotCounter;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField] int scoreValue = 150;


    [Header("Projectile")]
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] AudioClip[] soundEffects;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;


    //AudioSource myAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        ShotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
       // myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        ShotCounter -= Time.deltaTime;
        if(ShotCounter <= 0)
        {
            Fire();
            ShotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {

        GameObject fire = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity) as GameObject;

        fire.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        //  myAudioSource.PlayOneShot(soundEffects[1]);
        AudioSource.PlayClipAtPoint(soundEffects[1], Camera.main.transform.position,shootSoundVolume);
        

    }

  
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        Damage(other, damageDealer);
    }

    private void Damage(Collider2D other, DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {

            FindObjectOfType<GameScore>().AddToScore(scoreValue);

            AudioSource.PlayClipAtPoint(soundEffects[0], Camera.main.transform.position,deathSoundVolume);

            GameObject explosionParticle = Instantiate(explosionEffect, transform.position, transform.rotation);

            Destroy(explosionParticle, 0.2f);
            Destroy(gameObject);
        }
    }
}
