using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy {

    public float maxHP;
    public float currentHP;
    public float walkSpeed;
    public bool doesMove;

    public bool facingLeft;
    public Transform target;

    public Enemy (float _maxHP, float _walkSpeed, bool _doesMove) {
        maxHP = _maxHP;
        currentHP = maxHP;
        walkSpeed = _walkSpeed;
        doesMove = _doesMove;

        target = GameObject.Find("Player").transform;
    }

    public void GetTarget() {
        RaycastHit2D hit = facingLeft ? Physics2D.Raycast(transform.position, Vector2.left) : Physics2D.Raycast(transform.position, -Vector2.left);
    }

    public void DoMove() {

    }
}