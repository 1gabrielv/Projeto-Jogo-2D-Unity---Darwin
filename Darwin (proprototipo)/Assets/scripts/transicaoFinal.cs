using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transicaoFinal : MonoBehaviour
{
    [SerializeField]
    private string nome;

    public float time;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("player")){
            Invoke("fases", time);
        }
    }

    public void fases(){
        SceneManager.LoadScene(this.nome, LoadSceneMode.Single);
    }
}
