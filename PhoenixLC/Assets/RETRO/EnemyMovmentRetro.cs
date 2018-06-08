using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovmentRetro : MonoBehaviour {

    public float movmentSpeed=2f;
    bool endCorutine = true;
    Rigidbody rb;
    int randomBr=0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(MoveEnemy());
       
    }

    IEnumerator MoveEnemy()
    {
        while (endCorutine)
        {
            
            randomBr = Random.Range(1, 6);
            switch (randomBr)
            {
               
                case 1:
                    {
                        
                        rb.AddForce(transform.forward* movmentSpeed, ForceMode.Impulse);
                        
                        break;
                    }
                case 2:
                    {
                        rb.AddForce(-transform.forward* movmentSpeed, ForceMode.Impulse);
                        
                        break;
                    }
                case 3:
                    {
                        rb.AddForce(transform.right * movmentSpeed, ForceMode.Impulse);
                       
                        break;
                    }
                case 4:
                    {
                        rb.AddForce(-transform.right * movmentSpeed, ForceMode.Impulse);
                        
                        break;
                    }
                //stavio sam 2 put lijevo desno da se cesce krecu tako, znam da sam mogao nes bolje smisliti, ali ovo radi!!!
                case 5:
                    {
                        rb.AddForce(transform.right * movmentSpeed, ForceMode.Impulse);

                        break;
                    }
                case 6:
                    {
                        rb.AddForce(-transform.right * movmentSpeed, ForceMode.Impulse);

                        break;
                    }

            }

            float sekunda = Random.Range(0.75f, 1.5f);
            yield return new WaitForSeconds(sekunda);

        }

    }
}
