﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject bulletToFire;
    public Transform firePoint;

    public float timeBetweenShots;
    private float shotCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerController.instance.canMove && Levelmanager.instance.isPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                shotCounter = timeBetweenShots;
                AudioManager.instance.PlaySFX(12);
            }

            //if (Input.GetMouseButtonUp(0))
            //{
            //    shotCounter -= Time.deltaTime;

            //    if (shotCounter <= 0)
            //    {
            //        Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
            //        AudioManager.instance.PlaySFX(12);

            //        shotCounter = timeBetweenShots;
            //    }
            //}
        }
    }
}
