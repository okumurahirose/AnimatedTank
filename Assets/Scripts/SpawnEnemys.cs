using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnEnemys : MonoBehaviour
{   
    [SerializeField] private GameObject[] EnemyObjects;
    [SerializeField] private int[] EachEnemyAmounts;

    [SerializeField] GameObject[] SpawnPoint;

    void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            GenerateEnemy(EnemyObjects[0]);
        }
    }

    void GenerateEnemy(GameObject Target)
    {
        Instantiate(
            Target,
            SpawnPoint[Random.Range(0,SpawnPoint.Length)].transform.position,
            Quaternion.identity
        );
    
    }
}
