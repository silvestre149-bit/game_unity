using UnityEngine;
using UnityEngine.UI;

public class Pontuacao : MonoBehaviour
{
    public Text pontuacaoText; // Referência ao objeto de texto da pontuação
    private float tempoInicial;
    private bool jogoTerminado = false;

    void Start()
    {
        tempoInicial = Time.time;
    }

    void Update()
    {
        if (jogoTerminado)
        {
            return;
        }

        float tempoAtual = Time.time - tempoInicial;
        int pontuacao = Mathf.FloorToInt(tempoAtual);
        pontuacaoText.text = "Pontuação: " + pontuacao.ToString();
    }

    public void FinalizarJogo()
    {
        jogoTerminado = true;
        MostrarPontuacao();
    }

    void MostrarPontuacao()
    {
        int pontuacao = Mathf.FloorToInt(Time.time - tempoInicial);
        Debug.Log("Pontuação: " + pontuacao);
    }
}
