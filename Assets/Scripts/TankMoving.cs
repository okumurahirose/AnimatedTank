using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankMoving : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] private Camera MainCamera;

    [SerializeField] private float Speed;

    private Vector2 Direction;
    private Vector2 MoveDistance;

    private Vector2 MousePosition_Grobal;
    private Vector2 PositionDistance_ThisToMouse;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {   
        Direction = Vector2.zero;
        MousePosition_Grobal = ConvertScreenPositionToWorldPosition(Mouse.current.position.ReadValue());

        PositionDistance_ThisToMouse.x = MousePosition_Grobal.x - transform.position.x;
        PositionDistance_ThisToMouse.y = MousePosition_Grobal.y - transform.position.y;

        float rotation_z = -Mathf.Atan2(PositionDistance_ThisToMouse.x,PositionDistance_ThisToMouse.y) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0,0,rotation_z);

        if (Keyboard.current.wKey.isPressed)
        {   
            Direction.x = Mathf.Sin(-transform.eulerAngles.z * Mathf.Deg2Rad);
            Direction.y = Mathf.Cos(-transform.eulerAngles.z * Mathf.Deg2Rad);
        }
        if (Keyboard.current.sKey.isPressed)
        {
            Direction.x = -Mathf.Sin(-transform.eulerAngles.z * Mathf.Deg2Rad);
            Direction.y = -Mathf.Cos(-transform.eulerAngles.z * Mathf.Deg2Rad);
        }
        if (Keyboard.current.dKey.isPressed)
        {
            Direction.x = Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
            Direction.y = Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad);
        }
        if (Keyboard.current.aKey.isPressed)
        {
            Direction.x = -Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
            Direction.y = -Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad);
        }


        MoveDistance =  Direction * Speed * Time.deltaTime;

        characterController.Move(MoveDistance);
    }

    Vector2 ConvertScreenPositionToWorldPosition(Vector2 ScreenPosition)
    {
        Vector2 NomalizedScreenPosition;
        NomalizedScreenPosition.x = ScreenPosition.x / Screen.width * 2 - 1;
        NomalizedScreenPosition.y = ScreenPosition.y / Screen.height * 2 - 1;

        Vector2 LocalPosition;
        LocalPosition.x = NomalizedScreenPosition.x * MainCamera.orthographicSize * 2 * MainCamera.aspect;
        LocalPosition.y = NomalizedScreenPosition.y * MainCamera.orthographicSize * 2;

        Vector2 WorldPosition;
        WorldPosition.x = LocalPosition.x + MainCamera.transform.position.x;
        WorldPosition.y = LocalPosition.y + MainCamera.transform.position.y;

        return WorldPosition;
    }
}
