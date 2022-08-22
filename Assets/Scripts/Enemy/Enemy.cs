using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform targetPos;
    [SerializeField] private int damageFireTower = 10;
    [SerializeField] private int DamagePlayer = 30;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private float speed = 10f;
    
    private GameObject playerPos;
    private Animator _animator;
    private NavMeshAgent navMesh;
    private ObjectPool objectPool;
    private bool canWalk = true;
    
    private void OnDisable()
    {
        objectPool.ReturnGameObject(this.gameObject);
    }
    
    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        navMesh = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        playerPos = FindObjectOfType<PLayerAttack>().gameObject;
    }

    private void Update()
    {
        float distance = Vector3.Distance(playerPos.transform.position, this.transform.position);

        if (distance < attackRadius)
        {
            transform.LookAt(playerPos.transform);
            _animator.SetTrigger("Attack");
            navMesh.speed = 0;
        }
        else 
        {
            EnemySetDestination(targetPos.position);
        }
    }

    private void EnemySetDestination(Vector3 pos)
    {
        navMesh.SetDestination(pos * speed * Time.deltaTime);
    }

    //attack player 
    public void DamageEnemy()
    {
        playerPos.GetComponent<Health>().DamageEnemy(DamagePlayer);
    }
    
    
    //Enter FireTower
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out HelthFire helthFire) && other.gameObject != null)
        {
            helthFire.HealthFire.Invoke(damageFireTower);
            FindObjectOfType<SliderHealth>().sliderHealth.Invoke(helthFire.currentHealth);
            this.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}
