using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    private Rigidbody2D rb;
    [Range(1, 10)]
    public float jumpSpeed;
    [Range(1, 10)]
    public float runSpeed;
    [SerializeField]
    private bool airborne = true;
    private Vector2 velocity = new Vector2();
    private bool jump = false;
    private float horizontal;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update() {
        jump = Input.GetButton("Jump");
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate() {
        velocity.x = 0f;
        velocity.y = rb.velocity.y;
        if (jump && !airborne) {
            velocity += Vector2.up * jumpSpeed;
            airborne = true;
        }

        velocity.x = horizontal * runSpeed;

        if (velocity.y < 0) velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        rb.velocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Platform")) {

            Debug.Log(col.gameObject);

            if (transform.position.y > col.transform.position.y) airborne = false;
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.CompareTag("Platform")) {
            airborne = true;
        }
    }
}