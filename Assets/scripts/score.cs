using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Score
{
    public string nome;
    public int pontos;
    public float tempo;

    public Score(string nome, int pontos, float tempo)
    {
        this.nome = nome;
        this.pontos = pontos;
        this.tempo = tempo;
    }
}


