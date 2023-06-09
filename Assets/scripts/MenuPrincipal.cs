using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenu;
    [SerializeField] private GameObject painelComoJogar;
    [SerializeField] private GameObject painelSobre;
    [SerializeField] private GameObject painelNiveis;
    [SerializeField] private GameObject painelNome;
    [SerializeField] private GameObject painelRanking;


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
        if (PlayerPrefs.GetInt("MostrarRanking", 0) == 1)
        {
            // Resetando o sinalizador
            PlayerPrefs.SetInt("MostrarRanking", 0);

            // Aqui você pode adicionar o código para abrir a tela de ranking
            painelMenu.SetActive(false);
            painelRanking.SetActive(true);
        }
    }


    public void Sair()
    {
        painelSobre.SetActive(false);
        painelNiveis.SetActive(false);
        painelComoJogar.SetActive(false);
        painelRanking.SetActive(false);
        painelNome.SetActive(false);
        painelMenu.SetActive(true);
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

}