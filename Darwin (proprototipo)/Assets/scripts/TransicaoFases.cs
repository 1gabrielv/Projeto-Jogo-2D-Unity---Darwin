using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransicaoFases : MonoBehaviour
{
    [SerializeField]
    private string nome;

    public float time;

    public GameObject texto;
    public GameObject fundo;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        texto.SetActive(true);
        fundo.SetActive(true);
        Invoke("fases", time);
    }

    public void fases(){
        SceneManager.LoadScene(this.nome, LoadSceneMode.Single);
        texto.SetActive(false);
        fundo.SetActive(false);
    }
}
