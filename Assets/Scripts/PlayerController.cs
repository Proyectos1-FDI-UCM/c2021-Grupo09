using UnityEngine;

public class PlayerController : MonoBehaviour
{

    enum ModoJug { Disparo, Construccion }
    public float playerScale;
    public int distConstruc;
    public GameObject[] torres = new GameObject[4]; // Las 4 torres
    public int[] costes = new int[4]; // Costes de las torres
    int indice = 0; // Indica a qué torre del vector se está apuntando
    GameObject torrePuntero; // Semi-transparente, indica dónde se va a construir
    Vector3 pos;
    bool puedeConstruir;
    public GameObject areaConstruccion;
    public Texture2D cursorReticula;
    public Texture2D cursorReticulaPulsada;

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

    private GameManager instance;

    void Start()
    {
        //Acceso al componente rigidBody2D
        rb = GetComponent<Rigidbody2D>();
        playerInfo.modo = ModoJug.Disparo;
        //Acceso al GameManager
        instance = GameManager.GetInstance();
        instance.SetPlayer(gameObject);

        //Creación de la torre que estará en el puntero en el modo construcción
        posEnCursor();
        asignaTorrePuntero();

        //Area Construcción
        areaConstruccion.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0.5f);
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
            transform.localScale = new Vector3(-playerScale, playerScale, 1);
        }
        else if (movimientoX < 0)
        {
            transform.localScale = new Vector3(playerScale, playerScale, 1);
        }

        //Que no se pueda construir ni disparar si el cursor está sobre la interfaz
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > -11) 
        {
            //Disparo
            if (playerInfo.modo == ModoJug.Disparo)
            {
                // Ahora funciona manteniendo pulsado el botón. Si se quiere cambiar, quitar linea 81 y añadir 80
                // if(Input.GetButtonDown("Fire1"))
                if (Input.GetButton("Fire1"))
                {
                    if (Time.timeScale == 1)
                        Cursor.SetCursor(cursorReticulaPulsada, new Vector2(16, 16), CursorMode.Auto);
                    GetComponentInChildren<DispararJugador>().Shoot();
                }
                else
                {
                    if (Time.timeScale == 1)
                        Cursor.SetCursor(cursorReticula, new Vector2(16, 16), CursorMode.Auto);
                }
            }
            //Construcción
            else
            {
                if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    indice = (indice + 1) % 4;
                    Destroy(torrePuntero);
                    asignaTorrePuntero();
                    instance.torresTamañoUI(indice);
                }
                else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    indice = (indice + 3) % 4;
                    Destroy(torrePuntero);
                    asignaTorrePuntero();
                    instance.torresTamañoUI(indice);
                }
                //Actualiza la torrePuntero en la posición del cursor
                posEnCursor();
                torrePuntero.transform.position = pos;

                if (!torrePuntero.GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Camino", "Muro", "Torres"))
                    && Vector3.Distance(pos, this.gameObject.transform.position) < distConstruc
                    && instance.GetCoins() >= costes[indice])
                {
                    puedeConstruir = true;
                    torrePuntero.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0.5f); // Transparente
                }
                else
                {
                    puedeConstruir = false;
                    torrePuntero.GetComponent<SpriteRenderer>().color = new Vector4(1, 0, 0, 0.5f); // Rojo
                }

                if (Input.GetButtonDown("Fire1") && puedeConstruir)
                {
                    AudioManager.GetInstance().PlaySFX("ConstruirTorre"); // Reproducción del sonido de construcción torre
                    Instantiate(torres[indice], pos, new Quaternion(0, 0, 0, 1));
                    instance.SubtractCoins(costes[indice]);
                }
            }
        }
        else if (puedeConstruir) //Que se ponga roja la torre si el cursor está sobre la interfaz
        {
            puedeConstruir = false;
            torrePuntero.GetComponent<SpriteRenderer>().color = new Vector4(1, 0, 0, 0.5f); // Rojo
        }
        else
        {
            Cursor.SetCursor(null, new Vector2(16, 16), CursorMode.Auto);
        }

        //Cambio de modo
        if (Input.GetButtonDown("Fire2"))
        {
            if (playerInfo.modo == ModoJug.Disparo)
            {
                playerInfo.modo++;
                torrePuntero.SetActive(true);
                areaConstruccion.SetActive(true);
                instance.torresTamañoUI(indice);
                Cursor.visible = false;
            }

            else
            {
                playerInfo.modo--;
                torrePuntero.SetActive(false);
                areaConstruccion.SetActive(false);
                instance.torresTamañoUI(-1); // -1 es para todas al mismo tamaño
                Cursor.visible = true;
                Cursor.SetCursor(cursorReticula, new Vector2(0, 0), CursorMode.Auto);
            }

                Debug.Log(playerInfo.modo);
        }

        if(Time.timeScale < 1)
        {
            Cursor.SetCursor(null, new Vector2(16, 16), CursorMode.Auto);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Bala>() != null)
        {
            instance.HurtPlayer(other.gameObject.GetComponent<Bala>().damageDealt);
        }
    }

    void posEnCursor()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = -1;
        pos.x = Mathf.RoundToInt(pos.x);
        pos.y = Mathf.RoundToInt(pos.y);
    }

    void asignaTorrePuntero() {
        torrePuntero = Instantiate(torres[indice], pos, new Quaternion(0, 0, 0, 1));
        torrePuntero.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0.5f); // Transparencia
        torrePuntero.GetComponent<SelfDestruct>().enabled = false;
        if (torrePuntero.GetComponentInChildren<Canvas>())
        {
            torrePuntero.GetComponentInChildren<Canvas>().enabled = false;
        }
        if (torrePuntero.GetComponent<ShooterErizo>())
        {
            torrePuntero.GetComponent<ShooterErizo>().enabled = false;
        }
        else if (torrePuntero.GetComponent<ShooterPulpo>())
        {
            torrePuntero.GetComponent<ShooterPulpo>().enabled = false;
            torrePuntero.GetComponent<Animator>().enabled = false;
        }
        else
        {
            torrePuntero.transform.GetChild(0).gameObject.SetActive(false);
        }
        torrePuntero.layer = default;
        if (playerInfo.modo == ModoJug.Disparo) torrePuntero.SetActive(false);
    }
}