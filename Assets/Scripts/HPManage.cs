using UnityEngine;

public class HPManage : MonoBehaviour
{
    public enum Identification{Player,Enemy,Ballet};

    public Identification identification;

    public float MaxHP;
    public float CurrentHP;

    [SerializeField] private float AttackDamage;

    void Start()
    {
        CurrentHP = MaxHP;
    }

    void Update()
    {
        if(CurrentHP <= 0.0f)
        {   
            if(identification == Identification.Ballet)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("死亡 : " + gameObject.name);
                Destroy(gameObject);
            }
            
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        HPManage TargetHPManage = collision.gameObject.GetComponent<HPManage>();

        if(identification == Identification.Player && TargetHPManage.identification == Identification.Ballet) return;
        if(identification == Identification.Ballet && TargetHPManage.identification == Identification.Player) return;
        if(identification == Identification.Enemy && TargetHPManage.identification == Identification.Enemy) return;

        TargetHPManage.TakeDamage(AttackDamage);
    }

    public void TakeDamage(float AttackDamage)
    {
        CurrentHP -= AttackDamage;
        Debug.Log("ダメージ" + AttackDamage);
    }
}
