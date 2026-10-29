using UnityEngine;

public class BalletMove : MonoBehaviour
{   
    public GameObject PlayerObject;
    [SerializeField] private HPManage MyHPManage;
    [SerializeField] private float BalletSpeed;

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

        MyHPManage.CurrentHP = MyHPManage.MaxHP;
        Timer = 0.0f;
    }

    void Update()
    {   
        
        MoveDistance =  MoveDirection.normalized * BalletSpeed * Time.deltaTime;
        Vector2 currentposition = new Vector2(transform.position.x,transform.position.y);
        transform.position = currentposition + MoveDistance;
       
        Timer += Time.deltaTime;
        if(Timer >= LifeTime) gameObject.SetActive(false);
    }   
}
