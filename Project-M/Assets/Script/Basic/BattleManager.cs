using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using PlotSystem;

public class BattleManager : MonoSingleton<BattleManager>
{
    //玩家角色状态实例
    public static readonly Attack attackState = new Attack();
    public static readonly Idle idleState = new Idle();
    public static readonly Move moveState = new Move();
    public static readonly Dash dashState = new Dash();
    public static readonly Charge chargeState = new Charge();
    public static readonly OnDamage onDamageState = new OnDamage();
    public int GoldNum = 0;

    public RoomManager RoomManager;
    public override void OnInitialize() {
        base.OnInitialize();
        Application.targetFrameRate = 60;
    }

    public void InitBattle(CharacterBase character) {
        //初始化战斗，玩家的起始房间没有RoomFight
        GoldNum = 0;
        RoomManager = new RoomManager();
        RoomManager.Init();

        InitBattleWithCharacter(character);
    }

    public void InitBattleWithCharacter(CharacterBase character) {
        CharacterController.Instance.characterBase = CharacterFactory.CreatCharacterInstance(character, "SkyBook");
        HPBarController.Instance.Init(CharacterController.Instance.characterBase.CharacterStates[StateType.MaxHp], CharacterController.Instance.characterBase.MaxEnergy);
        HPBarController.Instance.UpdateHP(CharacterController.Instance.characterBase.CharacterStates[StateType.MaxHp]);
        HPBarController.Instance.UpdateEnergy(CharacterController.Instance.characterBase.MaxEnergy);
        CameraController.Instance.Init(CharacterController.Instance.characterBase.GameObject);
        RoomManager.EnterStartRoom();//进入房间

        GameDirecter.instance.PlayScenario("Test");//战斗开始播放剧情
    }


    public void CalculateDamage(CharacterBase attacker,CharacterBase defender,int damage) {
        var realDamage = attacker.CharacterStates[StateType.Attack] + damage;

        if (attacker.Team != defender.Team) {
            defender.OnDamage(realDamage);
        }
    }

    public void CalculateDamage(CharacterBase attacker,EnemyBase defender,int damage) {
        var realDamage = attacker.CharacterStates[StateType.Attack] + damage;

        if (attacker.Team != defender.Team) {
            defender.OnDamage(realDamage);
        }
    }

    public void CalculateDamage(EnemyBase attacker,CharacterBase defender,int damage) {
        var realDamage = attacker.Attack + damage;

        if (attacker.Team != defender.Team) {
            defender.OnDamage(realDamage);
        }
    }

    public void CalculateMapObjectDamage(CharacterBase attacker,MapObjectBase mapObjectBase,int damage) {
        var realDamage = attacker.CharacterStates[StateType.Attack] + damage;
        mapObjectBase.OnDamage(realDamage);
    }

    public void EndBattle(bool isWin) {
        //TODO:弹起结算页面
        UIManager.Instance.ShowPageCanvas();
        UIManager.Instance.Show(UIManager.Instance.EndPage);
    }
}
