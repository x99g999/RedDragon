using UnityEngine;
using System.Collections;

public class ProcessPlayerAnimEvent : MonoBehaviour
{

    private PlayerController player;
    [SerializeField]
    private CapsuleCollider WeaponCollider;
    [SerializeField]
    private CapsuleCollider PlayerCollider;

    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    void AttackStart()
    {
        WeaponCollider.enabled = true;
    }

    public void AttackEnd()
    {
        WeaponCollider.enabled = false;
    }

    public void StateEnd()
    {
        
    }

    public void EndDamage()
    {
        
    }

    public void DiePlayer()
    {
        PlayerCollider.enabled = false;
    }

    public void RivivePlayer()
    {
        PlayerCollider.enabled = true;
    }
}
