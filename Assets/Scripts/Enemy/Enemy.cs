//This is the main enemy behavior control script
//contains the enemy state machine, movement, attack, and death logic.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //Normal state: random movement(MovingState) + RestingState
    //FightingState
    public enum EnemyState
    {
        NormalState,
        FightingState,
        MovingState,
        RestingState
    }

    private EnemyState state = EnemyState.NormalState;
    private EnemyState childState = EnemyState.RestingState;
    private NavMeshAgent enemyAgent;

    public float restTime = 2;
    private float restTimer = 0;

    public EnemyProperty enemyProperty; 

    private float attackTimer = 0;
    private Transform player;
    private Animator animator;

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyProperty = GetComponent<EnemyProperty>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (state == EnemyState.NormalState)
        {
            DetectPlayer();
            if (childState == EnemyState.RestingState)
            {
                restTimer += Time.deltaTime;

                if (restTimer > restTime)
                {
                    Vector3 randomPosition = FindRandomPosition();
                    enemyAgent.SetDestination(randomPosition);
                    childState = EnemyState.MovingState;
                }
            }
            else if (childState == EnemyState.MovingState)
            {
                if (enemyAgent.remainingDistance <= 0)
                {
                    restTimer = 0;
                    childState = EnemyState.RestingState;
                }
            }
        }
        else if (state == EnemyState.FightingState)
        {
            if (player != null)
            {
                enemyAgent.SetDestination(player.position);
                if (Vector3.Distance(transform.position, player.position) <= enemyProperty.attackRange)
                {
                    attackTimer += Time.deltaTime;
                    if (attackTimer >= enemyProperty.attackCooldown)
                    {
                        AttackPlayer();
                        attackTimer = 0;
                    }
                }
            }
            else
            {
                state = EnemyState.NormalState;
            }
        }
    }

    Vector3 FindRandomPosition()
    {
        Vector3 randomDir = new Vector3(Random.Range(-1, 1f), 0, Random.Range(-1, 1f));
        return transform.position + randomDir.normalized * Random.Range(2, 5);
    }

    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, enemyProperty.detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                player = hitCollider.transform;
                state = EnemyState.FightingState;
                break;
            }
        }
    }

    void AttackPlayer()
    {
        animator.SetTrigger("IsAttack");
        PlayerProperty playerComponent = player.GetComponent<PlayerProperty>();
        if (playerComponent != null)
        {
            playerComponent.TakeDamage(enemyProperty.attackValue);
        }
    }

    public void TakeDamage(int damage)
    {
        enemyProperty.HP -= damage;
        //Debug.Log("Enemy HP: " + enemyProperty.HP);
        if (enemyProperty.HP <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        animator.SetTrigger("IsDead");
        GetComponent<Collider>().enabled = false;
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        for (int i = 0; i < enemyProperty.pickableCount; i++)
        {
            SpawnPickableItem();
        }
        EventCenter.EnemyDied(this);
        Destroy(this.gameObject);
    }
    private void SpawnPickableItem()
    {
        ItemSO item = ItemDBManager.Instance.GetRandomItem();
        GameObject go = GameObject.Instantiate(item.prefab, transform.position, Quaternion.identity);
        go.tag = Tag.INTERACTABLE;
        Animator anim = go.GetComponent<Animator>();
        if (anim != null)
        {
            anim.enabled = false;
        }
        PickableObject po = go.AddComponent<PickableObject>();
        po.itemSO = item;

        Collider collider = go.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
            collider.isTrigger = false;
        }
        Rigidbody rgd = go.GetComponent<Rigidbody>();
        if (rgd != null)
        {
            rgd.isKinematic = false;
            rgd.useGravity = true;
        }
    }
}
