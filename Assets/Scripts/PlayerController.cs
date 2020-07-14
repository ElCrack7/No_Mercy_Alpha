using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    private Vector2 moveInput;

    public Rigidbody2D rb;

    public Transform gunArm;

    private Camera theCam;

    public Animator anim;

    public GameObject bulletToFire;
    public Transform firePoint;

    public float timeBetweenShots;
    private float shotCounter;

    public SpriteRenderer bodySR;

    private float activeMoveSpeed;

    public float dashSpeed = 8f, dashLength = .5f, dashCooldown = 1f, dashInvinvibility = .5f;
    [HideInInspector]
    private float dashCoolCounter;
    public float dashCounter;
    [HideInInspector]
    public bool canMove = true;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;

        activeMoveSpeed = moveSpeed; 
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        { 
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();


        //transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime  * moveSpeed, 0f);

        rb.velocity = moveInput * activeMoveSpeed;


        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);

        if(mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            gunArm.localScale = Vector3.one;
        }

        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0, 0, angle);


        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
            shotCounter = timeBetweenShots;
            AudioManager.instance.PlaySFX(12);
        }

        if(Input.GetMouseButtonUp(0))
        {
            shotCounter = shotCounter - Time.deltaTime;

            if(shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                AudioManager.instance.PlaySFX(12);

                shotCounter = timeBetweenShots;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCoolCounter <= 0)
            { 
            activeMoveSpeed = dashSpeed;
            dashCounter = dashLength;

                anim.SetTrigger("dash");

                PlayerHealthController.instance.MakeInvincible(dashInvinvibility);
                AudioManager.instance.PlaySFX(8);
            }
        }
        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if(dashCounter <=0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }
        if(dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }




        if(moveInput != Vector2.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        }
        else
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }

    }
}
