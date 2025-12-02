using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // === 능력치 설정 ===
    public float hp = 150f;
    public float attackDamage = 15f;
    public float attackSpeed = 1.2f; // 초당 공격 횟수
    public float moveSpeed = 2.5f;
    public float attackRange = 1.5f; // 공격 사거리

    // === 내부 시스템 변수 ===
    private Transform target; // 공격할 대상 (적)
    private Rigidbody2D rb;
    private Animator animator;
    private float nextAttackTime = 0f;

    // [추가됨] 죽었는지 확인하는 변수
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // 태그로 적을 찾습니다.
        GameObject enemyObject = GameObject.FindGameObjectWithTag("Enemy");
        if (enemyObject != null)
        {
            target = enemyObject.transform;
        }
    }

    void Update()
    {
        // [추가됨] 이미 죽었다면 아무것도 하지 않음
        if (isDead) return;

        // 타겟(적)이 없으면 아무 행동도 하지 않습니다.
        if (target == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // 타겟과의 거리를 계산합니다.
        float distance = Vector2.Distance(transform.position, target.position);

        // 공격 사거리(attackRange)보다 멀리 있다면 타겟을 향해 이동합니다.
        if (distance > attackRange)
        {
            MoveTowardsTarget();
        }
        else // 공격 사거리 안에 있다면 이동을 멈추고 공격합니다.
        {
            rb.linearVelocity = Vector2.zero; // 이동 정지

            // 공격 속도에 맞춰 공격을 시도합니다.
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackSpeed;
            }
        }
    }

    void MoveTowardsTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        // 이동 방향에 따라 캐릭터의 좌우를 반전시킵니다.
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // animator?.SetBool("isMoving", true); // 애니메이션이 있다면 주석 해제
    }

    void Attack()
    {
        Debug.Log(gameObject.name + "가 적을 공격.");
        // animator?.SetTrigger("Attack"); // 공격 애니메이션 실행
    }

    // === [수정됨] 데미지 입는 함수 ===
    public void TakeDamage(float damage)
    {
        // 이미 죽었다면 데미지 처리를 하지 않음
        if (isDead) return;

        hp -= damage;
        Debug.Log($"{gameObject.name} 남은 체력: {hp}");

        // 체력이 0 이하가 되면 사망 처리
        if (hp <= 0)
        {
            Die();
        }
    }

    // === [추가됨] 사망 처리 함수 ===
    void Die()
    {
        isDead = true; // 사망 상태로 변경
        Debug.Log(gameObject.name + "가 사망했습니다.");

        // 1. 움직임 멈춤
        rb.linearVelocity = Vector2.zero;


        // 2. 더 이상 충돌하지 않도록 콜라이더 비활성화 (선택 사항)
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // 3. 사망 애니메이션 실행 (애니메이터에 "Die" 트리거가 있다고 가정)
        //if (animator != null)
        //{
        //    animator.SetTrigger("Die");
        //}

        // 4. 오브젝트 파괴 (애니메이션이 끝날 시간을 고려하여 2초 뒤 파괴)
        Destroy(gameObject, 2.0f);
    }
}