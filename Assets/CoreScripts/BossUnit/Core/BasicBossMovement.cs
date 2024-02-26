using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBossMovement : IBossMovement
{
    #region MainVariables
    private BossUnit _boss;
    private Vector3 _moveDirection; // �����, � ������� ����� ������� �����, �������� ��� ������ region MovementMethods
    #endregion

    #region PositionMiscVariables
    private Vector3 _destinationPoint;
    private float _rotationAngle;
    private float _rotationRadius;
    private float _distanceInRad;
    private float _rotateMod;
    private float _rotateDirection;
    private readonly Dictionary<string, int> movementMod = new()
    {
        ["noMovement"] = 0,
        ["infinity"] = 1,
        ["partly"] = 2,
    };
    #endregion

    // ���������� � FixedUpdate ������ BossUnit
    public void MovementUpdate()
    {
        //Gravity();
        AngleCorrection();

        if (Vector3.Distance(_destinationPoint, _boss.transform.position) > 0.1f)
        {
            _boss._controller.Move(_moveDirection);
        }
    }

    #region MovementMethods
    public void MoveBossToPoint(Vector3 point)
    {
        ResetVariables();

        _destinationPoint = point;
        Vector3 distance = _destinationPoint - _boss.transform.position;
        Vector3 pointMovement = _boss._movementSpeed * Time.deltaTime * distance.normalized;
        _boss.transform.LookAt(_destinationPoint);

        _moveDirection = new Vector3(pointMovement.x, _moveDirection.y, pointMovement.z);
    }

    public void MoveBossAroundPoint(Vector3 point, Dictionary<string, float> walkVariables)
    {
        ResetVariables();

        _destinationPoint = point;
        _rotationAngle = walkVariables["angle"];
        _rotationRadius = walkVariables["radius"];
        _distanceInRad = walkVariables["distanceInRad"];
        _rotateMod = walkVariables["rotateMod"];
        _rotateDirection = walkVariables["rotateDirection"];

        AngleCorrection();
    }
    #endregion

    #region UtilityMethods
    // ����������� ��������� ����� ��� ����������� ����� ��� �������� �� ����������
    private void AngleCorrection()
    {
        Debug.Log(_moveDirection / Time.deltaTime);
        // ���� ������ ����� ��������� �� �����, �� ���������� ����� � ������� ��������� ���������
        if ((_rotationAngle <= _distanceInRad && _rotateMod != movementMod["noMovement"]) || _rotateMod == movementMod["infinity"])
        {
            _rotationAngle += Time.deltaTime * _boss._movementSpeed * _rotateDirection;
            Vector3 pointAngle = new(Mathf.Cos(_rotationAngle) * _rotationRadius, _moveDirection.y, Mathf.Sin(_rotationAngle) * _rotationRadius);
            Vector3 pointDistance = _destinationPoint + pointAngle;
            _moveDirection = (pointDistance - _boss.transform.position) * Time.deltaTime;
            _boss.transform.LookAt(pointDistance);
        }
        // ����� ���� ��� �������� - partly (������ ������ ����� �����) � ������ ����� ����� ��� ���� �������� �� ���������� ��������
        else if (_rotateMod == movementMod["partly"])
        {
            ResetVariables();
        }
    }

    private void Gravity()
    {
        if (!_boss._controller.isGrounded)
        {
            _moveDirection.y -= _boss._gravity * Time.deltaTime;
        }
        else
        {
            _moveDirection.y = 0f;
        }
    }

    private void ResetVariables()
    {
        _moveDirection = Vector3.zero;
        _rotationAngle = 0f;
        _rotationRadius = 0f;
        _distanceInRad = 0f;
        _boss._movementSpeed = 1f;
        _rotateMod = 0f;
        _rotateDirection = 0f;
    }

    public void SetBossUnit(BossUnit boss)
    {
        _boss = boss;
    }
    #endregion
}
