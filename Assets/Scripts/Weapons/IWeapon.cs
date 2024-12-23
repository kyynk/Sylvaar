
namespace Weapons
{
    public enum WeaponType
    {
        Sword,
        Shield,
        Bomb
    }


    public interface IWeapon
    {
        string WeaponName { get; }

        WeaponType WeaponType { get; }
        
        float Damage { get; }
        
        float Range { get; }

        float CooldownTime { get; }

        void Attack();

        bool CanAttack();

        void ResetWeapon();
    }
}
