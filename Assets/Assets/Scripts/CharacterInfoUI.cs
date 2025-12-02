using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : MonoBehaviour
{
    public static CharacterInfoUI instance;

    [Header("UI 오브젝트 연결")]
    public Image characterImage;            // 캐릭터 이미지
    public TextMeshProUGUI nameText;        // 이름 텍스트

    // [변경됨] 기존 합쳐진 변수를 지우고 둘로 나눴습니다.
    public TextMeshProUGUI hpText;          // HP 표시용
    public TextMeshProUGUI atkText;         // 공격력 표시용

    public TextMeshProUGUI costText;        // 코스트 표시용
    public TextMeshProUGUI characterDescriptionText; // 설명 텍스트

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // 시작할 때 모든 정보창 UI 숨기기 (안전장치)
        HideAll();
    }

    public void ShowInfo(PartyData.UnitInfo info)
    {
        // 1. UI 켜기
        characterImage.gameObject.SetActive(true);
        nameText.gameObject.SetActive(true);
        hpText.gameObject.SetActive(true);   
        atkText.gameObject.SetActive(true);  
        characterDescriptionText.gameObject.SetActive(true);

        if (costText != null) costText.gameObject.SetActive(true);

        // 2. 데이터 반영하기
        characterImage.sprite = info.unitImage;
        nameText.text = info.unitName;
        characterDescriptionText.text = info.description;
        hpText.text = $"HP : {info.hp}";
        atkText.text = $"ATK : {info.attackPower}";

        if (costText != null) costText.text = $"Cost : {info.cost}";
    }
    private void HideAll()
    {
        characterImage.gameObject.SetActive(false);
        nameText.gameObject.SetActive(false);
        if (hpText != null) hpText.gameObject.SetActive(false);
        if (atkText != null) atkText.gameObject.SetActive(false);
        if (costText != null) costText.gameObject.SetActive(false);
        characterDescriptionText.gameObject.SetActive(false);
    }
}
