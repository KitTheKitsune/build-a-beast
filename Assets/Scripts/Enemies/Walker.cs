using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a comment. Are you happy now?

[RequireComponent(typeof(Rigidbody2D))]
public class Walker : MonoBehaviour {

    public BaseEnemy walker;
    public Transform target;

    public Vector3 raycastOffset = new Vector3(0f, 0.25f, 0f);

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
        RaycastHit2D hit = walker.facingLeft ? Physics2D.Raycast(transform.position + raycastOffset, transform.position + raycastOffset + Vector3.left * 10f / 0.4f) : Physics2D.Raycast(transform.position + raycastOffset, transform.position + raycastOffset - Vector3.left * 10f / 0.4f);

        if (hit.transform != null) {
            Debug.Log(hit.transform.tag);
            if (hit.transform.tag == "Player") canSeeTarget = true;
        } else canSeeTarget = false;

        if(!canSeeTarget) timeUntilTurn -= Time.deltaTime;
        if(timeUntilTurn <= 0f) {
            walker.facingLeft = !walker.facingLeft;
            timeUntilTurn = turnTimer;
        }
    }

    private void FixedUpdate() {
        Vector2 velocity;
        velocity.x = 0;
        velocity.y = rb.velocity.y;
        if (canSeeTarget) velocity += (walker.facingLeft) ? Vector2.left * walker.walkSpeed : Vector2.right * walker.walkSpeed;

        rb.velocity = velocity;
    }

    private void OnDrawGizmos () {
        if(walker != null) {
            Gizmos.color = Color.red;
            if (walker.facingLeft) Gizmos.DrawLine(transform.position, transform.position + Vector3.left * 10f);
            else Gizmos.DrawLine(transform.position, transform.position - Vector3.left * 10f);
        }
    }
}