using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("화살 설정")]
    public float speed = 15f;      // 화살 날아가는 속도
    public float lifetime = 3f;    // 화살이 아무것도 못 맞췄을 때 사라지는 시간

    private float damage;          // 전달받은 데미지
    private string targetTag;      // 맞춰야 할 대상의 태그 ("Enemy" 또는 "Player")
    private Vector3 moveDirection; // 날아갈 방향
    private bool isSetup = false;  // 설정이 완료되었는지 체크

    void Start()
    {
        // 안전장치: 일정 시간이 지나면 자동으로 파괴 (렉 방지)
        Destroy(gameObject, lifetime);
    }

    // 궁수(ArcherUnit)가 화살을 쏠 때 호출하는 초기화 함수
    public void Setup(Transform target, float newDamage, string newTargetTag)
    {
        damage = newDamage;
        targetTag = newTargetTag;

        if (target != null)
        {
            // 타겟을 향한 방향 벡터 계산 (단위 벡터)
            moveDirection = (target.position - transform.position).normalized;

            // (선택사항) 화살이 날아가는 방향을 바라보게 회전
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            isSetup = true;
        }
        else
        {
            // 타겟이 없는데 발사된 경우 그냥 파괴
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 설정이 안 됐으면 움직이지 않음
        if (!isSetup) return;

        // 정해진 방향으로 계속 날아감
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    // 충돌 감지 (Trigger)
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. 내가 맞춰야 할 태그인지 확인
        if (collision.CompareTag(targetTag))
        {
            // 2. 맞은 대상에게서 데미지를 받을 수 있는 컴포넌트 찾기

            // 우선 유닛인지 확인 (BaseUnit)
            BaseUnit unit = collision.GetComponent<BaseUnit>();
            if (unit != null)
            {
                unit.TakeDamage(damage);
            }
            else
            {
                // 유닛이 아니면 성인지 확인 (Castle)
                // ※ 이전에 성 스크립트를 만들었다고 가정합니다.
                Castle castle = collision.GetComponent<Castle>();
                if (castle != null)
                {
                    castle.TakeDamage(damage);
                }
            }

            // 3. 목표를 맞췄으니 화살 파괴
            Destroy(gameObject);
        }
        // (추가) 만약 땅이나 벽 태그가 있다면 여기에 추가해서 화살이 박히게 할 수도 있음
        // else if (collision.CompareTag("Ground")) { Destroy(gameObject); }
    }
}