using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class telatroca : MonoBehaviour
{
    [SerializeField]
    private string nome;

    public float time;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("troca", time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void troca(){
        SceneManager.LoadScene(this.nome, LoadSceneMode.Single);
    }
}
