using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkAroundPoint", menuName = "Actions/WalkAroundPoint")]
public class WalkAroundPoint : Actions
{
    #region InspectorVariables
    [SerializeField][Tooltip("Будет ли использоваться player в качестве точки\ntrue - будет")]
    private bool _targetIsPlayer = false;
    [SerializeField][HideIf("_targetIsPlayer")][Tooltip("Точка, в которую нужно переместить босса")]
    private Vector3 _targetPoint;
    [SerializeField][Tooltip("Дополнительное увеличение дистанции между боссом и игроком")][Range(0f, 5f)]
    private float _distanceOffset = 0.5f;
    [SerializeField][Tooltip("Часть круга, которую нужно пройти боссу \n1 - пройти полный круг\n8 - пройти восьмую часть круга")][Range(1, 8)]
    private int _partOfCircle = 1;
    [SerializeField][Tooltip("Модификатор скорости босса")][Range(0f, 5f)]
    private float _speedModificator;
    [SerializeField][Tooltip("0 - отсутствие движения \n1 - бесконечное движение по кругу \n2 - пройти только часть круга")][Range(0, 2)]
    private int _rotateMod = 0;
    [SerializeField][Tooltip("В какую сторону двигаться\n-1 - налево\n0 - никуда\n1 - направо")][Range(-1, 1)]
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
