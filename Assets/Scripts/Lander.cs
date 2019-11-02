using System.Collections;
using UnityEngine;

public class Lander : MonoBehaviour {

    private GameObject motorDerecho;
    private GameObject motorIzquierdo;
    private GameObject motorCentral;
    private Rigidbody2D rigidBody;
    private bool lost;
    private bool won;

    public float thrust=1;
    public float latThrust = 0.8F;
    public float maxSpeedToLand = 4f;
    public Transform baseTransform;
    public GameObject winObj;
    public GameObject loseObj;


    void Start () {
        motorDerecho = transform.Find("Derecha").gameObject;
        motorIzquierdo = transform.Find("Izquierda").gameObject;
        motorCentral = transform.Find("Centro").gameObject;
        rigidBody = GetComponent<Rigidbody2D>();
        reset();
    }

    public void win() {
        won = true;
        rigidBody.bodyType = RigidbodyType2D.Static;
        winObj.SetActive(true);
    }

    public void lose() {
        lost = true;
        rigidBody.bodyType = RigidbodyType2D.Static;
        loseObj.SetActive(true);
    }

    public void reset() {
        lost = false;
        won = false;
        loseObj.SetActive(false);
        winObj.SetActive(false);
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        transform.position = new Vector2(Random.Range(baseTransform.position.x - 8f, baseTransform.position.x + 8f), baseTransform.position.y + 7);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    public bool hasWon() {
        return won;
    }

    public bool hasLost() {
        return lost;
    }

    public void LeftThrust(bool active) {
        if (active) {
            rigidBody.AddRelativeForce(motorIzquierdo.transform.up * latThrust);
            StartCoroutine(ShowFire(motorIzquierdo));
        }
    }

    public void CentralThrust(bool active) {
        if (active) {
            rigidBody.AddRelativeForce(motorCentral.transform.up * thrust);
            StartCoroutine(ShowFire(motorCentral));
        }
    }

    public void RightThrust(bool active) {
        if (active) {
            rigidBody.AddRelativeForce(motorDerecho.transform.up * latThrust);
            StartCoroutine(ShowFire(motorDerecho));
        }
    }

    private IEnumerator ShowFire(GameObject engine) {
        engine.transform.Find("Fuego").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        engine.transform.Find("Fuego").gameObject.SetActive(false);
        yield break;
    }

    public Vector2 getVelocity() {
        return rigidBody.velocity;
    }

    public Vector2 getDistanceToBase() {
        Vector3 distance = transform.position - baseTransform.position;
        return new Vector2(distance.x, distance.y);
    }

    void Update() {
        LeftThrust(Input.GetKey(KeyCode.LeftArrow));
        CentralThrust(Input.GetKey(KeyCode.UpArrow));
        RightThrust(Input.GetKey(KeyCode.RightArrow));
        if (Input.GetKeyDown(KeyCode.Space)) {
            reset();
        }
    }
}
