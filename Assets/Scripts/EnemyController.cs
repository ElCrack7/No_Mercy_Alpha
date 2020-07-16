using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;


    public bool shouldChasePlayer;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    public bool shouldRunAway;
    public float runawayRange;

    public bool shouldWander;
    public float wanderLenght, pauseLenght;
    private float wanderCounter, pauseCounter;
    private Vector3 wanderDirection;

    public Animator anim;

    public int health = 150;

    public GameObject[] deathSplatters;

    public GameObject hitEffect;

    public bool shouldDropItem;
    public GameObject[] itemsToDrop;
    public float itemDropPercent;

    public bool shouldShoot;
    public GameObject bullet;

    public Transform firePoint;
    public float firerate;
    private float fireCounter;

    public SpriteRenderer theBody;

    public float shootRange;
    void Start()
    {
        if(shouldWander)
        {
            pauseCounter = Random.Range(pauseLenght * .75f, pauseLenght * 1.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            moveDirection = Vector3.zero;

            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer && shouldChasePlayer)
        {

            moveDirection = PlayerController.instance.transform.position - transform.position;
            }
            else
            {
                if(shouldWander)
                {
                    if(wanderCounter > 0)
                    {
                        wanderCounter -= Time.deltaTime;
                        //ruch wroga(gluta)
                        moveDirection = wanderDirection;

                        if(wanderCounter <=0)
                        {
                            pauseCounter = Random.Range(pauseLenght * .75f, pauseLenght * 1.25f);
                        }
                    }

                    if(pauseCounter > 0)
                    {
                        pauseCounter -= Time.deltaTime;

                        if(pauseCounter <= 0)
                        {
                            wanderCounter = Random.Range(wanderLenght * .75f, wanderLenght * 1.25f);

                            wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                        }
                    }
                }
            }

            if(shouldRunAway && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < runawayRange)
            {
                moveDirection = transform.position - PlayerController.instance.transform.position;
            }

           

        //    else
        //{
        //    moveDirection = Vector3.zero;
        //}
    

        moveDirection.Normalize();

        rb.velocity = moveDirection * moveSpeed;




        if(shouldShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootRange)
        {
            fireCounter -= Time.deltaTime;
            if(fireCounter <= 0)
            {

                fireCounter = firerate;
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                    AudioManager.instance.PlaySFX(13);
                }
        }

        }else
        {
            rb.velocity = Vector2.zero;
        }

        if (moveDirection != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;
        AudioManager.instance.PlaySFX(2);

        Instantiate(hitEffect, transform.position, transform.rotation);

        if(health <= 0)
        {
            Destroy(gameObject);

            AudioManager.instance.PlaySFX(1);

            int selectedSplatter = Random.Range(0, deathSplatters.Length);

            int rotation = Random.Range(0, 4);

            Instantiate(deathSplatters[selectedSplatter], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f ));
            if (shouldDropItem)
            {
                float dropChance = Random.Range(0f, 100f);

                if (dropChance < itemDropPercent)
                {
                    int randomItem = Random.Range(0, itemsToDrop.Length);

                    Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
                }
            }


        }
    }
}
