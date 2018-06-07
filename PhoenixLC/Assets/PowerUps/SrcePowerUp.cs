using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SrcePowerUp : MonoBehaviour {

    public float rotateSpeed=10f;

    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().Repair();
            GameObject.Find("ScoreSistem").GetComponent<ScoreSistem>().UpdateScore(10);
            Destroy(gameObject);
        }
    }

}
