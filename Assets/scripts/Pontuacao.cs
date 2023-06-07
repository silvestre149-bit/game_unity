using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pontuacao : MonoBehaviour
{
    public TextMeshProUGUI pontuacaoText; // Referência ao objeto de texto da pontuação
    private float tempoInicial;
    private bool jogoTerminado = false;
    public int PontuacaoFinal { get; private set; } // Isso deve ser uma propriedade, não uma variável


    public float GetTempoInicial()
    {
        return tempoInicial;
    }

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
        PontuacaoFinal = Mathf.FloorToInt(Time.time - tempoInicial);
        MostrarPontuacao();
    }

    void MostrarPontuacao()
    {
        Debug.Log("Pontuação: " + PontuacaoFinal);
    }
}
