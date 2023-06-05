using UnityEngine;
using UnityEngine.SceneManagement;

public class FimDeJogo : MonoBehaviour
{
    public GameObject gameOverUI; // Referência para o objeto da interface de Game Over
    private bool jogoTerminado = false;

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
                pontuacao.FinalizarJogo();
            }
        }
    }

    void MostrarGameOver()
    {
        gameOverUI.SetActive(true);
        // Você pode adicionar aqui qualquer ação adicional que desejar quando o jogo terminar
    }

    public void ReiniciarJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
