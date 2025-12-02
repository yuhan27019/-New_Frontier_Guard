using UnityEngine;
using UnityEngine.UI; // 버튼 제어를 위해 필요

public class PlayerUnitSpawner : MonoBehaviour
{
    // 유니티 에디터에서 순서대로 버튼을 연결해야 함 (병사, 보급, 궁수, 방패, 수류탄 순)
    public Button[] unitButtons;

    void Start()
    {
        UpdateButtons();
    }

    void UpdateButtons()
    {
        int currentStage = StageManager.instance.currentStage;
        int unlockedIndex = 0;

        // 해금 로직 (GameManager와 동일하게 맞춤)
        if (currentStage >= 8) unlockedIndex = 4;
        else if (currentStage >= 4) unlockedIndex = 3;
        else if (currentStage >= 2) unlockedIndex = 2;
        else unlockedIndex = 1;

        // 모든 버튼을 확인하며 활성화/비활성화 처리
        for (int i = 0; i < unitButtons.Length; i++)
        {
            if (i <= unlockedIndex)
            {
                unitButtons[i].interactable = true; // 버튼 활성화
                unitButtons[i].gameObject.SetActive(true); // 보이게 함
            }
            else
            {
                unitButtons[i].interactable = false; // 버튼 비활성화
                unitButtons[i].gameObject.SetActive(false); // 아예 안 보이게 함 (선택사항)
            }
        }
    }

    // 버튼 클릭 시 연결할 함수 (예: 0번 버튼은 병사 소환)
    public void OnClickSpawn(int unitIndex)
    {
        // 자원 체크 및 소환 로직은 여기에...
        // GameManager.instance.allUnitPrefabs[unitIndex] 를 생성하면 됨
    }
}