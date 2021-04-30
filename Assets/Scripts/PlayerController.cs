using UnityEngine;

public class PlayerController : MonoBehaviour
{

    enum ModoJug { Disparo, Construccion }
    public GameObject Erizo;
    public GameObject Pulpo;
    public GameObject Tortuga;
    GameObject torre;
    GameObject torrePuntero; // Semi-transparente, indica dónde se va a construir
    Vector3 pos;
    bool puedeConstruir;
    // Costes torres
    int costePulpo = 60;
    struct PlayerInfo
    {
        public ModoJug modo;
    }

     private PlayerInfo playerInfo;

    //Velocidad configurable desde el editor
    public float velocityScale;
    //Variable de tipo rigidBody2D
    private Rigidbody2D rb;
    //Variable de tipo Vector2 para el movimiento
    private Vector2 movimiento;

    private Animator anim;

    void Start()
    {
        //Acceso al componente rigidBody2D
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        playerInfo.modo = ModoJug.Disparo;

        //Iniciamos la torre por defecto del modo construir
        torre = Pulpo;

        //Creación de la torre que estará en el puntero en el modo construcción
        posEnCursor();
        torrePuntero = Instantiate(torre, pos, new Quaternion(0, 0, 0, 1));
        torrePuntero.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0.5f); // Transparencia
        if (torrePuntero.GetComponent<ShooterErizo>() != null) torrePuntero.GetComponent<ShooterErizo>().enabled = false;
        else if (torrePuntero.GetComponent<ShooterPulpo>() != null) torrePuntero.GetComponent<ShooterPulpo>().enabled = false;
        else if (torrePuntero.GetComponent<ShooterTortuga>() != null) torrePuntero.GetComponent<ShooterTortuga>().enabled = false;
        torrePuntero.layer = 0;
        torrePuntero.SetActive(false);
    }

    private void FixedUpdate()
    {
        //Movimineto fisico segun la velocidad ajustable
        rb.velocity = (movimiento * velocityScale);
    }
    void Update()
    {
        //Moviviento
        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoY = Input.GetAxis("Vertical");
        movimiento = new Vector2(movimientoX, movimientoY);
        movimiento.Normalize();

        if (movimientoX > 0)
        {
            transform.localScale = new Vector3(-1.8f, 1.8f, 1);
        }
        else if (movimientoX < 0)
        {
            transform.localScale = new Vector3(1.8f, 1.8f, 1);
        }

        //anim.SetFloat("Horizontal", movimientoX);
        //anim.SetFloat("Vertical", movimientoY);
        //anim.SetFloat("Magnitude", movimiento.magnitude);

        //Disparo
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > -11)
        {
            if (playerInfo.modo == ModoJug.Disparo)
            {
                if (Input.GetButtonDown("Fire1")) GetComponentInChildren<DispararJugador>().Shoot();
            }
            //Construcción
            else
            {
                //Actualiza la torrePuntero en la posición del cursor
                posEnCursor();
                torrePuntero.transform.position = pos;

                if (!torrePuntero.GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Camino", "Torres"))
                    && Vector3.Distance(pos, this.gameObject.transform.position) < 6) puedeConstruir = true;
                else puedeConstruir = false;

                if (Input.GetButtonDown("Fire1") && puedeConstruir && GameManager.GetInstance().GetCoins() >= costePulpo)
                {
                    Instantiate(torre, pos, new Quaternion(0, 0, 0, 1));
                    GameManager.GetInstance().SubtractCoins(costePulpo);
                }


                //Cambia el color según si se puede o no construir
                if (!puedeConstruir) torrePuntero.GetComponent<SpriteRenderer>().color = new Vector4(1, 0, 0, 0.5f); // Rojo
                else torrePuntero.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0.5f); // Transparente
            }
        }

        //Cambio de modo
        if (Input.GetButtonDown("Fire2"))
        {
            if (playerInfo.modo == ModoJug.Disparo)
            {
                playerInfo.modo++;
                torrePuntero.SetActive(true);
            }

            else
            {
                playerInfo.modo--;
                torrePuntero.SetActive(false);
            }

                Debug.Log(playerInfo.modo);
        }


    }
    void posEnCursor()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = -1;
        pos.x = Mathf.RoundToInt(pos.x);
        pos.y = Mathf.RoundToInt(pos.y);
    }
    public void CambiaPuedeConstruir(bool pc)
    {
        puedeConstruir = pc;
    }
}