using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ControladorDeInicio : MonoBehaviour
{
    public TMP_InputField campoNome;
    public Toggle toggle5Minutos;
    public Toggle toggle10Minutos;
    public Toggle toggle15Minutos;

    private int nivelSelecionado;
    public static int tempoJogo;

    public void IniciarJogo()
    {
        string nome = campoNome.text;
        PlayerPrefs.SetString("NomeJogador", nome);

        int tempoJogo = 0;
        if (toggle5Minutos.isOn) tempoJogo = 5;
        else if (toggle10Minutos.isOn) tempoJogo = 10;
        else if (toggle15Minutos.isOn) tempoJogo = 15;

        PlayerPrefs.SetInt("TempoJogo", tempoJogo * 60); // Armazena o tempo do jogo em segundos

        switch (nivelSelecionado)
        {
            case 1:
                SceneManager.LoadScene("Jogo");
                break;
            case 2:
                SceneManager.LoadScene("Jogo 1");
                break;
            case 3:
                SceneManager.LoadScene("Jogo 2");
                break;
            default:
                Debug.LogError("Nível selecionado inválido!");
                break;
        }
    }

    public void SelecionarNivel(int nivel)
    {
        nivelSelecionado = nivel;
        PlayerPrefs.SetInt("Nível", nivelSelecionado);
        /*PlayerPrefs.Save();*/
    }
}
