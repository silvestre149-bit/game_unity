using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class Pontuacao : MonoBehaviour
{
    public TextMeshProUGUI pontuacaoText;
    public TextMeshProUGUI tempoText;
    private float tempoInicial;
    private float tempoRestante;
    private int pontuacao;
    public int fatorPontuacao = 10;
    private bool jogoTerminado = false;

    public int PontuacaoFinal { get; private set; }

    public float GetTempoInicial()
    {
        return tempoInicial;
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("TempoJogo"))
        {
            // Obter a duração do jogo que foi definida no menu de início
            int duracaoDoJogo = PlayerPrefs.GetInt("TempoJogo");

            // Remova a chave para que não afete futuras execuções do jogo
            PlayerPrefs.DeleteKey("TempoJogo");

            // Iniciar o jogo com a duração definida
            IniciarJogo(duracaoDoJogo);
        }
    }

    public void IniciarJogo(int duracaoDoJogo)
    {
        tempoInicial = Time.time;
        tempoRestante = duracaoDoJogo;
        pontuacao = 0;
        jogoTerminado = false;
        UpdateUI();
    }

    void Update()
    {
        if (jogoTerminado)
        {
            return;
        }

        tempoRestante -= Time.deltaTime;
        if (tempoRestante <= 0)
        {
            tempoRestante = 0;
            FinalizarJogo();
        }

        pontuacao = Mathf.FloorToInt((Time.time - tempoInicial) * fatorPontuacao);

        UpdateUI();
    }


    public void FinalizarJogo()
    {
        jogoTerminado = true;
        PontuacaoFinal = Mathf.FloorToInt((Time.time - tempoInicial) * fatorPontuacao);
        Debug.Log("Pontuação: " + PontuacaoFinal);
        SalvarPontuacaoFinal();
    }


    void MostrarPontuacao()
    {
        Debug.Log("Pontuação: " + PontuacaoFinal);
    }

    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds - minutes * 60);
        string formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        return formattedTime;
    }


    public void SalvarPontuacaoFinal()
    {
        List<Score> scores = CarregarScores();
        string nomeJogador = PlayerPrefs.GetString("NomeJogador", "Anônimo");
        float tempoFinal = Time.time - tempoInicial;
        Score novoScore = new Score(nomeJogador, pontuacao, tempoFinal);

        scores.Add(novoScore);

        // Ordenar a lista scores em ordem decrescente com base na pontuação.
        scores = scores.OrderByDescending(score => score.pontos).ThenBy(score => score.tempo).ToList();

        // Remover o último elemento se a lista scores tiver mais que 5 elementos.
        if (scores.Count > 10)
        {
            scores.RemoveAt(scores.Count - 1);
        }

        // Salvar a lista scores em PlayerPrefs
        string json = JsonUtility.ToJson(new ScoreList(scores));
        PlayerPrefs.SetString("scores", json);
    }
    public int ObterPosicaoRanking()
    {
        List<Score> scores = CarregarScores();
        for (int i = 0; i < scores.Count; i++)
        {
            if (scores[i].nome == PlayerPrefs.GetString("NomeJogador", "Anônimo"))
            {
                return i + 1; // Adicionamos 1 porque a lista é baseada em 0
            }
        }
        return -1; // Se o jogador não estiver na lista, retornamos -1
    }

    public List<Score> CarregarScores()
    {
        string json = PlayerPrefs.GetString("scores", "");
        if (string.IsNullOrEmpty(json))
        {
            return new List<Score>();
        }
        else
        {
            ScoreList scoreList = JsonUtility.FromJson<ScoreList>(json);
            return scoreList.scores;
        }
    }

    public void LimparClassificacao()
    {
        PlayerPrefs.DeleteKey("scores");
    }

    public void PrintScores()
    {
        List<Score> scores = CarregarScores();
        if (scores.Count > 0)
        {
            for (int i = 0; i < Mathf.Min(5, scores.Count); i++) // Aqui fazemos a alteração
            {
                Score score = scores[i];
                Debug.Log("Nome: " + score.nome + ", Pontuação: " + score.pontos + ", Tempo: " + score.tempo);
            }
        }
        else
        {
            Debug.Log("Nenhum score encontrado.");
        }

    }

    private void UpdateUI()
    {
        pontuacaoText.text = "Pontuação: " + pontuacao.ToString();
        int minutos = Mathf.FloorToInt(tempoRestante / 60F);
        int segundos = Mathf.FloorToInt(tempoRestante - minutos * 60);
        tempoText.text = string.Format("Tempo: {0:0}:{1:00}", minutos, segundos);
    }

}

[System.Serializable]
public class ScoreList
{
    public List<Score> scores;

    public ScoreList(List<Score> scores)
    {
        this.scores = scores;
    }
}

