using UnityEngine;

public class Movimientoycamara : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidad de movimiento del personaje
    public float jumpForce = 5f;  // Fuerza del salto
    public float mouseSensitivity = 2f;  // Sensibilidad del movimiento del ratón
    public Transform groundCheck;  // Objeto de referencia para verificar si está en el suelo
    public LayerMask groundMask;  // Máscara de capa para el suelo

    private float rotationX = 0f;  // Rotación en el eje X
    private float rotationY = 0f;  // Rotación en el eje Y
    private bool isGrounded;  // Variable para verificar si el personaje está en el suelo
    private Rigidbody rb;  // Componente Rigidbodysssssssssssssssssssaa

    void Start()
    {
        // Bloquear el cursor en el centro de la pantalla y ocultarlo
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Obtener el componente Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movimiento del personaje
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);

        // Movimiento de la cámara
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);  // Limitar la rotación en el eje Y

        transform.eulerAngles = new Vector3(rotationY, rotationX, 0.0f);

        // Verificar si está en el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);

        // Salto del personaje
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Dibujar el gizmo del groundCheck para depuración
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
        }
    }
}
