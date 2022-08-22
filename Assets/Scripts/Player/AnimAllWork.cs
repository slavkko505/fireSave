using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class AnimAllWork : MonoBehaviour
{
    private Animator _animator;
    private StarterAssets.StarterAssetsInputs _inputs;

    private StarterAssets.StarterAssets _inventoryInput;
    
    [SerializeField] private PLayerAttack _pLayerAttack;
    
    
    void Awake()
    {
        _inventoryInput = new StarterAssets.StarterAssets();
    }

    private void OnEnable()
    {
        _inventoryInput.Enable();
    }

    private void OnDisable()
    {
        _inventoryInput.Disable();
    }
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _inputs = GetComponent<StarterAssets.StarterAssetsInputs>();
    }
    

    private void Update()
    {
        if (_inputs.axe && WeaponController.instance.IsMeelAttack())
        {
           StartAxe();
        }

        if (_inventoryInput.Inventory.Open.triggered)
        {
            Inventory.instanse.ShowInventory();
        }
        
        if (_inventoryInput.Player.Atack.triggered)
        {
            if (WeaponController.instance.IsMeelAttack())
            {
                StartAtack();
                _animator.SetInteger("IndexAtack",Random.Range(0, 4));
            }else if (!WeaponController.instance.IsMeelAttack())
            {
                _animator.SetTrigger("BowShoot");
                StartBowShoot();
            }
        }

        if (_inventoryInput.Player.Bow.triggered)
        {
            _animator.SetTrigger("EqiupBow");
            WeaponController.instance.EquipBow();
        }
    }

    private void StartBowShoot()
    {
        StartCoroutine(DisableAllMovePlayer(1.2f));

        PLayerAttack.instance.DamageEnemyBow();
    }


    private void StartAtack()
    {
        _animator.SetTrigger("Atack");
        
        StartCoroutine(DisableAllMovePlayer(1.2f));
    }

    //Cut Tree
    private void StartAxe()
    {
        _animator.SetTrigger("Axe");
        _inputs.axe = false;
        
        StartCoroutine(DisableAllMovePlayer(1.2f));
    }
    
    //Collect Item Anim
    private void CollectStuff()
    {
        _animator.SetTrigger("CollectStuff");

        StartCoroutine(DisableAllMovePlayer(1.9f));
    }

    private IEnumerator DisableAllMovePlayer(float timeForWaite)
    {
        ThirdPersonController _controller = GetComponent<ThirdPersonController>();
        
        _controller.MoveSpeed = 0;
        _controller.SprintSpeed = 0;
        _controller.Grounded = false;
        yield return new WaitForSecondsRealtime(timeForWaite);
        _controller.MoveSpeed = 2;
        _controller.SprintSpeed = 5.335f;
        _controller.Grounded = true;
    }
    
    private void OnTriggerStay(Collider other)
    {
        //collect Stay
        if (other.gameObject.TryGetComponent<ItemController>(out ItemController itemController))
        {
            if (_inventoryInput.Player.CollectStuff.triggered)
            {
                //Animation
                CollectStuff();
                
                //collect to inventory
                itemController.PickUp();

            }
        }
    }
}
