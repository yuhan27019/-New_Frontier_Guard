using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    public string characterName;
    public string characterDescription;
    public string characterHPATK;
    public Sprite characterImage;

    private Button button;
    private bool isSelected = false;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickCharacter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClickCharacter()
    {
        if (isSelected)
        {
            // 이미 선택된 상태라면 파티에서 제거
            PartyManager.instance.RemoveFromParty(characterImage);
            isSelected = false;
        }
        else
        {
            // 선택되지 않았다면 파티 추가
            PartyManager.instance.AddToParty(characterImage);
            isSelected = true;
        }

        CharacterINfo.instance.ShowCharacterInfo(characterName,characterHPATK, characterDescription, characterImage);
    }

}
