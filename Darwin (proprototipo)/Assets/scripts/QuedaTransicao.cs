using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuedaTransicao : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        PlayerPrefs.SetString("UltimaFase", SceneManager.GetActiveScene().name);
        if(movePlayer.fasemorte){
            movePlayer.fasemorte = false;
            Invoke("fase", 0.5f);
        }
        else{
            movePlayer.fasemorte = true;
            Invoke("gameover", 0.8f);
        }
    }

    private void fase(){
        SceneManager.LoadScene("morte", LoadSceneMode.Single);
    }
    private void gameover(){
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
