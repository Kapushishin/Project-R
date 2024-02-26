using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnit : MonoBehaviour
{
    #region Unit Stats
    // unit stats
    [SerializeField] public string _name = "Boss";
    [SerializeField] public float _damage = 2f;
    [SerializeField] public float _range = 2f;
    [SerializeField] public float _maxHealth = 10f;
    [SerializeField] public float _movementSpeed = 1f;
    [SerializeField] public float _gravity = 9.81f;
    [SerializeField] public Abilities _currentAbillity;
    #endregion

    #region Misc Variables
    [HideInInspector] public float _currentHealth;
    [HideInInspector] public PlayerUnit _playerUnit;
    [HideInInspector] public CharacterController _controller;
    #endregion

    #region Action Interfaces
    // actions interfaces
    public ITakeDamageBoss _takedamage;
    public IDoDamageBoss _dodamage;
    public IBossMovement _bossMovement;
    #endregion

    #region Initialization
    // ==============================
    //      Initialization Part
    // ==============================
    private void OnEnable()
    {
        InitUnitActions();
        InitMiscVariables();
    }

    public void InitUnitActions()
    {
        _takedamage = new BasicTakeDamageBoss();
        _dodamage = new BasicDoDamageBoss();
        _bossMovement = new BasicBossMovement();
    }

    public void InitMiscVariables()
    {
        _currentHealth = _maxHealth;
        _playerUnit = FindFirstObjectByType<PlayerUnit>();
        _controller = GetComponent<CharacterController>();
        _bossMovement.SetBossUnit(this);
    }
    #endregion

    #region Damage Part
    // ==============================
    //        Damage Part
    // ==============================
    public void TakeDamage(float damage)
    {
        _takedamage.OnTakeDamage(this, damage);
    }

    public void DoDamage()
    {
        _dodamage.OnDoDamage(this);
    }
    #endregion

    #region Actions Part
    // ==============================
    //        Actions Part
    // ==============================
    public void UseAbillity(Abilities abillity)
    {
        if (abillity)
        {
            _currentAbillity = abillity;
        }
        _currentAbillity.Use(this, _playerUnit);
    }

    private void FixedUpdate()
    {
        _bossMovement.MovementUpdate();
    }

    public void CoroutineStarter(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
    #endregion
}
