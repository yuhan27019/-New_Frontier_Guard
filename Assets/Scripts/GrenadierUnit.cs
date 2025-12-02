// GrenadierUnit.cs
using UnityEngine;

public class GrenadierUnit : BaseUnit
{
    public GameObject grenadePrefab;
    public Transform firePoint;

    protected override void Attack()
    {
        if (target == null) return;

        GameObject grenade = Instantiate(grenadePrefab, firePoint.position, Quaternion.identity);

        // 수류탄 스크립트 설정
        Grenade grenadeScript = grenade.GetComponent<Grenade>();
        if (grenadeScript != null)
        {
            // 타겟 위치로 던짐
            grenadeScript.Throw(target.position, attackDamage, enemyTag);
        }
    }
}