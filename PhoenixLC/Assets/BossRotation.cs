﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotation : MonoBehaviour {

    public float speed=1f;
	
	void Update () {
        transform.Rotate(0f, speed * Time.deltaTime, 0f);
	}
}
