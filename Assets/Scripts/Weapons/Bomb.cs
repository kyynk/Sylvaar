using Core;
using UnityEngine;

namespace Weapons
{
    public class Bomb : MonoBehaviour, IWeapon
    {
        public string WeaponName => "Bomb";
        public WeaponType WeaponType => WeaponType.Bomb;
        public float Damage => 100f;
        public float Range => 5f;
        public float CooldownTime => 2f;
        public float ThrowForce => 7f;
        // public bool _isReadyToThorw = true;
        private float lastAttackTime;

        public void Attack()
        {
            Debug.Log($"{WeaponName} is thrown!");
            lastAttackTime = Time.time;

            Destroy(this.gameObject);
            ThrowBomb();
        }

        private void ThrowBomb()
        {
            GameObject bombInstance = Instantiate(this.gameObject, this.transform.position, Camera.main.transform.rotation);

            bombInstance.transform.localScale = new Vector3(1, 1, 1);

            Rigidbody rb = bombInstance.AddComponent<Rigidbody>();
            SphereCollider collider = bombInstance.AddComponent<SphereCollider>();

            //Setup throw direstion to main camera
            Vector3 throwDirection = Camera.main.transform.forward;
            Vector3 forceToAdd = throwDirection * 5f + Camera.main.transform.up * 7f;

            rb.AddForce(forceToAdd, ForceMode.Impulse);

            Explode bombScript = bombInstance.AddComponent<Explode>();
            bombScript.Initialize(WeaponName, Damage, Range);

            // bomo explode after 5s
            Destroy(bombInstance, 5.1f);
        }

        public bool CanAttack()
        {
            return true;
        }

        public void ResetWeapon()
        {

        }
    }
}
