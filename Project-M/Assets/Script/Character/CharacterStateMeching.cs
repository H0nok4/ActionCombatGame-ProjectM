using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMeching
{
    public CharacterBase characterBase;

    public CharacterStateBase preState;
    public CharacterStateBase curState;
    public void Init(CharacterBase character) {
        characterBase = character;
        curState = BattleManager.idleState;
    }

    public void Run() {
        curState.Run(characterBase,this);
    }

    public void ChangeState(CharacterStateBase curState,CharacterStateBase targetState) {
        curState.Exit(characterBase);
        targetState.Enter(characterBase);
        this.preState = curState;
        this.curState = targetState;
    }

}
