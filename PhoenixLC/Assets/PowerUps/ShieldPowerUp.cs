using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour {
    public RectTransform shieldBar;
    
    public float rotateSpeed = 10f;

    float maxWidthShield;

    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }
    void Awake()
    {

        maxWidthShield = shieldBar.rect.width;

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
        if (maxWidthShield / shieldBar.rect.width == 1)
        {
            return;
        }
        else 
        {
            shieldBar.sizeDelta = new Vector2(maxWidthShield, 10f);
            Destroy(gameObject);
        }
        

    }
}
