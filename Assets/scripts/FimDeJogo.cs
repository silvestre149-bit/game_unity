using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FimDeJogo : MonoBehaviour
{
    public GameObject gameOverUI;
    public TextMeshProUGUI textoPontuacao;
    public TextMeshProUGUI textoTempo;
    private bool jogoTerminado = false;
    private Pontuacao pontuacao;
    public AudioSource audioSource;
    public AudioClip somDeFundo;
    public AudioClip somGameOver;

    void Start()
    {
        audioSource.clip = somDeFundo;
        audioSource.loop = true;
        audioSource.Play();
        GameObject pontuacaoObj = GameObject.FindGameObjectWithTag("Pontuacao");
        if (pontuacaoObj != null)
        {
            pontuacao = pontuacaoObj.GetComponent<Pontuacao>();
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Agua") && !jogoTerminado)
        {
            jogoTerminado = true;
            MostrarGameOver();
            GameObject pontuacaoObj = GameObject.FindGameObjectWithTag("Pontuacao");
            if (pontuacaoObj != null)
            {
                Pontuacao pontuacao = pontuacaoObj.GetComponent<Pontuacao>();
                if (pontuacao != null)
                {
                    pontuacao.FinalizarJogo();
                    AtualizarPontuacaoFinal(pontuacao.PontuacaoFinal);
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
                textoPontuacao.text = "Pontuacao: " + pontuacao.PontuacaoFinal;
                textoTempo.text = "Tempo: " + Mathf.FloorToInt(Time.time - pontuacao.GetTempoInicial());
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


    public void AtualizarPontuacaoFinal(int pontuacaoFinal)
    {
        textoPontuacao.text = "Pontuação: " + pontuacaoFinal;
    }


    public void ReiniciarJogo()
    {
        Time.timeScale = 1; // Volta o jogo ao normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
