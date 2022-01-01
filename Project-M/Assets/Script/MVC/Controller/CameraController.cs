using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject CharacterObject;//ÁÙÊ±²âÊÔÓÃ

    private void FixedUpdate() {
        var mouseWorldPosition = GetPlayerMouseWorldPosition();
        Debug.Log($"mouseWorldPosition = {mouseWorldPosition}");
        var characterPos = CharacterObject.transform.position;
        float targetCameraPosX = ((mouseWorldPosition)).x;
        targetCameraPosX = Mathf.Clamp(targetCameraPosX,characterPos.x - 3,characterPos.x + 3);
        float targetCameraPosY = ((mouseWorldPosition)).y;
        targetCameraPosY = Mathf.Clamp(targetCameraPosY,characterPos.y - 3,characterPos.y + 3);
        var cameraPos = Camera.main.transform.position;
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
