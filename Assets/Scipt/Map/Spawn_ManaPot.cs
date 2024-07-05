using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_ManaPot : MonoBehaviour
{
    [SerializeField] private GameObject ManaPot;
    void SpawnManaPot()
    {
        Vector2 spawn_pos = new Vector2(transform.position.x, transform.position.y + .7f);
        int random = Random.Range(1, 11);
        if (random >= 5) Instantiate(ManaPot, spawn_pos, transform.rotation); // ti le rot manaPot 60%
        else return;
    }
}
