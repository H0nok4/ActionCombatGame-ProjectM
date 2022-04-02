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
            //TODO:������룬��ʾ���
            if (SelectArrow != null) {
                SelectArrow.gameObject.SetActive(true);
            }
            OnMouseEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData) {
            //TODO:����Ƴ������ر��
            if (SelectArrow != null) {
                SelectArrow.gameObject.SetActive(false);
            }
            OnMouseExit?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData) {
            //TODO:���������Ӧ�¼�
            OnClick();
            OnMouseClick?.Invoke();
        }

        public virtual void OnClick() {
            Debug.Log("��û��ʵ�ֵ���¼���ButtonUI");
        }
    }
}

