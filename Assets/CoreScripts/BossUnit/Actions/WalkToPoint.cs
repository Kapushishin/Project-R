using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkToPoint", menuName = "Actions/WalkToPoint")]
public class WalkToPoint : Actions
{
    #region InspectorVariables
    [SerializeField][Tooltip("Будет ли использоваться player в качестве точки\ntrue - будет")]
    private bool _targetIsPlayer = false;
    [SerializeField][HideIf("_targetIsPlayer")][Tooltip("Точка, в которую нужно переместить босса")]
    private Vector3 _targetPoint;
    [SerializeField][Tooltip("Модификатор скорости босса")][Range(0f, 5f)] 
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
