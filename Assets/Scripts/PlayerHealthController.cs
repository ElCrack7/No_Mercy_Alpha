using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth;

    public float damageInvincLenght = 1f;
    private float invincCount;

 

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.healtSlider.maxValue = maxHealth;
        UIController.instance.healtSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        if(invincCount > 0)
        {
            invincCount -= Time.deltaTime;

            if(invincCount <= 0)
            {
                PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, 1f);
            }
        }
    }

    public void DamagePlayer()
    {
        if(invincCount <= 0)
        {

        

        currentHealth--;
       
        invincCount = damageInvincLenght;

            PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, .4f);

        if(currentHealth <= 0)
        {
            PlayerController.instance.gameObject.SetActive(false);

            UIController.instance.deathScreen.SetActive(true);
        }


        UIController.instance.healtSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        }
    }


}
