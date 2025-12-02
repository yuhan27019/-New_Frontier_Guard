using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [Header("기본 능력치")]
    public float hp = 100f;
    public float moveSpeed = 2f;
    public float attackRange = 1.5f;
    public float attackDamage = 10f;
    public float attackSpeed = 1f;

    [Header("설정")]
    public string enemyTag = "Enemy"; // 아군이면 "Enemy", 적군이면 "Player"
    public LayerMask enemyLayer;      // 적 감지용 레이어

    protected Transform target;       // 현재 공격 대상
    protected Transform enemyCastle;  // 적 성
    protected float nextAttackTime = 0f;
    protected Rigidbody2D rb;
    protected Animator anim;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // 1. 적 성을 찾아서 기본 목표로 설정
        GameObject castleObj = GameObject.FindGameObjectWithTag(enemyTag == "Enemy" ? "EnemyCastle" : "PlayerCastle");
        if (castleObj != null) enemyCastle = castleObj.transform;
    }

    protected virtual void Update()
    {
        FindNearestTarget(); // 가장 가까운 적 찾기

        if (target == null)
        {
            // 적이 없으면 적 성으로 전진
            if (enemyCastle != null) MoveTo(enemyCastle.position);
            else rb.linearVelocity = Vector2.zero; // 성도 없으면 대기
        }
        else
        {
            float distance = Vector2.Distance(transform.position, target.position);

            if (distance <= attackRange)
            {
                // 사거리 안이면 멈춰서 공격
                rb.linearVelocity = Vector2.zero;
                if (Time.time >= nextAttackTime)
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackSpeed;
                }
            }
            else
            {
                // 사거리 밖이면 추격
                MoveTo(target.position);
            }
        }
    }

    // 가장 가까운 적을 찾는 함수 (병사 vs 병사 우선, 없으면 성)
    void FindNearestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float minDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                nearestEnemy = enemy.transform;
            }
        }

        // 적 유닛이 있으면 그걸 타겟으로, 없으면 적 성을 타겟으로
        if (nearestEnemy != null) target = nearestEnemy;
        else target = enemyCastle;
    }

    void MoveTo(Vector3 dest)
    {
        Vector2 dir = (dest - transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;

        // 방향 전환
        if (dir.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        else if (dir.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }

    // 자식 클래스에서 오버라이드(덮어쓰기)할 공격 함수
    protected virtual void Attack()
    {
        // 기본 공격: 직접 데미지 주기 (Melee)
        // 적에게 "TakeDamage" 함수가 있다고 가정
        target.SendMessage("TakeDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
        Debug.Log(name + "가 공격함!");
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
        if (hp <= 0) Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}