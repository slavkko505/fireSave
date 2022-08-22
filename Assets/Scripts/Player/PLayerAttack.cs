using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PLayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform dfDamagePopup;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject hitArea;
    [SerializeField] private GameObject attackArea;
    [SerializeField] private Camera _camera;

    public static PLayerAttack instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DamageTree()
   {
       Vector3 posPopUp = new Vector3(hitArea.transform.position.x, hitArea.transform.position.y + 0.5f, hitArea.transform.position.z + 1f);
       
       //Find Object int area
       Vector3 collidersSize = Vector3.one * 0.5f;
       Collider[] collidersArray = Physics.OverlapBox(hitArea.transform.position, collidersSize);

       foreach (var collider in collidersArray)
       {
           if (collider.TryGetComponent<ITreeDamage>(out ITreeDamage treeDamage))
           {
               //FX
               Instantiate(_particleSystem, collider.bounds.ClosestPoint(hitArea.transform.position), transform.rotation);
               
               //PopUp
               int amount = Random.Range(15, 30);
               DamagePopUp.Create(dfDamagePopup, posPopUp, amount);
               
               // Tree Damage
               treeDamage.Damage(amount);
           }
       }
   }

   public void DamageEnemy()
   {
       float collidersSize =  1.5f;
       Collider[] collidersArray = Physics.OverlapSphere(attackArea.transform.position, collidersSize);

       foreach (var collider in collidersArray)
       {
           if (collider.TryGetComponent<IEnemyDamage>(out IEnemyDamage enemyDamage) && collider.TryGetComponent<Enemy>(out Enemy enemy))
           {
               //FX
               Instantiate(_particleSystem, collider.bounds.center, transform.rotation);
               
               //PopUp
               int amount = Random.Range(15, 30);
               DamagePopUp.Create(dfDamagePopup, collider.transform.position + new Vector3(0,2f,0), amount);
               
               // Enemy Damage
               enemyDamage.DamageEnemy(amount);
           }
       }
   }

   public void DamageEnemyBow()
   {
       RaycastHit hit;
       if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, 20f) && hit.distance < 20)
       {
           StartCoroutine(ShootEffect(hit.point));
       }
   }
   
   private IEnumerator ShootEffect(Vector3 pos)
   {
       yield return new WaitForSecondsRealtime(0.7f);
       GameObject arrow = Instantiate(arrowPrefab);

       arrow.transform.position = pos;
       
       yield return new WaitForSecondsRealtime(1f);
       Destroy(arrow);
   }
   
   
}

