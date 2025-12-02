using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public Transform spawnPoint; // 적 생성 위치
    public float spawnInterval = 20f; // 생성 간격

    private List<GameObject> spawnableUnits; // 현재 스테이지 출현 가능 유닛들
    private float nextSpawnTime = 0f;

    void Start()
    {
        // 1. 게임 시작 시 현재 스테이지에 맞는 유닛 목록을 가져옴
        if (StageManager.instance != null)
        {
            spawnableUnits = StageManager.instance.GetUnlockedUnits();
        }
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        if (spawnableUnits == null || spawnableUnits.Count == 0) return;

        // 2. 해금된 유닛 목록 중에서 랜덤하게 하나 뽑음
        int randomIndex = Random.Range(0, spawnableUnits.Count);
        GameObject selectedUnit = spawnableUnits[randomIndex];

        // 3. 적 생성
        GameObject enemy = Instantiate(selectedUnit, spawnPoint.position, Quaternion.identity);

        // 4. 중요: 생성된 유닛의 태그와 레이어를 "Enemy"로 변경해줘야 함!
        // (프리팹은 기본적으로 아군 세팅일 수 있으므로)
        SetUnitTeamToEnemy(enemy);
    }

    void SetUnitTeamToEnemy(GameObject unit)
    {
        unit.tag = "Enemy";
        unit.layer = LayerMask.NameToLayer("Enemy"); // 레이어도 있다면 변경

        // 유닛 색상을 빨간색으로 바꿔서 적임을 표시 (선택사항)
        SpriteRenderer sr = unit.GetComponent<SpriteRenderer>();
        if (sr != null) sr.color = new Color(1, 0.5f, 0.5f); // 붉은 틴트

        // BaseUnit 스크립트의 설정을 적군용으로 변경
        BaseUnit unitScript = unit.GetComponent<BaseUnit>();
        if (unitScript != null)
        {
            unitScript.enemyTag = "Player"; // 적의 적은 플레이어
        }
    }
}