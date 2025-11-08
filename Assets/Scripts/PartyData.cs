using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartyData", menuName = "Game/Party Data")]
public class PartyData : ScriptableObject
{
    public List<Sprite> selectedCharacterSprites = new List<Sprite>();

    public void ClearParty()
    {
        selectedCharacterSprites.Clear();
    }

    public void AddCharacter(Sprite sprite)
    {
        if (!selectedCharacterSprites.Contains(sprite))
            selectedCharacterSprites.Add(sprite);
    }

    public void RemoveCharacter(Sprite sprite)
    {
        if (selectedCharacterSprites.Contains(sprite))
            selectedCharacterSprites.Remove(sprite);
    }
}
