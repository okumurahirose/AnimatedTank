using UnityEngine;

public class BalletMove : CharacterBase
{   
    public GameObject PlayerObject;
    float LifeTime = 3.0f;
    float Timer;
    private Vector2 MoveDirection;
    private Vector2 MoveDistance;

    public void SetBallet()
    {   

        transform.eulerAngles = PlayerObject.transform.eulerAngles;
        MoveDirection.x = Mathf.Sin(-PlayerObject.transform.eulerAngles.z * Mathf.Deg2Rad);
        MoveDirection.y = Mathf.Cos(-PlayerObject.transform.eulerAngles.z * Mathf.Deg2Rad);
        
        GetComponent<Rigidbody2D>().totalTorque = 0.0f;
        GetComponent<Rigidbody2D>().totalForce = Vector2.zero;

        base.InitializeHP();
        Timer = 0.0f;
    }

    protected override void Update()
    {   
        base.Update();
        
        MoveDistance =  MoveDirection.normalized * base.Speed * Time.deltaTime;
        transform.position = base.currentPosition + MoveDistance;
       
        Timer += Time.deltaTime;
        if(Timer >= LifeTime) gameObject.SetActive(false);
    }   
}
