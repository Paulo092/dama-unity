using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {

    public Transform target;
    public float speed;

    void Start() {

    }

    void Update() {
        transform.RotateAround(target.position, transform.up, Input.GetAxis("Horizontal") * speed);
        transform.Translate(Input.GetAxis("Vertical") * speed * 0.1F, -Input.GetAxis("Vertical") * speed * 0.1F, 0);
    }
}
