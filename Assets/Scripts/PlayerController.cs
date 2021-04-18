using UnityEngine;

public class PlayerController : MonoBehaviour
{

    enum ModoJug { Disparo, Construccion }
    public GameObject torre;
    public Material transparent;
    public Material red;
    public Material normal;
    GameObject torrePuntero; // Semi-transparente, indica dónde se va a construir
    Vector3 pos;
    bool puedeConstruir;
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

        //Creación de la torre que estará en el puntero en el modo construcción
        posEnCursor();
        // ortographicSize = (alto de la pantalla en unidades de unity) / 2
        torrePuntero = Instantiate(torre, pos, new Quaternion(0, 0, 0, 1));
        torrePuntero.GetComponent<Renderer>().material = transparent;

        torrePuntero.GetComponent<ShooterPulpo>().enabled = false;
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

        anim.SetFloat("Horizontal", movimientoX);
        anim.SetFloat("Vertical", movimientoY);
        anim.SetFloat("Magnitude", movimiento.magnitude);

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

            if (Vector3.Distance(pos, this.gameObject.transform.position) > 4) puedeConstruir = false;
            else if (Input.GetButtonDown("Fire1") && puedeConstruir) Instantiate(torre, pos, new Quaternion(0, 0, 0, 1));
            
            
            //Cambia el material según si se puede o no construir
            if(!puedeConstruir) torrePuntero.GetComponent<Renderer>().material = red;
            else torrePuntero.GetComponent<Renderer>().material = transparent;
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