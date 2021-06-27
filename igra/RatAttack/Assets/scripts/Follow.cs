using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    public enum MovementType
    {
        Moving, Lerping
    }

    public MovementType Type = MovementType.Moving; //определяем вид движения
    public Move myPath; //определяем используемый путь
    public float speed = 1; //определяем скорость движения
    public float maxDistance = .1f; //определяем расстояние до точки, где объект "поймет" что нужно дергаться

    private IEnumerator<Transform> pointInPath; // проверка точек

    // Start is called before the first frame update
    void Start()
    {
        if (myPath==null)// проверка прикрепленного пути
        { Debug.Log("Путь не прикреплен");
            return;
        }

        pointInPath = myPath.GetNextPathPoint();//обращаемся к коротину GetNextPathPoint
        pointInPath.MoveNext();//получаем следующую точку

        if (pointInPath.Current == null)//есть ли точка к торой нужно двинуться
        { Debug.Log("Нужны точки");
            return;
        }
        transform.position = pointInPath.Current.position; // объект встанет на стартовую точку
    }

    // Update is called once per frame
    void Update()
    {
        if (pointInPath == null || pointInPath.Current == null)//проверка отсутствия пути
        { return;  }//пути нет, рвем цикл

        if (Type == MovementType.Moving)//если выбран этот вид
        { transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * speed); }//двигаем объект к след точке с определенной скоростью

        else if (Type == MovementType.Lerping)//если выбран этот вид
        { transform.position = Vector3.Lerp(transform.position, pointInPath.Current.position, Time.deltaTime * speed); }//дрыгаем объект к след точке

        var distanceSquare = (transform.position - pointInPath.Current.position).sqrMagnitude;//достаточно ли близки к точке, чтоб двигать к следующей
        if (distanceSquare < maxDistance * maxDistance) // достаточно ли мы близки (проверка по т.Пифагора)
        { pointInPath.MoveNext(); }
    }
}
