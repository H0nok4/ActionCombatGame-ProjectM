using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject CharacterObject;//临时测试用

    private void FixedUpdate() {
        //只是测试用，后续需要抽象成方法，并且修改各个参数
        var mouseWorldPosition = GetPlayerMouseWorldPosition();
        Debug.Log($"mouseWorldPosition = {mouseWorldPosition}");
        var characterPos = CharacterObject.transform.position;
        float targetCameraPosX = ((mouseWorldPosition)).x;
        targetCameraPosX = Mathf.Clamp(targetCameraPosX,characterPos.x - 3,characterPos.x + 3);
        float targetCameraPosY = ((mouseWorldPosition)).y;
        targetCameraPosY = Mathf.Clamp(targetCameraPosY,characterPos.y - 3,characterPos.y + 3);
        var cameraPos = Camera.main.transform.position;

        //因为当鼠标靠近玩家角色时，玩家的目标可能就在眼前，不应该拉长镜头，故只在鼠标位置超出玩家角色位置过多的时候才拉长镜头
        if (targetCameraPosX >= characterPos.x + 3 || targetCameraPosX <= characterPos.x - 3 || targetCameraPosY >= characterPos.y + 3 || targetCameraPosY <= characterPos.y - 3) {
            Camera.main.transform.position = new Vector3(Mathf.Lerp(cameraPos.x,targetCameraPosX,3 * Time.deltaTime),Mathf.Lerp(cameraPos.y,targetCameraPosY,3 * Time.deltaTime),-10);
        } else {
            Camera.main.transform.position = new Vector3(Mathf.Lerp(cameraPos.x,characterPos.x,3 * Time.deltaTime),Mathf.Lerp(cameraPos.y,characterPos.y,3 * Time.deltaTime),-10);
        }
        


    }

    public Vector3 GetPlayerMouseWorldPosition() {
        var mousePosition = new Vector2(Input.mousePosition.x,Input.mousePosition.y);


        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
