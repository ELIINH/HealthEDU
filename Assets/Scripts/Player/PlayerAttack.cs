using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttack : MonoBehaviour
{
    public Weapon weapon;
    public Sprite weaponIcon;
    private Animator anim;

    private NavMeshAgent playerAgent;
    public Transform sword;
    private Collider swordCollider;
    public float attackTime = 0.3f;

    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        anim = transform.Find("Character").GetComponent<Animator>();
        swordCollider = sword.GetComponent<Collider>(); 
        swordCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAgent.SetDestination(transform.position);
            swordAttack();
        }
    }

    public void LoadWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }
    public void LoadWeapon(ItemSO itemSO)
    {
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
            weapon = null;
        }

        string prefabName = itemSO.prefab.name;
        Transform weaponParent = transform.Find(prefabName + "Position");
        GameObject weaponGo = GameObject.Instantiate(itemSO.prefab);
        weaponGo.transform.parent = weaponParent;
        weaponGo.transform.localPosition = Vector3.zero;
        weaponGo.transform.localRotation = Quaternion.identity;

        this.weapon = weaponGo.GetComponent<Weapon>();
        this.weaponIcon = itemSO.icon;
        PlayerPropertyUI.Instance.UpdatePlayerPropertyUI();
    }
    public void UnloadWeapon()
    {
        weapon = null;
    }

    public void swordAttack()
    {
        anim.SetTrigger("IsAttack01");
        StartCoroutine(EnableColliderTemporarily());
    }
    private IEnumerator EnableColliderTemporarily()
    {
        swordCollider.enabled = true;
        yield return new WaitForSeconds(attackTime); 
        swordCollider.enabled = false;
    }
}
