using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TnakShooting : MonoBehaviour
{
    [SerializeField] private BalletPooling balletPooling;
    
    [SerializeField] private int MaxBalletNum;
    private int CurrentBalletNum;
    [SerializeField] private float BalletReloadTime;
    

    void Start()
    {
        CurrentBalletNum = MaxBalletNum;
    }

    void Update()
    {   
        if(CurrentBalletNum <= 0) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {   
            Shoot();
        }

        if(CurrentBalletNum <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        GameObject target = balletPooling.LendBallet();
        target.SetActive(true);
        target.GetComponent<BalletMoving>().SetBallet();
        CurrentBalletNum--;
    }

    IEnumerator Reload()
    {   
        float Timer = 0.0f;
        while(Timer < BalletReloadTime)
        {
            Timer += Time.deltaTime;
            yield return null;
        }
        CurrentBalletNum = MaxBalletNum;
    }
}
