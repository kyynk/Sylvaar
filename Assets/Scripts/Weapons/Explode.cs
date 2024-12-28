using Core;
using UnityEngine;

namespace Weapons
{
    public class Explode : MonoBehaviour
    {
    private string weaponName;
    private float damage;
    private float range;

    public void Initialize(string weaponName, float damage, float range)
    {
        this.weaponName = weaponName;
        this.damage = damage;
        this.range = range;

        Invoke(nameof(Explosion), 5f);
    }

    private void Explosion()
    {
        Debug.Log($"{weaponName} exploded, dealing {damage} damage!");

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Enemy") && hit.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(damage);
            }
        }

        Destroy(gameObject); // Destroy bomb object after explosion
    }
    }
}
