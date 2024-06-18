using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    GameObject parede;
    GameObject pB;
    public float speed = 0.15f; // Velocidade de movimento ajustável

    // Start é chamado antes do primeiro quadro de atualização
    void Start()
    {
        parede = GameObject.Find("parede assustadora");
        pB = GameObject.Find("ponto B");
         // Verifique o nome correto no Unity
    }

    // Update é chamado uma vez por quadro
    void Update()
    {
        // Verifica se o jogador encostou no ponto boneco 
                // Move o GameObject atual em direção ao ponto B
                
                    transform.position = Vector3.MoveTowards(transform.position, pB.transform.position, speed * Time.deltaTime);
    }
}
