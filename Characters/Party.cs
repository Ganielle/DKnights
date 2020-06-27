using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party
{
    public void addParty(partyMembers partyMem)
    {
        GameDatabaseStatic.addParty(partyMem);
    }

    public List<partyMembers> GetPartyMembers()
    {
        return GameDatabaseStatic.getPary();
    }
}
