using Core;
using UnityEngine;

namespace Weapons
{
    public class Explode : MonoBehaviour
    {
    private string weaponName;
    private float damage;
    private float range;

    [SerializeField] 
    private GameObject FireExplosionParticlePrefab;
    private ParticleSystem explosionEffect;


    public void Initialize(string weaponName, float damage, float range)
    {
        this.weaponName = weaponName;
        this.damage = damage;
        this.range = range;
        if (FireExplosionParticlePrefab == null)
        {
            Debug.Log($"Load Explode VFX");
            FireExplosionParticlePrefab = Resources.Load<GameObject>("Prefabs/Particles/FireExplosionParticlePrefab");
            GameObject explodeInstance = Instantiate(FireExplosionParticlePrefab);
            explosionEffect = explodeInstance.GetComponent<ParticleSystem>();
            explosionEffect.Stop();
        }
        Invoke(nameof(Explosion), 5f);
    }

    private void Explosion()
    {
        Debug.Log($"{weaponName} exploded, dealing {damage} damage!");

        explosionEffect.transform.position = transform.position;

        explosionEffect.Play();

        // GameObject explode = Instantiate(FireExplosionParticlePrefab, this.transform.position, Quaternion.identity);
        // Destroy(explode.gameObject,2f);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Enemy") && hit.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(damage);
            }
        }

        Destroy(this.gameObject); // Destroy bomb object after explosion
    }
    }
}
