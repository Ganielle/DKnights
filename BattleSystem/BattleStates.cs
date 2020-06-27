using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BattleStates
{
    public enum BattleState
    {
        NONE,
        STARTBATTLE,
        FIGHTSTART,
        PLAYERTURN,
        MEMBERONETURN,
        MEMBERTWOTURN,
        MEMBERTHREETURN,
        ENEMYTURN,
        CHOOSEENEMY,
        VICTORY,
        DEFEAT
    }

    public enum FirstTurn
    {
        PLAYER,
        ENEMY,
        NONE
    }

    public enum MethodState
    {
        NONE,
        SELECT,
        CHANT,
        SELECTENEMY,
        ATTACKENEMY
    }

    public enum BattlePosition
    {
        NOTDONE,
        DONE
    }

    private event EventHandler battleStateOnChange, firstTurnOnChange, methodStateOnChange;

    public event EventHandler battleStateChange
    {
        add
        {
            if (battleStateOnChange == null || !battleStateOnChange.GetInvocationList().Contains(value))
                battleStateOnChange += value;
        }
        remove
        {
            battleStateOnChange -= value;
        }
    }

    public event EventHandler firstTurnChange
    {
        add
        {
            if (firstTurnOnChange == null || !firstTurnOnChange.GetInvocationList().Contains(value))
                firstTurnOnChange += value;
        }
        remove
        {
            firstTurnOnChange -= value;
        }
    }

    public event EventHandler methodStateChange
    {
        add
        {
            if (methodStateOnChange == null || !methodStateOnChange.GetInvocationList().Contains(value))
                methodStateOnChange += value;
        }
        remove
        {
            methodStateOnChange -= value;
        }
    }

    private static BattleState battleState;

    private static FirstTurn firstTurn;

    private static MethodState methodState;

    private static BattlePosition playerBattleState;

    private List<GameObject> enemyGameObjects;

    public BattleStates()
    {
        enemyGameObjects = new List<GameObject>();
    }

    public BattleState getsetBattleState
    {
        get { return battleState; }
        set
        {
            battleState = value;
            battleStateOnChange?.Invoke(this, EventArgs.Empty);
        }
    }

    public FirstTurn getsetFirstTurn
    {
        get { return firstTurn; }
        set
        {
            firstTurn = value;
            firstTurnOnChange?.Invoke(this, EventArgs.Empty);
        }
    }

    public MethodState getsetMethodState
    {
        get { return methodState; }
        set
        {
            methodState = value;
            methodStateOnChange?.Invoke(this, EventArgs.Empty);
        }
    }

    public void setEnemyGameObjects(GameObject go)
    {
        enemyGameObjects.Add(go);
    }

    public List<GameObject> getEnemyGameObjects()
    {
        return enemyGameObjects;
    }

    public BattlePosition getsetPlayerBattlePositionState
    {
        get { return playerBattleState; }
        set { playerBattleState = value; }
    }
}
