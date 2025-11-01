using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : MonoBehaviour
{
    public static CharacterInfoUI instance;

    public TextMeshProUGUI nameText;
    public Image characterImage;
    public TextMeshProUGUI characterHPATK;
    public TextMeshProUGUI characterDescriptionText;

    void Start()
    {
        characterImage.gameObject.SetActive(false);
        nameText.gameObject.SetActive(false);
        characterDescriptionText.gameObject.SetActive(false);
        characterHPATK.gameObject.SetActive(false);
    }


    public void ShowInfo(string name,string Info, string desc, Sprite img)
    {
        characterImage.gameObject.SetActive(true);
        nameText.gameObject.SetActive(true);
        characterDescriptionText.gameObject.SetActive(true);
        characterHPATK.gameObject.SetActive(true);

        characterImage.sprite = img;
        nameText.text = name;
        characterDescriptionText.text = desc;
        characterHPATK.text = Info;
    }
}
