using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RecodAnimation : MonoBehaviour
{
    private GameObject PlayerObject;
    public bool IsRecorded = false;

    public int StoreNumber;
    private List<Vector3[]> TransformLog = new List<Vector3[]>();

    public float RecordingTime;
    private float RecordingRate;


    void Start()
    {
        RecordingRate = AnimationStorage.Instance.RecordingRate;
        PlayerObject = GameObject.FindWithTag("Player");    
    }

    void Update()
    {
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            StartCoroutine(StartRecording());
        } 
    }

    public IEnumerator StartRecording()
    {
        IsRecorded = true;
        float Timer = 0.0f;
        int k = 0;

        Vector3 StartPosition = PlayerObject.transform.position;
        Vector3 StartAngles = PlayerObject.transform.eulerAngles;
        Vector3 StartScale = PlayerObject.transform.localScale;

        Debug.Log(StartPosition);

        Debug.Log("StartRecording");

        while (IsRecorded)
        {
            if(Timer < RecordingTime)
            {
                Vector3[] CurrentTransform = new Vector3[3];

                CurrentTransform[0] = PlayerObject.transform.position - StartPosition;
                CurrentTransform[1] = PlayerObject.transform.eulerAngles - StartAngles;
                CurrentTransform[2] = PlayerObject.transform.localScale - StartScale;
    
                TransformLog.Add(CurrentTransform);

                k++;
                Timer += RecordingRate;
                yield return new WaitForSeconds(RecordingRate);
            }
            else
            {
                IsRecorded = false;
                AnimationStorage.Instance.StoreAnimation(TransformLog,StartAngles,RecordingTime,StoreNumber);
                Debug.Log("EndRecording");
            }
        }
    }
}
