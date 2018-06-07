using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPowerUp : MonoBehaviour {

    public float rotateSpeed = 10f;

    void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime, 0f, 0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<Shooting>().laserLevel < 5)
            other.GetComponent<Shooting>().LaserLevelUp();
            GameObject.Find("ScoreSistem").GetComponent<ScoreSistem>().UpdateScore(10);
            Destroy(gameObject);
        }
    }
}
