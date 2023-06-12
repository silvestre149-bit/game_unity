using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FimDeJogo : MonoBehaviour
{
    public GameObject gameOverUI;
    public TextMeshProUGUI novoTextoPontuacao;
    public TextMeshProUGUI novoTextoTempo;
    public TextMeshProUGUI textoPontuacaoDuranteJogo;
    public TextMeshProUGUI textoTempoDuranteJogo;
    public TextMeshProUGUI seuTextoPosicaoRanking;
    private bool jogoTerminado = false;
    private Pontuacao pontuacao;
    public AudioSource audioSource;
    public AudioClip somDeFundo;
    public AudioClip somGameOver;
    public static int duracaoDoJogo;





    void Start()
    {
        audioSource.clip = somDeFundo;
        audioSource.loop = true;
        audioSource.Play();
        GameObject pontuacaoObj = GameObject.FindGameObjectWithTag("Pontuacao");
        if (pontuacaoObj != null)
        {
            pontuacao = pontuacaoObj.GetComponent<Pontuacao>();
            /*Limpar();*/
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Agua") && !jogoTerminado)
        {
            jogoTerminado = true;
            GameObject pontuacaoObj = GameObject.FindGameObjectWithTag("Pontuacao");
            if (pontuacaoObj != null)
            {
                Pontuacao pontuacao = pontuacaoObj.GetComponent<Pontuacao>();
                if (pontuacao != null)
                {
                    pontuacao.FinalizarJogo();
                    AtualizarPontuacaoFinal(pontuacao.PontuacaoFinal);
                    MostrarGameOver();
                }
                else
                {
                    Debug.LogWarning("O GameObject com a tag 'Pontuacao' não tem um componente Pontuacao.");
                }
            }
            else
            {
                Debug.LogWarning("Não há GameObject com a tag 'Pontuacao' na cena.");
            }
        }
    }

    void MostrarGameOver()
    {
        gameOverUI.SetActive(true);
        GameObject pontuacaoObj = GameObject.FindGameObjectWithTag("Pontuacao");
        audioSource.clip = somGameOver;
        audioSource.loop = false;
        audioSource.Play();

        if (pontuacaoObj != null)
        {
            Pontuacao pontuacao = pontuacaoObj.GetComponent<Pontuacao>();
            if (pontuacao != null)
            {
                textoPontuacaoDuranteJogo.enabled = false;
                textoTempoDuranteJogo.enabled = false;
                novoTextoPontuacao.enabled = true;
                novoTextoTempo.enabled = true;

                novoTextoPontuacao.text = "Pontuacao: " + pontuacao.PontuacaoFinal;
                novoTextoTempo.text = FormatTime(Time.time - pontuacao.GetTempoInicial());

                pontuacao.PrintScores();
            }
            else
            {
                Debug.LogWarning("O GameObject com a tag 'Pontuacao' não tem um componente Pontuacao.");
            }
        }
        else
        {
            Debug.LogWarning("Não há GameObject com a tag 'Pontuacao' na cena.");
        }

        int posicaoRanking = pontuacao.ObterPosicaoRanking();
        if (posicaoRanking != -1)
        {
            // Exiba a posição no ranking na tela. Você precisará adicionar um novo TextMeshProUGUI para isso
            seuTextoPosicaoRanking.text = "Você ficou na posição: " + posicaoRanking;
        }
        else
        {
            seuTextoPosicaoRanking.text = "Sua pontuação não foi suficiente para entrar no ranking.";
        }

    }
    public void AtualizarPontuacaoFinal(int pontuacaoFinal)
    {
        novoTextoPontuacao.text = pontuacaoFinal + " pontos";
    }

    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds - minutes * 60);
        string formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        return formattedTime;
    }
    public void ReiniciarJogo()
    {
        Time.timeScale = 1; // Volta o jogo ao normal
        int duracaoDoJogo = PlayerPrefs.GetInt("TempoJogo");
        Debug.Log("aqui" + duracaoDoJogo);
        PlayerPrefs.SetInt("TempoJogo", duracaoDoJogo);// Armazena o tempo do jogo em segundos
        SceneManager.LoadScene("Jogo");
    }


    public void IrParaRanking()
    {
        PlayerPrefs.SetInt("MostrarRanking", 1); // definindo o sinalizador
        SceneManager.LoadScene("TelaInicial");
    }
    public void Limpar()
    {
        pontuacao.LimparClassificacao();
    }

    public void Sair()
    {
        Time.timeScale = 1; //Volta ao jogo normal
        SceneManager.LoadScene("TelaInicial");
    }
    public void AlternarSom()
    {
        audioSource.mute = !audioSource.mute;
    }
}
