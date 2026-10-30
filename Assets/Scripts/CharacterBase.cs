using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public enum Identification{Player,Enemy,Ballet};
    public Identification identification;
    protected Vector2 currentPosition;
    [SerializeField] protected float Speed;
    [SerializeField] protected float AttackDamage;
    
    [SerializeField] protected float MaxHP;
    protected float CurrentHP;

    public float CopyMaxHP => MaxHP;
    public float CopyCurrentHP => CurrentHP;

    protected virtual void Start()
    {
        InitializeHP();
    }

    protected virtual void Update()
    {
        currentPosition = new Vector2(transform.position.x,transform.position.y);
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

    public void InitializeHP()
    {
        CurrentHP = MaxHP;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0.0f;

        CharacterBase TargetCharacterBase = collision.gameObject.GetComponent<CharacterBase>();

        if(identification == Identification.Player && TargetCharacterBase.identification == Identification.Ballet) return;
        if(identification == Identification.Ballet && TargetCharacterBase.identification == Identification.Player) return;
        if(identification == Identification.Enemy && TargetCharacterBase.identification == Identification.Enemy) return;

        TakeDamage(TargetCharacterBase.AttackDamage);
    }

    public void TakeDamage(float AttackDamage)
    {
        CurrentHP -= AttackDamage;
        Debug.Log("ダメージ" + AttackDamage);
    }
}
