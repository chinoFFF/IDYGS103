using TMPro;
using UnityEngine;

public class BanderaPuntos : MonoBehaviour
{
    // Lugares donde puede aparecer la bandera
    public Transform[] posiciones;

    // Textos de los puntos
    public TextMeshProUGUI textoJugador1;
    public TextMeshProUGUI textoJugador2;
    public TextMeshProUGUI textoGanador;

    private int puntosJugador1 = 0;
    private int puntosJugador2 = 0;

    private int limitePuntos = 10;
    private int ultimaPosicion = -1;

    private bool juegoTerminado = false;

    void Start()
    {
        Time.timeScale = 1f;

        textoGanador.gameObject.SetActive(false);

        ActualizarPuntos();
        MoverBandera();
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (juegoTerminado) return;

        // Si la bolita roja toca la bandera
        if (otro.GetComponent<MovimientoBolita>() != null)
        {
            puntosJugador1++;
            Debug.Log("Jugador 1: " + puntosJugador1);
        }

        // Si la bolita azul toca la bandera
        else if (otro.GetComponent<MovimientoBolita2>() != null)
        {
            puntosJugador2++;
            Debug.Log("Jugador 2: " + puntosJugador2);
        }
        else
        {
            return;
        }

        ActualizarPuntos();

        // Comprobar si alguien gano
        if (puntosJugador1 >= limitePuntos)
        {
            Ganar("Jugador 1");
        }
        else if (puntosJugador2 >= limitePuntos)
        {
            Ganar("Jugador 2");
        }
        else
        {
            MoverBandera();
        }
    }

    void ActualizarPuntos()
    {
        textoJugador1.text = "Rojo: " + puntosJugador1 + "/10";
        textoJugador2.text = "Azul: " + puntosJugador2 + "/10";
    }

    void MoverBandera()
    {
        if (posiciones.Length == 0) return;

        int nuevaPosicion;

        // Elegir una posicion diferente
        if (posiciones.Length == 1)
        {
            nuevaPosicion = 0;
        }
        else
        {
            do
            {
                nuevaPosicion = Random.Range(0, posiciones.Length);
            }
            while (nuevaPosicion == ultimaPosicion);
        }

        ultimaPosicion = nuevaPosicion;

        transform.position = posiciones[nuevaPosicion].position;
    }

    void Ganar(string jugador)
    {
        juegoTerminado = true;

        textoGanador.text = "¡GANÓ " + jugador + "!";
        textoGanador.gameObject.SetActive(true);

        Debug.Log("Ganador: " + jugador);

        // Ocultar bandera y detener el juego
        gameObject.SetActive(false);
        Time.timeScale = 0f;
    }
}
