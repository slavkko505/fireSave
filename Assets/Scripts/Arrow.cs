using UnityEngine;
using Random = UnityEngine.Random;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Transform dfDamagePopup;
    [SerializeField] private ParticleSystem _particleSystem;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy) && collision.gameObject.TryGetComponent<Health>(out Health health) )
        {
            //health
            int damage = Random.Range(15, 31);
            health.DamageEnemy(damage);
            
            //popUp
            DamagePopUp.Create(dfDamagePopup, collision.GetContact(0).point, damage);
            
            //FX
            Instantiate(_particleSystem, collision.GetContact(0).point, transform.rotation);
        }
    }
}
