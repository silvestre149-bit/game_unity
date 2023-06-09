using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ExibirRanking : MonoBehaviour
{
    public TextMeshProUGUI[] nomeTexts;  // array com referências para os 5 campos de nome
    public TextMeshProUGUI[] pontuacaoTexts;  // array com referências para os 5 campos de pontuação

    void Start()
    {
        AtualizarRanking();
    }

    public void AtualizarRanking()
    {
        // Carregar pontuações
        string json = PlayerPrefs.GetString("scores", "");
        List<Score> scores = new List<Score>();
        if (!string.IsNullOrEmpty(json))
        {
            ScoreList scoreList = JsonUtility.FromJson<ScoreList>(json);
            scores = scoreList.scores;
        }

        // Limpar texto existente em cada campo
        for (int i = 0; i < nomeTexts.Length && i < pontuacaoTexts.Length; i++)
        {
            nomeTexts[i].text = "";
            pontuacaoTexts[i].text = "";
        }

        // Atualizar texto com os 5 primeiros jogadores
        for (int i = 0; i < Mathf.Min(5, scores.Count); i++)
        {
            Score score = scores[i];
            nomeTexts[i].text = score.nome;
            pontuacaoTexts[i].text = score.pontos.ToString() + " Pontos";
        }
    }
}
