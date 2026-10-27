using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HPManaging : MonoBehaviour
{
    public enum Identification{Player,Enemy,Ballet};

    public Identification identification;

    [SerializeField] private float MaxHP;
    private float CurrentHP;

    [SerializeField] private float AttackDamage;

    void Start()
    {
        CurrentHP = MaxHP;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        HPManaging TargetHPManaging = collision.gameObject.GetComponent<HPManaging>();

        if(identification == Identification.Player && TargetHPManaging.identification == Identification.Enemy)
        {
            TargetHPManaging.TakeDamage(AttackDamage);
        }
        else if(identification == Identification.Enemy && TargetHPManaging.identification == Identification.Player)
        {
            TargetHPManaging.TakeDamage(AttackDamage);
        }
        else if(identification == Identification.Ballet && TargetHPManaging.identification == Identification.Enemy)
        {
            TargetHPManaging.TakeDamage(AttackDamage);
        }
    }

    public void TakeDamage(float AttackDamage)
    {
        CurrentHP -= AttackDamage;
        Debug.Log("ダメージ" + AttackDamage);
    }
}
