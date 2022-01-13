using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character",fileName = "CharacterBase")]
public class CharacterProperty : ScriptableObject
{
    [SerializeField] string _CharacterName;
    [SerializeField] int _health;
    [SerializeField] int _attack;
    [SerializeField] float _attackRange;
    [SerializeField] int _energy;
    [SerializeField] int _burstEnergy;
    [SerializeField] int _moveSpeed;

    public string CharacterName {
        get {
            return _CharacterName;
        }
    }

    public int Health {
        get {
            return _health;
        }
    }

    public int Attack {
        get {
            return _attack;
        }
    } 

    public float AttackRange {
        get {
            return _attackRange;
        }
    }

    public int Energy {
        get {
            return _energy;
        }
    }

    public int BurstEnergy {
        get {
            return _burstEnergy;
        }
    }

    public int MoveSpeed {
        get {
            return _moveSpeed;
        }
    }
}
