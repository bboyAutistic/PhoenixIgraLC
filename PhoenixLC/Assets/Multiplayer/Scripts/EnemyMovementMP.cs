using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementMP : MonoBehaviour {

    [SerializeField]
    Transform target;
    [SerializeField]
    float rotationalDamp = 0.5f;

    [SerializeField]
    float movementSpeed = 10f;

    float targetTimer = 0f;

    void Update()
    {
        targetTimer += Time.deltaTime;
        if (target == null)
        {
            if (!FindTarget())
                return;
        }
        else if (targetTimer >= 10f)
        {
            targetTimer = 0f;
            if (!FindTarget())
                return;
        }
        Turn();
        Move();

    }



    void Turn()
    {
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation,  rotationalDamp*Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
    }
    void Move()
    {
        //provjerava jel u X(br) polja blizini, ako je usporva, ako ne ubrzava
        if ((target.transform.position - this.transform.position).sqrMagnitude < 5 * 5)
        {

            //Debug.Log("usporava");
            transform.position += transform.forward * Time.deltaTime * movementSpeed * 0.01f;

        }
        else if ((target.transform.position - this.transform.position).sqrMagnitude < 30 * 30)
        {

            //Debug.Log("usporava");
            transform.position += transform.forward * Time.deltaTime * movementSpeed * 0.4f;

        }
        else
        {

            //Debug.Log("brzooo");
            transform.position += transform.forward * Time.deltaTime * movementSpeed * 1.2f;
        }
    }

    bool FindTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < targets.Length; i++)
        {
            list.Add(targets[i]);
        }
        list.Sort((e, x) => Vector3.Distance(e.transform.position, transform.position).CompareTo(Vector3.Distance(x.transform.position, transform.position)));

        GameObject temp;
        if (list.Count != 0)
            temp = list[0];
        else
            temp = null;
        if (temp == null)
            return false;

        target = temp.transform;
        return true;
    }
}
