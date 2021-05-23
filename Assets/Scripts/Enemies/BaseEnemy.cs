using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseEnemy {

    public float maxHP;
    public float currentHP;
    public float walkSpeed;
    public bool doesMove;

    public bool facingLeft;

    public BaseEnemy(float _maxHP, float _walkSpeed, bool _doesMove) {
        maxHP = _maxHP;
        currentHP = maxHP;
        walkSpeed = _walkSpeed;
        doesMove = _doesMove;
    }

    public void DoMove(Transform target) {

    }
}