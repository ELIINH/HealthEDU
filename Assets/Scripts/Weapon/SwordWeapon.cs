using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : MonoBehaviour
{
    public const string ANIM_PARM_ISATTACK = "IsAttack";

    private Animator anim;

    private int swordAttackValue = 10;
    public Transform player;

    private void Start()
    {
        anim = GetComponent<Animator>();
        //attackValue = player.GetComponent<PlayerProperty>().attackValue;
;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Attack();
    //    }
    //}

    public  void Attack()
    {
        anim.SetTrigger(ANIM_PARM_ISATTACK);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.ENEMY)
        {
            other.GetComponent<Enemy>().TakeDamage(swordAttackValue + player.GetComponent<PlayerProperty>().attackValue);
        }
    }
}
