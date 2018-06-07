using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BossRotationMP : NetworkBehaviour {

    public float speed = 1f;

    void Update()
    {
        if (!isServer)
            return;

        transform.Rotate(0f, speed * Time.deltaTime, 0f);
    }
}
