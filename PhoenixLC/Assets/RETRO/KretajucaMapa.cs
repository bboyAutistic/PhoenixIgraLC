using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KretajucaMapa : MonoBehaviour {

    public float brzinaKretanjeMape=2;
	void Update () {
        transform.position += transform.forward * Time.deltaTime * brzinaKretanjeMape;
    }
}
