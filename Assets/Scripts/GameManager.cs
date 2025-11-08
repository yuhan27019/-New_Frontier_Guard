using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string nextSceneName = "SampleScene"; // 이동할 씬 이름
    public string behindSceneName = "StageScene";
    public PartyData partyData;

    public void StartGame()
    {
        if (partyData == null)
        {
            Debug.LogError("PartyData가 연결되지 않았습니다!");
            return;
        }

        if (partyData.selectedCharacterSprites.Count == 0)
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
}
