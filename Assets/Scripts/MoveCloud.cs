using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCloud : MonoBehaviour {

    public float moveCloud; // Скорость движения обьекта

    void Start()
    {
        moveCloud = Random.Range(1.5f, 2.5f); //Инициализация случайной скорости объекта
    }

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * moveCloud); //Движение облака в левую сторону
        Destroy(gameObject, 35f); //Уничтожение обьекта через определенное время
    }
}
