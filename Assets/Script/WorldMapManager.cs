using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Button을 사용하기 위해 추가!

public class WorldMapManager : MonoBehaviour
{
    // Inspector 창에서 버튼들을 순서대로(1-1, 1-2 ...) 드래그 앤 드롭
    public Button[] stageButtons;

    void Start()
    {
        // "World1_MaxLevel" 키로 저장된 값을 불러옵니다. 없으면 기본값 1 (1-1만 열림)
        int maxLevel = PlayerPrefs.GetInt("World1_MaxLevel", 1);

        // 모든 스테이지 버튼을 순회합니다.
        for (int i = 0; i < stageButtons.Length; i++)
        {
            // 버튼의 스테이지 번호 (i가 0일 때 1-1, 1일 때 1-2 ...)
            int stageNum = i + 1;

            if (stageNum <= maxLevel)
            {
                // 현재 스테이지 번호가 저장된 최대 레벨보다 낮거나 같으면
                stageButtons[i].interactable = true; // 버튼 활성화
            }
            else
            {
                // 아직 도달하지 못한 스테이지
                stageButtons[i].interactable = false; // 버튼 비활성화 (클릭 안 됨)
            }
        }
    }

    public void LoadStage(string sceneName)
    {
        Debug.Log("씬 로드 시도: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
    public int stageNumber = 1; // 이 씬은 1-1이므로 1, 1-2 씬은 2로 설정

    public void StageClear()
    {
        // "World1_MaxLevel" 키로 저장된 값을 불러옵니다. 없으면 기본값 1
        int maxLevel = PlayerPrefs.GetInt("World1_MaxLevel", 1);

        int nextStage = stageNumber + 1; // 다음 스테이지 번호 (예: 1-1 클리어 시 2)

        // 만약 새로 열릴 스테이지 번호가 기존에 저장된 값보다 크다면
        if (nextStage > maxLevel)
        {
            // 값을 갱신하고 저장합니다.
            PlayerPrefs.SetInt("World1_MaxLevel", nextStage);
            PlayerPrefs.Save(); // PlayerPrefs는 꼭 Save()를 호출해야 저장됩니다.
        }

        // 월드맵으로 복귀
        SceneManager.LoadScene("WorldMap_1");
    }
}