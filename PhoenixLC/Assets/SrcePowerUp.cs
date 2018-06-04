using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SrcePowerUp : MonoBehaviour {

    public RectTransform healthBar;
    public float heal=20f;
    float maxWidthHealth;


    void Awake()
    {
        
        maxWidthHealth = healthBar.rect.width;
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        if(maxWidthHealth/ healthBar.rect.width == 1)
        {
            return;
        }
        else if(maxWidthHealth / healthBar.rect.width > 0.8)
        {
            healthBar.sizeDelta = new Vector2(maxWidthHealth, 10f);
            Destroy(gameObject);
        }
        else
        {
            healthBar.sizeDelta = new Vector2(healthBar.rect.width + heal, 10f);
            Destroy(gameObject);
        }

       
    }



}
