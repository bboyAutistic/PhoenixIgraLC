using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissilePowerUp : MonoBehaviour {

    public float rotateSpeed = 10f;
    
    

    void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime,0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Shooting>().AddMissile();
            Destroy(gameObject);

        }
    }

}
