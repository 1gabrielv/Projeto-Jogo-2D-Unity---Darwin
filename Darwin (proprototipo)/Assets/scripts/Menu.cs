using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private string nome;
    // Start is called before the first frame update
    void Start()
    {
        movePlayer.fasemorte = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void jogar(){
        SceneManager.LoadScene(this.nome, LoadSceneMode.Single);
    }

    public void Quit(){
        Application.Quit();
    }

}
