using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public enum PathTypes //делаем два вида пути - линейный и зацикленный
    {
        linear, loop
    }
    public PathTypes PathType; //тип пути
    public int movementDirection = 1; //направление движения
    public int movingTo = 0; //выбираем точку к которой двигаться надо
    public Transform[] PathElements; // создаем массив из опорных точек движения

    public void OnDrawGizmos() //визуал, помогающий видеть линии меж точками
    {
        if (PathElements == null || PathElements.Length < 2) //проверка на 2 эл-та пути
        { return; }

        for (var i = 1; i < PathElements.Length; i++)//прогон по точкам массива
        { Gizmos.DrawLine(PathElements[i - 1].position, PathElements[i].position);/*рисует линии меж точками*/ }

        if (PathType == PathTypes.loop) //на слчай если путь замкнулся
        { Gizmos.DrawLine(PathElements[0].position, PathElements[PathElements.Length - 1].position); /*рисует линии меж перой и последней точками*/ }
    }


    public IEnumerator<Transform> GetNextPathPoint()//получает положение следующей точки
    {
        if (PathElements == null || PathElements.Length < 1) //проверяет точки которыйм нужно просчитать положение
        { yield break; } // выходим из коротина, если есть несоответсвие


        while (true)
        {
            yield return PathElements[movingTo]; //возвращаем текущее положение точки
            if (PathElements.Length == 1)//если точка одна, то выходим из цикла
            { continue; }

            if (PathType == PathTypes.linear)//если линия не зациклена
            {
                if (movingTo <= 0) //если двигаемся по нарастанию
                { movementDirection = 1; }//добавляем 1 к движению

                else if (movingTo >= PathElements.Length - 1)//если двигаемся по убыванию
                { movementDirection = -1; } //минус 1 из движения

            }

                movingTo = movingTo + movementDirection; //диапазон движения от 1 до -1
            if (PathType==PathTypes.loop) //если зациклена линия
            { 
                    if (movingTo>=PathElements.Length) // если дошли до последней точки
                    { movingTo = 0; }// идем к первой точке

                    if (movingTo < 0)// если пришлир до первой точки
                    { movingTo = PathElements.Length - 1; } // идем в последнюю
            }    

            

                
        }
    }
}

        

