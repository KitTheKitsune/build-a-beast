using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Walker : MonoBehaviour {

    public BaseEnemy walker;
    public Transform target;

    public bool canSeeTarget;

    private float turnTimer = 2f;
    private float timeUntilTurn = 2f;
    private Rigidbody2D rb;

    private void Start() {
        walker = new BaseEnemy(1f, 3f, true);
        target = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        RaycastHit2D hit = facingLeft ? Physics2D.Raycast(transform.position, Vector2.left) : Physics2D.Raycast(transform.position, -Vector2.left);

        if (hit.collider != null & hit.collider.gameObject.CompareTag("Player")) {
            canSeeTarget = true;
        } else canSeeTarget = false;
    }

    private void FixedUpdate() {
        if(canSeeTarget) Transform.
    }
}