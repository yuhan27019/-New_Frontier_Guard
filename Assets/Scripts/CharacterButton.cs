using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    [Header("데이터 연결")]
    public PartyData partyData; // Merged Party Data 파일 연결
    public int unitIndex;       // 도감 번호 (0, 1, 2...)
    private Button button;
    private PartyData.UnitInfo myInfo;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickCharacter);

        // 데이터 로드
        if (partyData != null && partyData.allUnits.Count > unitIndex)
        {
            myInfo = partyData.allUnits[unitIndex];

            // 버튼 이미지 유닛 얼굴로 변경
            if (myInfo.unitImage != null)
                GetComponent<Image>().sprite = myInfo.unitImage;
        }
    }

    public void OnClickCharacter()
    {
        if (myInfo == null) return;

        // 1. [왼쪽 화면] 정보창에 내 정보 띄우기
        CharacterINfo.instance.ShowCharacterInfo(myInfo);

        // 2. [오른쪽 아래] 파티 슬롯에 넣거나 빼기
        // 내 정보가 현재 파티 리스트에 들어있는지 확인
        if (partyData.myParty.Contains(myInfo))
        {
            // 이미 있으면 -> 파티에서 뺌
            PartyManager.instance.RemoveFromParty(myInfo);
        }
        else
        {
            // 없으면 -> 파티에 추가 (꽉 찼는지는 PartyManager가 알아서 거절함)
            PartyManager.instance.AddToParty(myInfo);
        }
    }
}
