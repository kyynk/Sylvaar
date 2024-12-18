// 命名空間，方便組織代碼
namespace Weapons
{
    /// <summary>
    /// 定義武器的通用接口
    /// </summary>
    public interface IWeapon
    {
        // 武器名稱
        string WeaponName { get; }

        // 武器傷害值
        float Damage { get; }

        // 攻擊範圍（近戰為距離，遠程為射程）
        float Range { get; }

        // 武器冷卻時間（每次攻擊的間隔時間）
        float CooldownTime { get; }

        // 攻擊方法
        void Attack();

        // 是否可以攻擊（檢查冷卻或其他條件）
        bool CanAttack();

        // 重置武器狀態
        void ResetWeapon();
    }
}
