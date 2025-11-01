using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PartyManager : MonoBehaviour
{
    public static PartyManager instance;

    public List<Image> partySlots = new List<Image>();
    private List<Sprite> partyList = new List<Sprite>(); // 선택된 순서 저장

    void Awake()
    {
        instance = this;
    }

    public void AddToParty(Sprite characterImg)
    {
        if (partyList.Count >= partySlots.Count)
        {
            Debug.Log("파티가 꽉 찼습니다!");
            return;
        }

        partyList.Add(characterImg);
        UpdateSlots();
    }

    public void RemoveFromParty(Sprite characterImg)
    {
        if (partyList.Contains(characterImg))
        {
            partyList.Remove(characterImg);
            UpdateSlots();
        }
    }

    private void UpdateSlots()
    {
        // 모든 슬롯 초기화
        for (int i = 0; i < partySlots.Count; i++)
        {
            if (i < partyList.Count)
            {
                partySlots[i].sprite = partyList[i];
                partySlots[i].color = Color.white;
            }
            else
            {
                partySlots[i].sprite = null;
                partySlots[i].color = new Color(1, 1, 1, 0); // 안보이게
            }
        }
    }
}
