using UnityEngine;

public class Enemy_1 : EnemyBase
{
    protected override void Update()
    {   
        base.Update();
        base.ChasePlayer();
    }
}
