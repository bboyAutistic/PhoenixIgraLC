﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovementMP : NetworkBehaviour {

    public float moveSpeed = 7f;
    public float turnSpeed = 5f;
    public float pitchSpeedLimit = 3f;
    public float jawSpeedLimit = 3f;
    public float rollSpeedLimit = 3f;
    public float boostPercentage = 100f;
    public float boostTime = 10f;

    //boost
    public RectTransform boostBar;
    float maxHeight;

    Rigidbody rb;

    bool boost;
    float originalSpeed;
    float boostSpeed;
    float boostTimer;
    BoostUI boostUI;

    float verticalRotation;
    float horizontalRotation;
    float bankAngle;

    public GameObject canvas;

    public override void OnStartLocalPlayer()
    {
        GetComponentInChildren<Camera>().enabled = true;
        GetComponentInChildren<AudioListener>().enabled = true;
        GetComponentInChildren<Camera>().transform.SetParent(null);

        canvas.SetActive(true);
    }

    void Start()
    {
        if (!isLocalPlayer)
            return;

        rb = GetComponent<Rigidbody>();
        originalSpeed = moveSpeed;
        boostSpeed = moveSpeed * (1 + (boostPercentage / 100));
        boostTimer = boostTime;
        maxHeight = boostBar.rect.height;
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;


        boost = Input.GetKey(KeyCode.Space);
        if (boost && boostTimer > 0)
        {

            moveSpeed = boostSpeed;
            boostTimer -= Time.deltaTime;

        }
        else
        {

            moveSpeed = originalSpeed;
            if (boostTimer <= boostTime && !boost)
                boostTimer += Time.deltaTime / 2;
        }
        updateBoostBar(boostTimer / boostTime);

    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        //kretanje broda
        //rikverc je 25% brzine
        float fwd = Input.GetAxis("Vertical");
        if (fwd >= 0)
        {
            rb.AddForce(fwd * transform.forward * moveSpeed, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(fwd * transform.forward * moveSpeed * 0.25f, ForceMode.Impulse);
        }

        //strafing
        float strafe = Input.GetAxis("Bank");
        if (strafe != 0)
        {
            rb.AddForce(strafe * transform.right * moveSpeed * 0.25f, ForceMode.Impulse);
        }

        //rotacija broda
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        float z = Input.GetAxis("Horizontal");

        horizontalRotation = x * turnSpeed;
        verticalRotation = y * turnSpeed;
        bankAngle = z * turnSpeed;

        //limitacija brzine rotiranja, bez limita vrti se kolko brzo pomices mis, s limitima ima odredjenu maksmimalnu brzinu rotacije
        verticalRotation = Mathf.Clamp(verticalRotation, -pitchSpeedLimit, pitchSpeedLimit);
        horizontalRotation = Mathf.Clamp(horizontalRotation, -jawSpeedLimit, jawSpeedLimit);
        bankAngle = Mathf.Clamp(bankAngle, -rollSpeedLimit, rollSpeedLimit);

        transform.Rotate(-verticalRotation, 0f, 0f, Space.Self);
        transform.Rotate(0f, horizontalRotation, 0f, Space.Self);
        transform.Rotate(0f, 0f, -bankAngle, Space.Self);
    }

    public void updateBoostBar(float percent)
    {
        boostBar.sizeDelta = new Vector2(15f, maxHeight * percent);
    }

}
