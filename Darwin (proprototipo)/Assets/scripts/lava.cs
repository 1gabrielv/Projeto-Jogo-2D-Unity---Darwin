using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lava : MonoBehaviour
{
    public float targetY = 80f; // Ponto alvo no eixo Y
    public float speed = 1.4f;    // Velocidade do movimento

    void Update()
    {
        // Pega a posição atual da parede
        Vector3 currentPosition = transform.position;

        // Verifica se a parede já alcançou a posição alvo
        if (currentPosition.y < targetY)
        {
            // Move a parede em direção ao ponto alvo no eixo Y
            float newY = Mathf.MoveTowards(currentPosition.y, targetY, speed * Time.deltaTime);
            transform.position = new Vector3(currentPosition.x, newY, currentPosition.z);
        }
    }
}
