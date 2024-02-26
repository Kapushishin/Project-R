using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkAroundPoint", menuName = "Actions/WalkAroundPoint")]
public class WalkAroundPoint : Actions
{
    #region InspectorVariables
    [SerializeField][Tooltip("����� �� �������������� player � �������� �����\ntrue - �����")]
    private bool _targetIsPlayer = false;
    [SerializeField][HideIf("_targetIsPlayer")][Tooltip("�����, � ������� ����� ����������� �����")]
    private Vector3 _targetPoint;
    [SerializeField][Tooltip("�������������� ���������� ��������� ����� ������ � �������")][Range(0f, 5f)]
    private float _distanceOffset = 0.5f;
    [SerializeField][Tooltip("����� �����, ������� ����� ������ ����� \n1 - ������ ������ ����\n8 - ������ ������� ����� �����")][Range(1, 8)]
    private int _partOfCircle = 1;
    [SerializeField][Tooltip("����������� �������� �����")][Range(0f, 5f)]
    private float _speedModificator;
    [SerializeField][Tooltip("0 - ���������� �������� \n1 - ����������� �������� �� ����� \n2 - ������ ������ ����� �����")][Range(0, 2)]
    private int _rotateMod = 0;
    [SerializeField][Tooltip("� ����� ������� ���������\n-1 - ������\n0 - ������\n1 - �������")][Range(-1, 1)]
    private int _rotateDirection = 1;
    #endregion

    #region Action
    public override void DoAction(BossUnit owner, PlayerUnit target)
    {
        Vector3 targerPosition = _targetPoint;
        if (_targetIsPlayer)
        {
            targerPosition = target.gameObject.transform.position;
        }
        
        Vector3 bossPosition = owner.gameObject.transform.position;
        owner._movementSpeed += owner._movementSpeed * _speedModificator;
        Vector3 direction = (bossPosition - targerPosition);
        float angle = Mathf.Atan2(direction.y, direction.x);

        Dictionary<string, float> walkVariables = new()
        {
            ["angle"] = angle,
            ["radius"] = Vector3.Distance(targerPosition, bossPosition) + _distanceOffset,
            ["distanceInRad"] = angle + Mathf.Deg2Rad * 360 / _partOfCircle,
            ["rotateMod"] = (float)_rotateMod,
            ["rotateDirection"] = _rotateDirection
        };

        owner._bossMovement.MoveBossAroundPoint(targerPosition, walkVariables);
    }
    #endregion
}
