using UnityEngine;

public class Foot : MonoBehaviour {

    private Lander lander;
    private Rigidbody2D rigidBody;

    void Start() {
        lander = GetComponentInParent<Lander>();
        rigidBody = transform.parent.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Piso") {
            lander.lose();
        } else {
            if (other.tag == "Base") {
                if (Mathf.Abs(rigidBody.velocity.y) > lander.maxSpeedToLand) {
                    lander.lose();
                }
                else {
                    lander.win();
                }
            }
        }
    }

}
