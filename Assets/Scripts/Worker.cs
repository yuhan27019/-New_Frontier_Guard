using UnityEngine;
using System.Collections;

public class Worker : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int gatherAmount = 10;
    public float gatherTime = 1f;

    private Transform castle;   // �츮 �� (�ڿ� ���� �� ��)
    private Transform resource;

    // �ϲ��� ���� ���¸� ��Ÿ���� ����
    private enum WorkerState { MovingToResource, Gathering, MovingToCastle }
    private WorkerState currentState;

    private Rigidbody2D rb;
    private bool isGathering = false; // 중복 채굴 방지용
    [SerializeField] Animator animator;
    [SerializeField] string tagName;
    [SerializeField] string castleTagName;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 1. 우리 성 찾기 (Tag: "Player") - 아까 만든 성 태그 활용
        GameObject castleObj = GameObject.FindGameObjectWithTag(castleTagName);
        if (castleObj != null) castle = castleObj.transform;

        // 2. 자원 찾기 (Tag: "Resource") - 새로 태그를 만들어야 함
        GameObject resourceObj = GameObject.FindGameObjectWithTag(tagName);
        if (resourceObj != null) resource = resourceObj.transform;

        // 처음 상태는 광산으로 이동
        currentState = WorkerState.MovingToResource;
    }

    void Update()
    {
        if (castle == null || resource == null) return;

        switch (currentState)
        {
            case WorkerState.MovingToResource:
                MoveTo(resource.position);
                // ���꿡 �����ߴ��� Ȯ�� (�Ÿ� 0.5 ����)
                if (Vector2.Distance(transform.position, resource.position) < 0.5f)
                {
                    StartCoroutine(GatherProcess());
                }
                break;

            case WorkerState.MovingToCastle:
                MoveTo(castle.position);
                // ���� �����ߴ��� Ȯ��
                if (Vector2.Distance(transform.position, castle.position) < 0.5f)
                {
                    DepositResource();
                }
                break;

            case WorkerState.Gathering:
                rb.linearVelocity = Vector2.zero; // ä�� �߿� ����
                break;
        }
    }

    // �̵� �Լ�
    void MoveTo(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        if (direction.x > 0)
            transform.localScale = new Vector3(2, 2, 2);
        else
            transform.localScale = new Vector3(-2, 2, 2);
    }

    // ä�� �ڷ�ƾ (�ð� ����)
    IEnumerator GatherProcess()
    {
        if (isGathering) yield break;
        isGathering = true;

        currentState = WorkerState.Gathering;
        Debug.Log("열심히 캐는 중...");

        yield return new WaitForSeconds(gatherTime);

        Debug.Log("채굴 완료! 성으로 돌아갑니다.");
        currentState = WorkerState.MovingToCastle;
        animator.SetBool("ooly", true);
        isGathering = false;
    }

    void DepositResource()
    {
        if (StageManager.instance != null)
        {
            StageManager.instance.AddFood(gatherAmount);
        }

        Debug.Log("자원 반납 완료! 다시 광산으로.");
        currentState = WorkerState.MovingToResource;
        animator.SetBool("ooly", false);
    }
}