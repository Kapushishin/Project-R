using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossMovement
{
    public void SetBossUnit(BossUnit boss);
    public void MoveBossToPoint(Vector3 point);
    public void MoveBossAroundPoint(Vector3 point, Dictionary<string, float> _variables);
    public void MovementUpdate();
}
