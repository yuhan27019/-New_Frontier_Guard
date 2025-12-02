using UnityEngine;

public class CharacterINfo : MonoBehaviour
{
    public static CharacterINfo instance;
    public CharacterInfoUI infoUI;

    void Awake()
    {
        instance = this;
    }

    // 매개변수 타입 변경: UnitData -> PartyData.UnitInfo
    public void ShowCharacterInfo(PartyData.UnitInfo info)
    {
        infoUI.ShowInfo(info);
    }
}
