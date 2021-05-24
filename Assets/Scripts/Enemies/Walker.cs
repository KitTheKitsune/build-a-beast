using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Walker : MonoBehaviour {

    //keeps track of health and stats
    public BaseEnemy walker;
    //always looking for player
    public Transform target;
    //line of sight needs to be raised
    public Vector3 raycastOffset = new Vector3(0f, 0.25f, 0f);
    //self explanatory
    public bool canSeeTarget;
    //amount of time before entity turns around
    private float turnTimer = 2f;
    private float timeUntilTurn = 2f;
    private Rigidbody2D rb;

    //instantiate initial fields
    private void Start() {
        walker = new BaseEnemy(1f, 3f, 1f, true);
        target = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    //is called every frame
    private void Update() {
        //literal line of sight
        RaycastHit2D hit = walker.facingLeft ? Physics2D.Raycast(transform.position + raycastOffset, transform.position + raycastOffset + Vector3.left * 10f / 0.4f) : Physics2D.Raycast(transform.position + raycastOffset, transform.position + raycastOffset - Vector3.left * 10f / 0.4f);

        //if there is something in line of sight
        if (hit.transform != null) {
            //Debug.Log(hit.transform.tag);
            //if that thing is the player
            if (hit.transform.tag == "Player") canSeeTarget = true;
        } else canSeeTarget = false;
        //while the target is unseen, turn on the timer
        if(!canSeeTarget) timeUntilTurn -= Time.deltaTime;
        //turn on timer
        if(timeUntilTurn <= 0f) {
            //turn around
            walker.facingLeft = !walker.facingLeft;
            //reset timer
            timeUntilTurn = turnTimer;
        }
    }

    //is called based on set time
    private void FixedUpdate() {
        //speed of this
        Vector2 velocity;
        
        //change horizontal velocity but maintain vertical velocity.
        velocity.x = 0;
        velocity.y = rb.velocity.y;
        
        //set velocity relative to target if they are seen
        if (canSeeTarget) velocity += (walker.facingLeft) ? Vector2.left * walker.walkSpeed : Vector2.right * walker.walkSpeed;
        //move this
        rb.velocity = velocity;
    }

    //draw a red line to view line of sight
    private void OnDrawGizmos () {
        if(walker != null) {
            Gizmos.color = Color.red;
            if (walker.facingLeft) Gizmos.DrawLine(transform.position, transform.position + Vector3.left * 10f);
            else Gizmos.DrawLine(transform.position, transform.position - Vector3.left * 10f);
        }
    }
}
