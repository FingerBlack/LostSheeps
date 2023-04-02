using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTurret : Turret
{
    //public bool ifmove; ??
    protected float buffValue;
    protected GameObject upRightBox;
    protected GameObject downRightBox;
    protected GameObject upLeftBox;
    protected GameObject downLeftBox;
    protected BulletType buffBulletType;

    protected override void Init()
    {
        base.Init();

        // varying initialization
        buffValue=0.3f;
        buffBulletType = BulletType.Normal;
        // fixed initialization
    }

    protected override void GetLocalPosition()
    {
        base.GetLocalPosition();

        upRightBox = base.GetBox(map.GetCellCenterWorld(gridPosition + new Vector3Int(1, 1, 0)));
        downRightBox = base.GetBox(map.GetCellCenterWorld(gridPosition + new Vector3Int(1, -1, 0)));
        upLeftBox = base.GetBox(map.GetCellCenterWorld(gridPosition + new Vector3Int(-1, 1, 0)));
        downLeftBox = base.GetBox(map.GetCellCenterWorld(gridPosition + new Vector3Int(-1, -1, 0)));
    }

    protected virtual void BuffSurrounding()
    {
        AddBuff(upBox);
        AddBuff(downBox);
        AddBuff(leftBox);
        AddBuff(rightBox);
        AddBuff(upRightBox);
        AddBuff(downRightBox);
        AddBuff(upLeftBox);
        AddBuff(downLeftBox);
    }

    protected virtual void AddBuff(GameObject gameObject)
    {
        if(gameObject && gameObject.transform.childCount==1){
            if(gameObject.transform.GetChild(0).gameObject.TryGetComponent<AttackTurret>(out AttackTurret attackTurret)){
                if(attackTurret.bulletBuffTimer < buffValue){
                    attackTurret.bulletBuffTimer = buffValue;
                    attackTurret.buffBullet(buffBulletType);
                }
            }
        }
    }
}
