using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler //добавлены классы, которые отвечают за считывание нажатия на экран, нажатия на джойстик и отжатия от экрана
{
    Image joyS, joyB;

    Vector2 inputVector;
    // Start is called before the first frame update
    void Start() //здесь мы делаем ссылки на джойстик
    {
        joyB = GetComponent<Image>();
        joyS = transform.GetChild(0).GetComponent<Image>();
    }
    public void OnPointerDown(PointerEventData eventData) //проверяем нажатие на экран
    {
        OnDrag(eventData);
    }
    public void OnDrag(PointerEventData eventData) //делаем позицию движений для джойстика
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joyB.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joyB.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joyB.rectTransform.sizeDelta.y);
        }
        inputVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);
        inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

        joyS.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joyB.rectTransform.sizeDelta.x / 2), inputVector.y * (joyB.rectTransform.sizeDelta.y / 2));
    }
    public void OnPointerUp(PointerEventData eventData) //при отжатии джойстика, он вернется в исходное положение
    {
        inputVector = Vector2.zero;
        joyS.rectTransform.anchoredPosition = Vector2.zero;
    }

    public float Horizontal() //перемещение персонажа по горизонтальной оси
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxisRaw("Horizontal");
    }
    public float Vertical() //перемещение персонажа по вертикальной оси
    {
        if (inputVector.y != 0)
            return inputVector.y;
        else
            return Input.GetAxisRaw("Vertical");
    }
}