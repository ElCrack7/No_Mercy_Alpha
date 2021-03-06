﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public static Boss instance;
    public BossAction[] actions;

    private int currentAction;
    private float actionCounter;

    private float shotCounter;
    private Vector2 moveDirection;
    public Rigidbody2D rb;

    public int currentHealth;
    public GameObject deathEffect, hitEffect;
    public GameObject levelExit;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        actionCounter = actions[currentAction].actionLength; 
    }

    // Update is called once per frame
    void Update()
    {
        if (actionCounter > 0)
        {
            actionCounter -= Time.deltaTime;

            //ruch

            moveDirection = Vector2.zero;

            if (actions[currentAction].shouldMove)
            {
                if(actions[currentAction].shouldCHasePlayer)
                {
                    moveDirection = PlayerController.instance.transform.position - transform.position;
                    moveDirection.Normalize();
                }

                if (actions[currentAction].moveToPoints)
                {
                    moveDirection = actions[currentAction].pointToMoveTo.position - transform.position;
                }
            }



            rb.velocity = moveDirection * actions[currentAction].moveSpedd;


            //shhotin
            if(actions[currentAction].shouldShoot)
            {
                shotCounter -= Time.deltaTime;
                if(shotCounter <=0)
                {
                    shotCounter = actions[currentAction].timeBeetwenShots;

                    foreach (Transform t in actions[currentAction].shootPoints)
                    {
                        Instantiate(actions[currentAction].itemToShoot, t.position, t.rotation);
                    }
                }
            }

        }else
        {
            currentAction++;
            if(currentAction >= actions.Length)
            {
                currentAction = 0;
            }
            actionCounter = actions[currentAction].actionLength;

            

        }
    }

    public void TakeDamage(int damageAmount)
    {


        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);

            Instantiate(deathEffect, transform.position, transform.rotation);

            if (Vector3.Distance(PlayerController.instance.transform.position, levelExit.transform.position) < 2f)
            {
                levelExit.transform.position += new Vector3(4f, 0f, 0f);
            }

            levelExit.SetActive(true);

            
        }
    }
}
[System.Serializable]
public class BossAction
{
    [Header("Action")]
    public float actionLength;

    public bool shouldMove;
    public bool shouldCHasePlayer;
    public float moveSpedd;

    public bool moveToPoints;
    public Transform pointToMoveTo;


    public bool shouldShoot;
    public GameObject itemToShoot;
    public float timeBeetwenShots;
    public Transform[] shootPoints;
}
