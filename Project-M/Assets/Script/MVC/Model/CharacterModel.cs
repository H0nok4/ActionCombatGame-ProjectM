using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel
{
    //角色实例GameObejct
    public GameObject _characterObject;
    public GameObject _characterSprite;
    public int Hp;
    public int Def;
    public int Att;
    public int moveSpeed = 3;
    public Rigidbody2D CharacterRig;
    public Animator animator;

    public CharacterModel(GameObject characterObject,GameObject characterSprite) {
        _characterObject = characterObject;
        _characterSprite = characterSprite;
        var rig = _characterObject.GetComponent<Rigidbody2D>();
        if (rig == null) {
            CharacterRig = _characterObject.AddComponent<Rigidbody2D>();
        } else {
            CharacterRig = rig;
        }


        animator = _characterSprite.GetComponent<Animator>();
        //TODO:可能会空的处理
    }

    public void Move(int x,int y) {
        animator.SetBool("IsMove",true);
        CharacterRig.transform.Translate(new Vector3(x,y) * moveSpeed * Time.deltaTime);
        Debug.Log($"MoveX = {x},MoveY = {y}");
    }

}
