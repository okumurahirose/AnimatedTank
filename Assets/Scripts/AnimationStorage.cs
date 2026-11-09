using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationStorage : MonoBehaviour
{
    public static AnimationStorage Instance;

    public List<Vector3[]>[] TransformLogs = new List<Vector3[]>[5];
    public Vector3[] StartRecordAngles = new Vector3[5];
    public float[] RecordingTimes;
    public float RecordingRate = 0.025f;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StoreAnimation(List<Vector3[]> TransformLog,Vector3 StartAngle,float RecordingTime,int StoreNumber)
    {
        TransformLogs[StoreNumber] = TransformLog;
        StartRecordAngles[StoreNumber] = StartAngle;
        RecordingTimes[StoreNumber] = RecordingTime;
    }

}
