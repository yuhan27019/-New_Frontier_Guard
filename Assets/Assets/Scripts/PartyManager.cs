using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PartyManager : MonoBehaviour
{
    public static PartyManager instance;

    [Header("데이터 연결")]
    public PartyData partyData;

    [Header("설정")]
    public int maxPartySize = 4; // 파티 최대 인원 (4명)

    [Header("UI 슬롯 연결")]
    public List<Image> partySlots = new List<Image>();

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        UpdateSlots(); // 시작하자마자 슬롯 상태 갱신
    }

    // 파티 추가 시도 (성공하면 true, 실패하면 false 반환)
    public bool AddToParty(PartyData.UnitInfo unit)
    {
        // 꽉 찼으면 실패
        if (partyData.myParty.Count >= maxPartySize) return false;

        // 이미 있으면 실패
        if (partyData.myParty.Contains(unit)) return false;

        // 추가하고 슬롯 갱신
        partyData.AddToParty(unit);
        UpdateSlots();
        return true;
    }

    // 파티 제거
    public void RemoveFromParty(PartyData.UnitInfo unit)
    {
        partyData.RemoveFromParty(unit);
        UpdateSlots();
    }

    // [핵심] 오른쪽 아래 슬롯 이미지를 그리는 함수
    private void UpdateSlots()
    {
        for (int i = 0; i < partySlots.Count; i++)
        {
            if (i < partyData.myParty.Count)
            {
                // 파티에 있는 유닛이면 -> 이미지를 보여준다
                partySlots[i].sprite = partyData.myParty[i].unitImage;
                partySlots[i].color = Color.white; // 불투명하게
            }
            else
            {
                // 빈 칸이면 -> 투명하게 숨긴다
                partySlots[i].sprite = null;
                partySlots[i].color = new Color(1, 1, 1, 0); // 투명하게
            }
        }
    }
}
