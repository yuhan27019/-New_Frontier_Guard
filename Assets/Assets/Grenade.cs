using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("수류탄 설정")]
    public float duration = 1.0f;     // 날아가는 데 걸리는 시간 (초)
    public float arcHeight = 2.0f;    // 포물선 높이
    public float explosionRadius = 2.0f; // 폭발 범위
    public GameObject explosionEffect; // 폭발 이펙트 (파티클 프리팹)

    private float damage;
    private string targetTag; // 공격할 대상 태그

    // 포물선 이동을 위한 변수들
    private Vector3 startPos;
    private Vector3 targetPos;
    private Vector3 controlPos; // 포물선의 정점(제어점)
    private float timeElapsed = 0f;
    private bool isThrown = false;

    // 수류탄병(GrenadierUnit)이 던질 때 호출
    public void Throw(Vector3 targetPosition, float newDamage, string newTargetTag)
    {
        startPos = transform.position;
        targetPos = targetPosition; // 목표 위치 고정 (유도탄 아님)
        damage = newDamage;
        targetTag = newTargetTag;

        // 시작점과 목표점의 중간 지점에서 위로 arcHeight만큼 띄운 점을 계산
        controlPos = startPos + (targetPos - startPos) / 2 + Vector3.up * arcHeight;

        isThrown = true;
    }

    void Update()
    {
        if (!isThrown) return;

        // 시간 흐름 계산 (0 ~ 1 사이의 값)
        timeElapsed += Time.deltaTime;
        float t = timeElapsed / duration;

        // 베지에 곡선 공식: (1-t)^2 * P0 + 2(1-t)t * P1 + t^2 * P2
        // 수류탄의 위치를 곡선에 따라 이동시킴
        Vector3 m1 = Vector3.Lerp(startPos, controlPos, t);
        Vector3 m2 = Vector3.Lerp(controlPos, targetPos, t);
        transform.position = Vector3.Lerp(m1, m2, t);

        // 회전 효과 (빙글빙글 돌기)
        transform.Rotate(0, 0, -360 * Time.deltaTime * 2);

        // 목표 지점에 도착했으면 (t가 1 이상이면)
        if (t >= 1f)
        {
            Explode();
        }
    }

    void Explode()
    {
        // 1. 폭발 이펙트 생성 (있다면)
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // 2. 범위 내 적 감지 (원형 범위)
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D obj in hitObjects)
        {
            // 타겟 태그(Enemy 또는 Player)와 일치하는지 확인
            if (obj.CompareTag(targetTag))
            {
                // 데미지 전달
                BaseUnit unit = obj.GetComponent<BaseUnit>();
                if (unit != null)
                {
                    unit.TakeDamage(damage);
                }

                // 성도 공격 가능
                Castle castle = obj.GetComponent<Castle>();
                if (castle != null)
                {
                    castle.TakeDamage(damage);
                }
            }
        }

        Debug.Log("쾅! 수류탄 폭발");
        Destroy(gameObject); // 수류탄 삭제
    }

    // 에디터에서 폭발 범위를 눈으로 보기 위한 함수
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}