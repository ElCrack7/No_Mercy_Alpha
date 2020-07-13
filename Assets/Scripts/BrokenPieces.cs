using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPieces : MonoBehaviour
{

    public float moveSpeed = 3f;
    private Vector3 moveDirecton;

    public float deceleration = 5f;
    public float lifeTime = 3f;

    public SpriteRenderer theSr;
    public float fadeSpeed = 2.5f;


   
    // Start is called before the first frame update
    void Start()
    {
        moveDirecton.x = Random.Range(-moveSpeed, moveSpeed);
        moveDirecton.y = Random.Range(-moveSpeed, moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirecton * Time.deltaTime;

        moveDirecton = Vector3.Lerp(moveDirecton, Vector3.zero, deceleration * Time.deltaTime);

        lifeTime -= Time.deltaTime;

        if(lifeTime<0)

        {
            theSr.color = new Color(theSr.color.r, theSr.color.g, theSr.color.b, Mathf.MoveTowards(theSr.color.a, 0f, fadeSpeed * Time.deltaTime));

            if(theSr.color.a == 0f)
            {
                Destroy(gameObject);
            }
            
        }
    }
}
