using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPiece : MonoBehaviour {
    public float aceleration, speed;
    private bool highPriority;
    private Vector3 position;

    void Start() {
        this.speed = 5;
        this.highPriority = false;
        this.position = this.transform.position;
    }

    void Update() {
        if(this.transform.position == this.position) this.highPriority = false;
        else this.highPriority = true;

        transform.position = Vector3.MoveTowards(this.transform.position, this.position, speed * Time.deltaTime);
    }

    private void SetHighPriority(bool state) {
        this.highPriority = state;
    }

    private void OnCollisionEnter(Collision other) {
        if(this.highPriority){
            Destroy(other.gameObject);
        }
    }

    public void MoveTo(Vector3 destiny) {
        this.position = destiny;
    }

    public void Select() {
        this.position = new Vector3(this.position[0], 1F, this.position[2]);
    }

    public void Unselect() {
        this.position = new Vector3(this.position[0], 0.4F, this.position[2]);
    }
}
