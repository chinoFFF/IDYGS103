using UnityEngine;

public class ControlPausa : MonoBehaviour
{
    public GameObject panelPausa; // Arrastra aquí el PanelPausa del Canvas
    private bool estaPausado = false;

    // Este método lo conectaremos al Botón de UI
    public void AlternarPausa()
    {
        if (estaPausado)
        {
            Reanudar();
        }
        else
        {
            Pausar();
        }
    }

    public void Pausar()
    {
        estaPausado = true;
        Time.timeScale = 0f; // <--- LO MÁS IMPORTANTE DEL EXAMEN: Congela el tiempo
        if (panelPausa != null) panelPausa.SetActive(true);
    }

    public void Reanudar()
    {
        estaPausado = false;
        Time.timeScale = 1f; // <--- Regresa el tiempo a la normalidad
        if (panelPausa != null) panelPausa.SetActive(false);
    }
}
