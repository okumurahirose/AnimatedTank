using UnityEngine;

public abstract class EnemyBase : CharacterBase
{
    protected GameObject PlayerObject;

    protected override void Start()
    {   
        base.Start();
        PlayerObject = GameObject.FindWithTag("Player");
    }

    protected void ChasePlayer()
    {
        Vector2 Direction = PlayerObject.transform.position - transform.position;
        Direction.Normalize();

        transform.position = currentPosition + Direction * base.Speed * Time.deltaTime;
    }
}
