using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkToPoint", menuName = "Actions/WalkToPoint")]
public class WalkToPoint : Actions
{
    #region InspectorVariables
    [SerializeField][Tooltip("����� �� �������������� player � �������� �����\ntrue - �����")]
    private bool _targetIsPlayer = false;
    [SerializeField][HideIf("_targetIsPlayer")][Tooltip("�����, � ������� ����� ����������� �����")]
    private Vector3 _targetPoint;
    [SerializeField][Tooltip("����������� �������� �����")][Range(0f, 5f)] 
    private float _speedModificator;
    #endregion

    #region Action
    public override void DoAction(BossUnit owner, PlayerUnit target)
    {
        Vector3 targerPosition = _targetPoint;
        if (_targetIsPlayer)
        {
            targerPosition = target.gameObject.transform.position;
        }
        owner._movementSpeed += owner._movementSpeed * _speedModificator;
        owner._bossMovement.MoveBossToPoint(targerPosition);
    }
    #endregion
}
