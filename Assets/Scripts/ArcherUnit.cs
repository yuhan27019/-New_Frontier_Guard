// ArcherUnit.cs
using UnityEngine;

public class ArcherUnit : BaseUnit
{
    public GameObject arrowPrefab; // 화살 프리팹
    public Transform firePoint;    // 화살이 나가는 위치

    protected override void Attack()
    {
        // 부모의 공격(직접 데미지)은 실행하지 않고 화살을 날림
        if (target == null) return;

        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);

        // 화살 스크립트에 타겟과 데미지 정보 전달
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if (arrowScript != null)
        {
            arrowScript.Setup(target, attackDamage, enemyTag);
        }
    }
}