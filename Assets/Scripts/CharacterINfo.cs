using UnityEngine;

public class CharacterINfo : MonoBehaviour
{
    public static CharacterINfo instance;

    public CharacterInfoUI infoUI;

    void Awake()
    {
        instance = this;
    }

    public void ShowCharacterInfo(string name,string Info, string desc, Sprite img)
    {
        infoUI.ShowInfo(name, Info, desc, img);
    }
}
