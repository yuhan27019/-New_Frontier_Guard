using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyLoader : MonoBehaviour
{
    public PartyData partyData;
    public Image[] partySlots; // 게임씬의 UI 슬롯

    void Start()
    {
        LoadParty();
    }

    void LoadParty()
    {
        if (partyData == null)
        {
            Debug.LogError("PartyData가 연결되지 않았습니다!");
            return;
        }

        // 슬롯 초기화
        for (int i = 0; i < partySlots.Length; i++)
        {
            partySlots[i].sprite = null;
            partySlots[i].color = new Color(1, 1, 1, 0);
        }

        // PartyData에서 Sprite 불러와서 UI에 표시
        for (int i = 0; i < partyData.myParty.Count && i < partySlots.Length; i++)
        {
            PartyData.UnitInfo unitInfo = partyData.myParty[i];

            if (unitInfo != null)
            {
                partySlots[i].sprite = unitInfo.unitImage;
                partySlots[i].color = Color.white; 

                Debug.Log($"파티 슬롯 {i + 1} 로드 완료 : {unitInfo.unitName}");
            }
        }
    }
}
