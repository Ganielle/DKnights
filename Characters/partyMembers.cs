using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class partyMembers
{
    
    public enum memberType
    {
        mainCharacter,
        member1,
        member2,
        member3
    }
    
    public memberType memType;

    public Sprite getIconSpriteCharacter()
    {
        switch(memType)
        {
            default:
            case memberType.mainCharacter: return characterAssets.Instance.mainCharacterEquipment;
            case memberType.member1: return characterAssets.Instance.friend1Equipment;
            case memberType.member2: return characterAssets.Instance.friend2Equipment;
            case memberType.member3: return characterAssets.Instance.friend3Equipment;
        }
    }

    public Sprite getCharacterSpriteBattle()
    {
        switch (memType)
        {
            default:
            case memberType.mainCharacter: return characterAssets.Instance.mainCharacter;
            case memberType.member1: return characterAssets.Instance.IrahWitz;
            case memberType.member2: return characterAssets.Instance.YuukiBelle;
            case memberType.member3: return characterAssets.Instance.ReeveOwun;
        }
    }
}
