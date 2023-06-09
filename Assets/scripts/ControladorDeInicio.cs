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

    public void IniciarJogo()
    {
        string nome = campoNome.text;
        PlayerPrefs.SetString("NomeJogador", nome);

        int tempoJogo = 0;
        if (toggle5Minutos.isOn) tempoJogo = 5;
        else if (toggle10Minutos.isOn) tempoJogo = 10;
        else if (toggle15Minutos.isOn) tempoJogo = 15;

        PlayerPrefs.SetInt("TempoJogo", tempoJogo * 60); // Armazena o tempo do jogo em segundos
        SceneManager.LoadScene("Jogo");
    }
}
