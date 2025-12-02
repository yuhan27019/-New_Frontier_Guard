using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("소환 설정")]
    public Transform spawnPoint;    // 적이 나타날 위치 (적 성 앞)
    public float spawnInterval = 10f; // 소환 간격 (10초)

    private float timer = 0f;



    void Update()
    {
        // 시간 계속 흐름
        timer += Time.deltaTime;

        // 10초가 지나면 소환!
        if (timer >= spawnInterval)
        {
            SpawnEnemyRandomly();
            timer = 0f; // 타이머 초기화 (다시 0초부터 셈)
        }
    }

    void SpawnEnemyRandomly()
    {
        if (BattleManager.instance == null) return;

        // 1. 현재 스테이지에서 나올 수 있는 유닛들 가져오기 (해금된 유닛)
        List<GameObject> units = BattleManager.instance.GetUnlockedUnits();

        if (units.Count == 0) return;

        // 2. 그 중에서 '랜덤'으로 하나 뽑기 (냥코대전쟁 스타일)
        int randomIndex = Random.Range(0, units.Count);
        GameObject selectedPrefab = units[randomIndex];

        // 3. 적 생성
        GameObject enemyObj = Instantiate(selectedPrefab, spawnPoint.position, Quaternion.identity);

        // 4. 적군 세팅 (아군 프리팹을 적으로 변환)
        SetupEnemy(enemyObj);
    }

    // 아군 프리팹을 적군으로 개조하는 함수 (기존과 동일)
    void SetupEnemy(GameObject unit)
    {
        unit.name = unit.name.Replace("(Clone)", "") + "_Enemy";
        unit.tag = "Enemy";
        // unit.layer = LayerMask.NameToLayer("Enemy"); // 필요시 주석 해제

        BaseUnit unitScript = unit.GetComponent<BaseUnit>();
        SpriteRenderer sr = unit.GetComponent<SpriteRenderer>();

        if (unitScript != null)
        {
            unitScript.enemyTag = "Player"; // 적의 공격 대상은 플레이어

            // 적은 왼쪽을 봐야 하므로 좌우 반전
            Vector3 scale = unit.transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            unit.transform.localScale = scale;

            // 적군 전용 이미지(스프라이트)가 있다면 교체
            if (sr != null && unitScript.enemySprite != null)
            {
                sr.sprite = unitScript.enemySprite;
                sr.color = Color.white; // 색상 원래대로
            }
            else if (sr != null)
            {
                // 이미지가 따로 없으면 붉은색 틴트 처리
                sr.color = new Color(1f, 0.6f, 0.6f);
            }
        }
    }
}