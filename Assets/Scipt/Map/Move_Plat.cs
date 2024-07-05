using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Plat : MonoBehaviour
{
    [SerializeField] private Transform[] plat_Point; // cac diem tren quy dao
    private int platPoint_Index = 0; //bien index thay doi diem den cua plat
    [SerializeField] private float speed;
    void Update()
    {
        // xet khoang cach giua moveplat va cac point
        if(Vector2.Distance(transform.position, plat_Point[platPoint_Index].position) < .1f)
        {

            platPoint_Index++;
            if(platPoint_Index >= plat_Point.Length) // neu da di qua cac point, tro ve diem ban dau
            {
                platPoint_Index = 0;
            }
        }
        // cap nhat vi tri platform tu vi tri cu den point co chi so index
        transform.position = Vector2.MoveTowards(transform.position, plat_Point[platPoint_Index].position, Time.deltaTime * speed);
    }

}
