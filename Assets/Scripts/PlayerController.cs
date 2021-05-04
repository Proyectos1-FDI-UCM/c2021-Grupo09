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
    private int vidaTotal = 100;
    private int vidaRestante;
    private UIManager theUIManager;

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
        vidaRestante = vidaTotal;
        //Acceso al GameManager
        instance = GameManager.GetInstance();

        //Creación de la torre que estará en el puntero en el modo construcción
        posEnCursor();
        asignaTorrePuntero();
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

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            indice = (indice + 1) % 4;
            Destroy(torrePuntero);
            asignaTorrePuntero();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            indice = (indice + 3) % 4;
            Destroy(torrePuntero);
            asignaTorrePuntero();
        }


        //Que no se pueda construir ni disparar si el cursor está sobre la interfaz
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > -11) 
        {
            //Disparo
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

                if (!torrePuntero.GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Camino", "Muro", "Torres"))
                    && Vector3.Distance(pos, this.gameObject.transform.position) < distConstruc)
                {
                    puedeConstruir = true;
                    torrePuntero.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0.5f); // Transparente
                }
                else
                {
                    puedeConstruir = false;
                    torrePuntero.GetComponent<SpriteRenderer>().color = new Vector4(1, 0, 0, 0.5f); // Rojo
                }

                if (Input.GetButtonDown("Fire1") && puedeConstruir && GameManager.GetInstance().GetCoins() >= costes[indice])
                {
                    Instantiate(torres[indice], pos, new Quaternion(0, 0, 0, 1));
                    GameManager.GetInstance().SubtractCoins(costes[indice]);
                }
            }
        }
        else if (puedeConstruir) //Que se ponga roja la torre si el cursor está sobre la interfaz
        {
            puedeConstruir = false;
            torrePuntero.GetComponent<SpriteRenderer>().color = new Vector4(1, 0, 0, 0.5f); // Rojo
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

    public void SetUIManager(UIManager uim)
    {
        theUIManager = uim;
    }

    void asignaTorrePuntero() {
        torrePuntero = Instantiate(torres[indice], pos, new Quaternion(0, 0, 0, 1));
        torrePuntero.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0.5f); // Transparencia
        if (torrePuntero.GetComponent<ShooterErizo>() != null) torrePuntero.GetComponent<ShooterErizo>().enabled = false;
        else if (torrePuntero.GetComponent<ShooterPulpo>() != null) torrePuntero.GetComponent<ShooterPulpo>().enabled = false;
        else if (torrePuntero.GetComponent<ShooterTortuga>() != null) torrePuntero.GetComponent<ShooterTortuga>().enabled = false;
        //else if (torrePuntero.GetComponent<ShooterTortuga>() != null) torrePuntero.GetComponent<ShooterBallena>().enabled = false;
        torrePuntero.layer = default;
        if (playerInfo.modo == ModoJug.Disparo) torrePuntero.SetActive(false);
    }
}