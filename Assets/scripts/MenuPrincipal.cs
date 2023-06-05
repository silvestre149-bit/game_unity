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

    public void Sair()
    {
        painelSobre.SetActive(false);
        painelNiveis.SetActive(false);
        painelComoJogar.SetActive(false);
        painelMenu.SetActive(true);   
    }
    
    public void AbrirNivel(){
        painelMenu.SetActive(false);
        painelNiveis.SetActive(true);    
    }

}