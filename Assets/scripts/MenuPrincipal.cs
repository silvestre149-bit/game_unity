using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenu;
    [SerializeField] private GameObject painelComoJogar;
    [SerializeField] private GameObject painelSobre;
    [SerializeField] private GameObject painelNiveis;
    [SerializeField] private GameObject painelNome;
    [SerializeField] private GameObject painelRanking;
    [SerializeField] private GameObject painelSelecaoTempoRanking;
    [SerializeField] private GameObject painelSelecaoNivelRanking;
    private ControladorDeInicio controladorDeInicio;
    private Pontuacao pontuacao;
    private int nivelSelecionado;
    private int tempoSelecionado;

    /*private bool mostrarRanking = false;*/

    public void Jogar()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }

    public void AbrirComoJogar()
    {
        painelMenu.SetActive(false);
        painelComoJogar.SetActive(true);
    }

    public void Sobre()
    {
        painelMenu.SetActive(false);
        painelSobre.SetActive(true);
    }

    void Awake()
    {
        pontuacao = FindObjectOfType<Pontuacao>();
        controladorDeInicio = GameObject.FindObjectOfType<ControladorDeInicio>();
        if (PlayerPrefs.GetInt("MostrarRanking", 0) == 1)
        {
            // Resetar o sinalizador
            PlayerPrefs.SetInt("MostrarRanking", 0);

            // Abrir a tela de seleção de nível no ranking
            painelMenu.SetActive(false);
            painelRanking.SetActive(false);
            painelSelecaoNivelRanking.SetActive(true);
        }
        else
        {
            painelMenu.SetActive(true);
            painelNiveis.SetActive(false);
            painelNome.SetActive(false);
            painelRanking.SetActive(false);
        }
    }

    public void SelecionarNivelRanking(int nivel)
    {
        PlayerPrefs.SetInt("NivelSelecionadoRanking", nivel);
        painelSelecaoNivelRanking.SetActive(false);
        painelSelecaoTempoRanking.SetActive(true);
    }

    public void Sair()
    {
        painelSobre.SetActive(false);
        painelNiveis.SetActive(false);
        painelComoJogar.SetActive(false);
        painelRanking.SetActive(false);
        painelNome.SetActive(false);
        painelMenu.SetActive(true);
        SceneManager.LoadScene("TelaInicial");
    }

    public void SelecionarTempoRanking(int tempo)
    {
        PlayerPrefs.SetInt("TempoSelecionadoRanking", tempo);
        painelSelecaoTempoRanking.SetActive(false);
        painelRanking.SetActive(true);
    }


    public void AbrirNivel()
    {
        painelMenu.SetActive(false);
        painelNiveis.SetActive(true);
    }

    public void AbrirNome()
    {
        painelNiveis.SetActive(false);
        painelNome.SetActive(true);
       
    }

    public void AbrirRanking()
    {
        /*mostrarRanking = true;*/
        painelMenu.SetActive(false);
        painelRanking.SetActive(true);
    }


}




