using UnityEngine;

public class BalletMoving : MonoBehaviour
{   
    public GameObject PlayerObject;
    [SerializeField] private float BalletSpeed;

    float LifeTime = 3.0f;
    float Timer;

    Vector3 MoveDirection;

    public void SetBallet()
    {   
        transform.position = PlayerObject.transform.position;

        MoveDirection.x = Mathf.Sin(-PlayerObject.transform.eulerAngles.z * Mathf.Deg2Rad);
        MoveDirection.y = Mathf.Cos(-PlayerObject.transform.eulerAngles.z * Mathf.Deg2Rad);
        MoveDirection.z = 0;

        Timer = 0.0f;
    }

    void Update()
    {
        transform.Translate(MoveDirection * BalletSpeed * Time.deltaTime);

        Timer += Time.deltaTime;
        if(Timer >= LifeTime) gameObject.SetActive(false);
    }   
}
