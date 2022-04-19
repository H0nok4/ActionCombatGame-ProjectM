using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace MyUI {
    public class SelectKlee : ButtonBase {
        public override void OnClick() {
            UIManager.Instance.HidePageCanvas();
            var characterProperty = DataCenter.Instance.GetCharacterPropertyByName("Klee");
            CharacterBase character = new CharacterKlee();

            character.Init(characterProperty, Team.Player);
            BattleManager.Instance.InitBattle(character);

        }


    }

}
