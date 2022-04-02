using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace MyUI {
    public class ButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
        [SerializeField] GameObject SelectArrow;
        public Action OnMouseClick;
        public Action OnMouseEnter;
        public Action OnMouseExit;
        public void OnPointerEnter(PointerEventData eventData) {
            //TODO:鼠标移入，显示标记
            if (SelectArrow != null) {
                SelectArrow.gameObject.SetActive(true);
            }
            OnMouseEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData) {
            //TODO:鼠标移除，隐藏标记
            if (SelectArrow != null) {
                SelectArrow.gameObject.SetActive(false);
            }
            OnMouseExit?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData) {
            //TODO:鼠标点击，响应事件
            OnClick();
            OnMouseClick?.Invoke();
        }

        public virtual void OnClick() {
            Debug.Log("有没有实现点击事件的ButtonUI");
        }
    }
}

