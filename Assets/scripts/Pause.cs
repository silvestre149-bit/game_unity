using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject painelPause;
    private bool jogoPausado = false;
    private float tempoPausado;

    public void PausarJogo()
    {
        jogoPausado = true;
        tempoPausado = Time.timeScale;
        Time.timeScale = 0f; // Pausa o tempo do jogo
        painelPause.SetActive(true);
    }

    public void ContinuarJogo()
    {
        jogoPausado = false;
        Time.timeScale = tempoPausado; // Restaura o tempo do jogo
        painelPause.SetActive(false);
    }

    public void ReiniciarJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CarregarProximoNivel1()
    {
        SceneManager.LoadScene("Jogo 2");
    }

    public void CarregarProximoNivel2()
    {
        SceneManager.LoadScene("Jogo 3");
    }

    public void CarregarProximoNivel3()
    {
        SceneManager.LoadScene("Jogo 1");
    }

    public void CarregarNivelAnterior1()
    {
        SceneManager.LoadScene("Jogo 3");        
    }
    public void CarregarNivelAnterior2()
    {
        SceneManager.LoadScene("Jogo 1");        
    }
    public void CarregarNivelAnterior3()
    {
        SceneManager.LoadScene("Jogo 2");        
    }

    public void CarregarTelaInicial()
    {
        SceneManager.LoadScene("TelaInicial");
    }

    public void AlternarSom()
    {
        AudioListener.pause = !AudioListener.pause;
    }
}
