using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    // 어디서든 접근할 수 있게 만드는 '싱글톤' (이게 있어야 instance 에러가 안 남)
    public static BattleManager instance;

    [Header("전투 자원 설정")]
    public int food = 100;

    [Header("스테이지 및 유닛 설정")]
    public int currentStage = 1; // 현재 스테이지 (에디터에서 설정)

    // 0:병사, 1:보급병, 2:궁수, 3:방패병, 4:수류탄병 (순서대로 넣기)
    public GameObject[] allUnitPrefabs;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    // 식량 사용 함수
    public bool TrySpendFood(int amount)
    {
        if (food >= amount)
        {
            food -= amount;
            return true;
        }
        return false;
    }

    // ★ EnemySpawner가 사용할 유닛 해금 로직 (GameManager에서 이사옴)
    public List<GameObject> GetUnlockedUnits()
    {
        List<GameObject> availableUnits = new List<GameObject>();
        int maxIndex = 0;

        if (currentStage >= 8) maxIndex = 4;
        else if (currentStage >= 4) maxIndex = 3;
        else if (currentStage >= 2) maxIndex = 2;
        else maxIndex = 1;

        for (int i = 0; i <= maxIndex; i++)
        {
            if (i < allUnitPrefabs.Length)
            {
                availableUnits.Add(allUnitPrefabs[i]);
            }
        }
        return availableUnits;
    }
}