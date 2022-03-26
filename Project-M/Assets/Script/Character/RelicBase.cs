using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RelicBase {
    public abstract void OnAdd(CharacterBase character);
    public abstract void OnRemove(CharacterBase character);

}


public class RelicManager {
    public CharacterBase Character;

    public RelicManager(CharacterBase character) {
        Character = character;
    }

    public void AddRelic(RelicBase relic) {
        relic.OnAdd(Character);
    }

    public void RemoveRelic(RelicBase relic) {
        relic.OnRemove(Character);
    }
}


public class TestRelic : RelicBase {
    private Buff atkUp = new AttackUp();
    public override void OnAdd(CharacterBase character) {
        throw new System.NotImplementedException();
    }

    public override void OnRemove(CharacterBase character) {
        throw new System.NotImplementedException();
    }
}