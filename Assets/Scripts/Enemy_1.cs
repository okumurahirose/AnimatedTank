using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    [SerializeField] private GameObject PlayerObject;
    [SerializeField] private float Speed;

    void Update()
    {
        Vector3 Direction = PlayerObject.transform.position - transform.position;
        Direction.Normalize();

        transform.Translate(Direction * Speed * Time.deltaTime);
    }
}
