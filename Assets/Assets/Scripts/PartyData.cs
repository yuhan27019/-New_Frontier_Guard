using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartyData", menuName = "Game/Party Data")]
public class PartyData : ScriptableObject
{
    [System.Serializable]
    public class UnitInfo
    {
        public string unitName;     // 이름
        public Sprite unitImage;    // 이미지
        public int cost;            // 코스트
        public int hp;              // HP
        public int attackPower;     // 공격력
        [TextArea]
        public string description;  // 설명
    }

    [Header("전체 유닛 도감 (여기서 모든 유닛을 등록하세요)")]
    public List<UnitInfo> allUnits = new List<UnitInfo>();

    [Header("현재 선택된 파티")]
    public List<UnitInfo> myParty = new List<UnitInfo>();

    // 파티 초기화
    public void ClearParty()
    {
        myParty.Clear();
    }

    // 파티 추가
    public void AddToParty(UnitInfo unit)
    {
        if (!myParty.Contains(unit))
        {
            myParty.Add(unit);
        }
    }

    // 파티 제거
    public void RemoveFromParty(UnitInfo unit)
    {
        if (myParty.Contains(unit))
        {
            myParty.Remove(unit);
        }
    }
}
