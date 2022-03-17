using UnityEngine;
using System.Collections;

public class ProcessEnemyAnimEvent : MonoBehaviour
{

    private EnemyController enemy;
    [SerializeField]
    private CapsuleCollider capsuleCollider;
   
    void Start()
    {
        enemy = GetComponent<EnemyController>();
    }

    void AttackStart()
    {
        capsuleCollider.enabled = true;
     
    }

    public void AttackEnd()
    {
        capsuleCollider.enabled = false;
      
    }

    public void StateEnd()
    {
        enemy.SetState(EnemyController.EnemyState.Freeze);
    }

    public void EndDamage()
    {
        enemy.SetState(EnemyController.EnemyState.Walk);
    }

    public void DieEnemy()
    {
        Destroy(this.gameObject);
    }

    public void Die()
    {
        capsuleCollider.enabled = false;
    }
}
