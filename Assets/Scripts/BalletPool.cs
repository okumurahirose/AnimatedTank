using System;
using System.Collections.Generic;
using UnityEngine;

public class BalletPool : MonoBehaviour
{
    [SerializeField] private GameObject PlayerObject;
    [SerializeField] private GameObject BalletObject;

    private List<GameObject> BalletList = new List<GameObject>();

    [SerializeField] private int FirstBalletNum;

    void Start()
    {
        for(int i = 0; i < FirstBalletNum; i++)
        {
            CreatNewBallet();
        }
    }

    public GameObject LendBallet()
    {
        foreach(GameObject target in BalletList)
        {
            if (!target.activeInHierarchy)
            {
                return target;
            }
        }

        return CreatNewBallet();
    }

    private GameObject CreatNewBallet()
    {
        GameObject target = Instantiate(
                                    BalletObject,
                                    transform.position,
                                    Quaternion.identity
                                );
            
            target.SetActive(false);
            target.GetComponent<BalletMove>().PlayerObject = PlayerObject;
            target.transform.SetParent(transform);
            BalletList.Add(target);
        
        return target;
    }
}
