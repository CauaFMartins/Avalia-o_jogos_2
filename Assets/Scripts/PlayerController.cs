using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5f;

    [Header("Mouse")]
    public float sensibilidade = 200f;
    public Transform cameraPlayer;

    [Header("Tiro")]
    public GameObject balaPrefab;
    public Transform firePoint;
    public float forcaTiro = 20f;

    private Rigidbody rb;

    private float rotacaoX = 0f;

    private GameManager gm;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        gm = FindFirstObjectByType<GameManager>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Movimento();
        MouseLook();
        Atirar();
    }

    void Movimento()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movimento =
            transform.right * x +
            transform.forward * z;

        transform.position +=
            movimento *
            velocidade *
            Time.deltaTime;
    }

    void MouseLook()
    {
        float mouseX =
            Input.GetAxis("Mouse X") *
            sensibilidade *
            Time.deltaTime;

        float mouseY =
            Input.GetAxis("Mouse Y") *
            sensibilidade *
            Time.deltaTime;

        rotacaoX -= mouseY;

        rotacaoX = Mathf.Clamp(rotacaoX, -90f, 90f);

        cameraPlayer.localRotation =
            Quaternion.Euler(rotacaoX, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    void Atirar()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gm != null && gm.PodeAtirar())
            {
                gm.GastarMunicao();

                GameObject bala = Instantiate(
                    balaPrefab,
                    firePoint.position,
                    firePoint.rotation
                );

                Rigidbody rbBala =
                    bala.GetComponent<Rigidbody>();

                if (rbBala != null)
                {
                    rbBala.velocity =
                        firePoint.forward * forcaTiro;
                }

                Destroy(bala, 5f);
            }
        }
    }
}