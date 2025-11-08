using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PartyManager : MonoBehaviour
{
    public static PartyManager instance;
    public PartyData partyData;

    public List<Image> partySlots = new List<Image>();
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddToParty(Sprite characterImg)
    {
        if (partyData.selectedCharacterSprites.Count >= partySlots.Count)
        {
            Debug.Log("파티가 꽉 찼습니다!");
            return;
        }

        if (partyData.selectedCharacterSprites.Contains(characterImg))
        {
            Debug.Log("이미 선택된 캐릭터입니다!");
            return;
        }

        partyData.AddCharacter(characterImg);
        UpdateSlots();
    }

    public void RemoveFromParty(Sprite characterImg)
    {
        partyData.RemoveCharacter(characterImg);
        UpdateSlots();
    }

    private void UpdateSlots()
    {
        for (int i = 0; i < partySlots.Count; i++)
        {
            if (i < partyData.selectedCharacterSprites.Count)
            {
                partySlots[i].sprite = partyData.selectedCharacterSprites[i];
                partySlots[i].color = Color.white;
            }
            else
            {
                partySlots[i].sprite = null;
                partySlots[i].color = new Color(1, 1, 1, 0);
            }
        }
    }
}
