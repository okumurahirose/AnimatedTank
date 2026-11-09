using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankMove : CharacterBase
{   
    [SerializeField] private Camera MainCamera;

    private Vector2 Direction;
    private Vector2 MoveDistance;

    private Vector2 MousePosition_Grobal;
    private Vector2 PositionDistance_ThisToMouse;

    float rotation_z;

    private int AnimationNumber;
    private bool IsTracing;

    protected override void Update()
    {   
        base.Update();
        if(IsTracing) return;
        if(Keyboard.current.tKey.wasPressedThisFrame){
            IsTracing = true;
            StartCoroutine(TraceRecord(AnimationNumber));
        }
        
        Direction = Vector2.zero;
        MousePosition_Grobal = ConvertScreenPositionToWorldPosition(Mouse.current.position.ReadValue());

        PositionDistance_ThisToMouse.x = MousePosition_Grobal.x - transform.position.x;
        PositionDistance_ThisToMouse.y = MousePosition_Grobal.y - transform.position.y;

        rotation_z = Mathf.Atan2(PositionDistance_ThisToMouse.y,PositionDistance_ThisToMouse.x) * Mathf.Rad2Deg - 90.0f;
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

        MoveDistance =  Direction.normalized * Speed * Time.deltaTime;

        transform.position = base.currentPosition + MoveDistance;
    }
    
    Vector2 ConvertScreenPositionToWorldPosition(Vector2 ScreenPosition)
    {
        Vector2 NomalizedScreenPosition;
        NomalizedScreenPosition.x = ScreenPosition.x / Screen.width * 2 - 1;
        NomalizedScreenPosition.y = ScreenPosition.y / Screen.height * 2 - 1;

        Vector2 LocalPosition;
        LocalPosition.x = NomalizedScreenPosition.x * MainCamera.orthographicSize * MainCamera.aspect;
        LocalPosition.y = NomalizedScreenPosition.y * MainCamera.orthographicSize;

        Vector2 WorldPosition;
        WorldPosition.x = LocalPosition.x + MainCamera.transform.position.x;
        WorldPosition.y = LocalPosition.y + MainCamera.transform.position.y;

        return WorldPosition;
    }

    IEnumerator TraceRecord(int AnimationNumber)
    {
        List<Vector3[]> TransformLog = AnimationStorage.Instance.TransformLogs[AnimationNumber];
        float RecordingTime = AnimationStorage.Instance.RecordingTimes[AnimationNumber];


        float Timer = 0.0f;
        int index = 0;
        Vector3 StartPosition = transform.position;
        Vector3 StartAngles = transform.eulerAngles;
        Vector3 StartScale = transform.localScale;
        Debug.Log("StartTrace");

        while(Timer < RecordingTime){
            if(index + 1 == TransformLog.Count) break;

            Vector3 TargetPosition = Quaternion.Euler(StartAngles - AnimationStorage.Instance.StartRecordAngles[AnimationNumber]) * TransformLog[index+1][0] + StartPosition;
            Vector3 TargetEulerAngles = TransformLog[index+1][1] + StartAngles;
            Vector3 TargetLocalScale = TransformLog[index+1][2] + StartScale;

            transform.position = TargetPosition;
            transform.eulerAngles = TargetEulerAngles;
            transform.localScale = TargetLocalScale;

            Timer += AnimationStorage.Instance.RecordingRate;
            index++;
            yield return new WaitForSeconds(AnimationStorage.Instance.RecordingRate);
        }

        IsTracing = false;
    }
}
