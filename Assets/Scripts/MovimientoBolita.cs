using UnityEngine;

public class MovimientoBolita : MonoBehaviour
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

        // Mover a la izquierda con A
        if (Input.GetKey(KeyCode.A))
        {
            movimiento = -1;
        }

        // Mover a la derecha con D
        if (Input.GetKey(KeyCode.D))
        {
            movimiento = 1;
        }

        // Saltar con W
        if (Input.GetKeyDown(KeyCode.W) && enSuelo)
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