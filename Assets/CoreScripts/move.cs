using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    [SerializeField] private GameObject _point;
    [SerializeField] private float _speed = 1f;
    private CharacterController _characterController;

    [SerializeField] private float _elevationOffset = 0f;
    private Vector3 _positionOffset;
    [SerializeField] private float _rotationRadius = 1f;
    [SerializeField] private float _angularSpeed = 2f;
    private float _posX, _posZ, _angle = 0f;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        //MoveToPoint();
        MoveAroundPoint();
    }

    private void MoveAroundPoint()
    {
        _positionOffset.Set(Mathf.Cos(_angle) * _rotationRadius,
            _elevationOffset,
            Mathf.Sin(_angle) * _rotationRadius);
        transform.position = _point.transform.position + _positionOffset;
        //Vector3 distance = transform.position - _point.transform.position;
        //_characterController.Move((distance - _positionOffset) * Time.deltaTime);
        _angle += Time.deltaTime * _angularSpeed;
    }

    private void MoveToPoint()
    {
        Vector3 distanse = _point.transform.position - transform.position;
        if (distanse.magnitude > 0.1f)
        {
            distanse = distanse.normalized * _speed;
            _characterController.Move(distanse * Time.deltaTime);
        }
    }
}
