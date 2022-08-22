using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Tree : MonoBehaviour, ITreeDamage
{
    public int health;
    private int maxHealth; 
    
    [SerializeField] private Type treeType;
    [SerializeField] private Transform treeLog;
    [SerializeField] private Transform treeLogHalf;
    [SerializeField] private Transform treeStamp;
    
    [SerializeField] private Transform dfDamagePopup;
    [SerializeField] private ParticleSystem fxTreeDestroy;

    [SerializeField] private Item item;


   public UnityAction<int> TreeHealth;

   public enum Type
   {
       Tree,
       Log, 
       LogHalf,
       Stump
   }

   private void Awake()
   {
       switch (treeType)
       {
           default:
           case Type.Tree: 
               maxHealth = 30;
               break;
           case Type.Log:
               maxHealth = 50;
               break;
           case Type.LogHalf:
               maxHealth = 50;
               break;
           case Type.Stump:
               maxHealth = 50;
               break;
       }

       health = maxHealth;
   }

   public void Damage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            HealthSystemOnDeath();
            Destroy(gameObject);
        }
    }

   private void HealthSystemOnDeath()
   {
       switch (treeType)
       {
           default:
           case Type.Tree: 
               //Spawn Fx
               Instantiate(fxTreeDestroy, transform.position, transform.rotation);
               
               //Spawn log
                Vector3 treelogOffset = transform.up * 1.8f;
               Instantiate(treeLog, transform.position + treelogOffset,
                   Quaternion.Euler(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f)));
           
               //Spawn Stump
               Instantiate(treeStamp, transform.position + new Vector3(0,0.1f,0), transform.rotation);
               
               break;
           
           case Type.Log:
               //Spawn Fx
               Instantiate(fxTreeDestroy, transform.position, transform.rotation);

               //Spawn HalfLog
               float logPosStump = 1f;
               treelogOffset = transform.up * logPosStump;
               Instantiate(treeLogHalf, transform.position + treelogOffset, transform.rotation);
               
               //Spawn HalfLog
               float logPosAboveStump = -1f;
               treelogOffset = transform.up * logPosAboveStump;
               Instantiate(treeLogHalf, transform.position + treelogOffset, transform.rotation);

               break;
           
           case Type.LogHalf:
               //Spawn Fx
               Instantiate(fxTreeDestroy, transform.position, transform.rotation);
               
               //Add tree to Inventory
               item.Amount = Random.Range(5, 10);
               Inventory.instanse.AddItem(item);

               break;
           
           case Type.Stump:
               //Spawn Fx
               Instantiate(fxTreeDestroy, transform.position, transform.rotation);
               
               //Add tree to Inventory
               item.Amount = Random.Range(5, 10);
               Inventory.instanse.AddItem(item);

               break;
       }
   }

   private void OnCollisionEnter(Collision collision)
   {
       if (collision.gameObject.TryGetComponent<ITreeDamage>(out ITreeDamage treeDamage))
       {
           if (collision.relativeVelocity.magnitude > 1f)
           {
               int damageAmout = Random.Range(5, 20);
               DamagePopUp.Create(dfDamagePopup, collision.GetContact(0).point, damageAmout);
               treeDamage.Damage(damageAmout);
           }
       }
   }
}
