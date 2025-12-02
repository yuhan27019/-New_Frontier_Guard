using UnityEngine;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [Header("게임 설정")]
    public int currentStage = 1; // 1은 1-1, 2는 1-2 ... 11은 1-11

    [Header("전체 유닛 리스트 (순서 중요!)")]
    // 순서: 0:병사, 1:보급병, 2:궁수, 3:방패병, 4:수류탄병
    public GameObject[] allUnitPrefabs;

    public int food = 0;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    // 현재 스테이지에 따라 사용 가능한 유닛 리스트를 반환하는 함수
    public List<GameObject> GetUnlockedUnits()
    {
        List<GameObject> availableUnits = new List<GameObject>();
        int maxIndex = 0;

        // 스테이지별 해금 로직
        if (currentStage >= 8) maxIndex = 4; // 1-8 이상: 수류탄병까지
        else if (currentStage >= 4) maxIndex = 3; // 1-4 ~ 1-7: 방패병까지
        else if (currentStage >= 2) maxIndex = 2; // 1-2 ~ 1-3: 궁수까지
        else maxIndex = 1; // 1-1: 병사, 보급병만 (0, 1번)

        // 허용된 인덱스까지 리스트에 담아서 반환
        for (int i = 0; i <= maxIndex; i++)
        {
            if (i < allUnitPrefabs.Length)
            {
                availableUnits.Add(allUnitPrefabs[i]);
            }
        }

        return availableUnits;
    }

    public void AddFood(int amount)
    {
        food += amount;
        // UI 업데이트 코드 추가 가능
    }
}