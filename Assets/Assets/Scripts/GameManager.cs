using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string nextSceneName = "GameScene"; // 이동할 씬 이름
    public string behindSceneName = "StageScene";
    public PartyData partyData;

    public void StartGame()
    {
        if (partyData == null)
        {
            Debug.LogError("PartyData가 연결되지 않았습니다!");
            return;
        }

        if (partyData.myParty.Count == 0)
        {
            Debug.LogWarning("파티가 비어있습니다! 시작할 수 없습니다.");
            return;
        }

        SceneManager.LoadScene(nextSceneName);
    }

    public void Cancel()
    {
        if (partyData != null)
        {
            Debug.Log("파티 데이터 초기화 후 스테이지 선택 씬으로 이동합니다!");
            partyData.ClearParty(); // 선택된 캐릭터 전부 삭제
        }
        SceneManager.LoadScene(behindSceneName);
    }


    public static GameManager instance;

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
