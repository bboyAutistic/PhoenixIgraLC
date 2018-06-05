using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    //laser public variables
    public GameObject laserBullet;
    public float rateOfFire = 120f;
    public Transform pointOfOrigin;

    //laser private variables
    float reloadTimer = 0f;

    //missile public variables
    public GameObject missile;
    public float lockOnRange = 50f;
    public float missileReloadTime = 5f;
    public float lockOnTime = 5f;

    //missile private variables
    float missileAmount = 0f;
    GameObject target = null;
    int lockTarget = 0;
    float nearestTarget;
    float missileReloadTimer = 0f;
    float lockTimer = 0f;
    bool lockOn = false;

    //lock-on UI variables
    public RectTransform targetLockUI;
    Color targetLockUIColor;

    void Awake()
    {
        targetLockUIColor = targetLockUI.GetComponent<Image>().color;
    }

    void Update()
    {
        if (Time.timeScale == 0) { return; }

        //laseri
        reloadTimer += Time.deltaTime;
        if (Input.GetMouseButton(0) && reloadTimer >= 60 / rateOfFire)
        {
            reloadTimer = 0;
            FireLaser();
        }

        //reload raketa i lock-on
        missileReloadTimer += Time.deltaTime;
        if (lockTimer > lockOnTime)
            lockOn = true;
        else
            lockOn = false;

        //update lock-on UI
        if (target != null)
        {
            targetLockUI.gameObject.SetActive(true);
            UpdateTargetLockUI();
        }
        else
        {
            targetLockUI.gameObject.SetActive(false);
        }

        //pucanje raketa
        if (Input.GetKeyDown(KeyCode.Mouse1) && target != null && missileReloadTimer > missileReloadTime && lockOn)
        {
            missileReloadTimer = 0f;
            FireMissile();
            missileAmount--;

            Debug.Log(missileAmount);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && target == null && missileReloadTimer > missileReloadTime && missileAmount > 0)
        {
            missileReloadTimer = 0f;
            Instantiate(missile, transform.position - transform.up, transform.rotation, null);
            missileAmount--;

            Debug.Log(missileAmount);
        }
    }

    void FireLaser()
    {
        GameObject bullet = Instantiate(laserBullet, pointOfOrigin.position, pointOfOrigin.rotation, null);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bullet.transform.forward * rb.mass * 300, ForceMode.Impulse);
    }

    void FireMissile()
    {
        GameObject firedMissile = Instantiate(missile, transform.position - transform.up, transform.rotation, null);
        firedMissile.GetComponent<Missile>().target = target;
    }

    //lock-on system
    /*
    void OnTriggerStay(Collider other)
    {
        //gledaj samo za Enemy
        if (other.CompareTag("Enemy"))
        {
            
        }

        //if target was found
        if (target != null)
        {
            //if target left the area, reset target
            if (Vector3.Angle(target.transform.position - transform.position, transform.forward) > 30f)
            {
                ResetTarget();
            }
        }
    }
    */

    public void LockOnSystem(Collider other)
    {
        if(missileAmount < 1)
        {
            ResetTarget();
            return;
        }
        Vector3 direction = other.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        //ako je unutar 30 stupnjeva
        if (angle <= 30f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, lockOnRange, LayerMask.NameToLayer("LaserBullets")))
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    //when no target, set a target
                    if (lockTarget == 0 || target == null)
                    {
                        lockTarget = hit.collider.gameObject.GetInstanceID();
                        nearestTarget = hit.distance;
                        lockTimer = 0f;
                        target = hit.collider.gameObject;
                    }

                    //if same target, add to timer, update distance
                    else if (lockTarget == hit.collider.gameObject.GetInstanceID())
                    {
                        lockTimer += Time.deltaTime;
                        nearestTarget = hit.distance;
                        if (target != hit.collider.gameObject)
                        {
                            target = hit.collider.gameObject;
                        }
                    }

                    //if new target is closer, change target
                    else if (lockTarget != hit.collider.gameObject.GetInstanceID() && hit.distance < nearestTarget)
                    {
                        lockTimer = 0f;
                        lockTarget = hit.collider.gameObject.GetInstanceID();
                        nearestTarget = hit.distance;
                    }

                }
                //if no line of sight, reset target
                else
                {
                    ResetTarget();
                }
            }
        }

        if (target != null)
        {
            //if target left the area, reset target
            if (Vector3.Angle(target.transform.position - transform.position, transform.forward) > 30f)
            {
                ResetTarget();
            }
        }
    }

    void ResetTarget()
    {
        lockTarget = 0;
        lockTimer = 0f;
        target = null;
    }

    void UpdateTargetLockUI()
    {
        targetLockUI.position = Camera.main.WorldToScreenPoint(target.transform.position);

        float size = 450 * (1-(lockTimer / lockOnTime)) + 50;
        size = Mathf.Clamp(size, 50f, 500f);

        targetLockUI.sizeDelta = new Vector2(size, size);
        if (lockOn)
        {
            targetLockUI.GetComponent<Image>().color = Color.red;
        }
        else
        {
            targetLockUI.GetComponent<Image>().color = targetLockUIColor;
        }
    }

    public void AddMissile()
    {
        missileAmount++;
        Debug.Log(missileAmount);
    }
}