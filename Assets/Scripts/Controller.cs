using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player controller
public class Controller : MonoBehaviour {

    //self explanatory
    private Rigidbody2D rb;
    //initial velocity of a jump
    [Range(1, 10)]
    public float jumpSpeed;
    //initial velocity of running
    [Range(1, 10)]
    public float runSpeed;
    //is the player not touching the ground? used to determine if the player may jump or not
    [SerializeField]
    private bool airborne = true;
    //vector for movement
    private Vector2 velocity = new Vector2();
    //did the player click the jump button? used to determine if the player may jump or not
    private bool jump = false;
    //horizontal axis magnitude
    private float horizontal;
    //acceleration due to gravity
    private float FALL_MULTIPLIER = 2.5f;
    //maximum health of player
    private float maxHP = 5;
    //current health of player
    public float currentHP;
    //is the player alive?
    private bool dead;
    //hit level end
    private bool levelEnd;
    

    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        currentHP = maxHP;
        dead = false;
        levelEnd = false;
    }

    void Update() {
        jump = Input.GetButton("Jump");
        horizontal = Input.GetAxisRaw("Horizontal");
        if(currHP <= 0) dead = true;
        if(dead) onDeath();
    }
    
    

    void FixedUpdate() {
        velocity.x = 0f;
        velocity.y = rb.velocity.y;
        if (canJump()) {
            velocity += Vector2.up * jumpSpeed;
            airborne = true;
        }

        velocity.x = horizontal * runSpeed;

        if (velocity.y < 0) velocity += Vector2.up * Physics2D.gravity.y * (FALLMULTIPLIER - 1) * Time.deltaTime;

        rb.velocity = velocity;
    }

    //when colliding with any game object
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Platform")) {

            //Debug.Log(col.gameObject);

            if (transform.position.y > col.transform.position.y) airborne = false;
        } else if (col.gameObject.CompareTag("Enemy")) {
            //currentHP -= ?ENEMY_DAMAGE?
        } else if (col.gameObject.CompareTag("ExitLadder")) {
            levelEnd = true;
        }
    }
    
    //when stop colliding with any game object
    void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.CompareTag("Platform")) {
            airborne = true;
        }
    }
    
    bool canJump() {
        return jump && !airborne;
    }
    
    
    void onDeath() {
    }
}
