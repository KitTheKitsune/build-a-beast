using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseEnemy {

    public float maxHP;
    public float currentHP;
    public float damage;
    public float walkSpeed;
    public bool doesMove;

    public bool facingLeft;

    public BaseEnemy(float _maxHP, float _walkSpeed, float _damage, bool _doesMove) {
        maxHP = _maxHP;
        currentHP = maxHP;
        damage = _damage;
        walkSpeed = _walkSpeed;
        doesMove = _doesMove;
    }
}
