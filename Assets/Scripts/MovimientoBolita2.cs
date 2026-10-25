using UnityEngine;

public class MovimientoBolita2 : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 7f;

    private Rigidbody2D rb;
    private float movimiento;
    private bool enSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movimiento = 0;

        // Mover a la izquierda
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movimiento = -1;
        }

        // Mover a la derecha
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movimiento = 1;
        }

        // Saltar con flecha arriba
        if (Input.GetKeyDown(KeyCode.UpArrow) && enSuelo)
        {
            rb.AddForce(Vector2.up * fuerzaSalto,
                        ForceMode2D.Impulse);

            enSuelo = false;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            movimiento * velocidad,
            rb.linearVelocity.y
        );
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        RevisarSuelo(collision);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        RevisarSuelo(collision);
    }

    void RevisarSuelo(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            foreach (ContactPoint2D contacto in collision.contacts)
            {
                if (contacto.normal.y > 0.5f)
                {
                    enSuelo = true;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = false;
        }
    }
}