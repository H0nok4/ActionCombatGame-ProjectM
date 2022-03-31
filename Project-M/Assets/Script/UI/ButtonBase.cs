using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace MyUI {
    public class ButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
        [SerializeField] GameObject SelectArrow;
        public void OnPointerEnter(PointerEventData eventData) {
            //TODO:������룬��ʾ���
            if (SelectArrow != null) {
                SelectArrow.gameObject.SetActive(true);
            }

        }

        public void OnPointerExit(PointerEventData eventData) {
            //TODO:����Ƴ������ر��
            if (SelectArrow != null) {
                SelectArrow.gameObject.SetActive(false);
            }
        }

        public void OnPointerClick(PointerEventData eventData) {
            //TODO:���������Ӧ�¼�
            OnClick();

        }

        public virtual void OnClick() {
            Debug.Log("��û��ʵ�ֵ���¼���ButtonUI");
        }
    }
}

