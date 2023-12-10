using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repaso : MonoBehaviour
{

    /*Movimiento 3a persona - Free Lock Camera

   Personaje - Character controller - Personaje dentro de cápsula

   → Slope limit - inclinación rampa
   → step offset - tamaño escalón


   Free Lock Camera

   → Follow - Player
   → Look At - player
   → Orbit - World space


   //script Character Controller

       private CharacterController _controller;
       private Transform _camera;
       private float _horizontal;
       private float _vertical;

       [SerializeField] private float _playerSpeed = 5;
       [SerializeField] private float _jumpForce = 6;
       [SerializeField]private transform sensorPosition;
       [SerializeField] private float sensorRadius = 0.2f;
       [SerializeField] private float groundLayer;

       private float gravity = -9.81f;
       private Vector3 _playergravity;
       private float turnSmoothVeloity;
       private float turnSmoothTime = 0.1f;
       private bool _isGrounded;

     void Awake()
       {
           _controller = GetComponent<CharacterController>();
           _camera = Camera.main.transform;
       }

       void Update()
       {
           _horizontal = Input.GetAxisRaw("Horizontal");
           _vertical = Input.GetAxisRaw("Vertical");

           Movement();
           Jump();
       }

       void Movement()
       {
           Vector3 direction = new Vector3(_horizontal, 0, _vertical);

           if (direction != Vector3.zero)

           {
               float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
               float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVeloity, turnSmoothTime);   ← (no es necesario +nota)
               transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
               Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
               _controller.Move(moveDirection * playerSpeed * Time.deltaTime);
           }

       }


       Jerarquía

       Añadir empty a jerarquía - dentro del personaje -  sensor

       Añadir Layer - Ground - al suelo



   Script character controller

    void Jump()
       {

           _isGrounded = Physics.CheckSphere(_sensorPosition.position, _sensorRadius, _groundLayer);
           _playerGravity.y += _gravity * Time.deltaTime;

           _controller.Move(_playerGravity * Time.deltaTime);

           if (_isGrounded && _playerGravity.y < 0)
           {
               _playerGravity.y = -2;
           }

           if (_isGrounded && Input.GetButtonDown("Jump"))
           {
               _playerGravity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
           }

       }

  Animaciones

       Animator _animator

       void Awake()
       {

           _animator = GetComponentInChildren<Animator>();
           //Este In Children es porque el idle está dentro del personaje
       }

       Idle - añadir animator

   Animator - crear un blend tree - doble click encima
   Añadir parámetros VelX y VelY

   Dentro del Blend tree


   IDLE                 0        0
   WALK                 0       0.2
   WALK BACK            0      -0.2
   LEFT WALK           -0.2    0
   RIGHT WALK           0.2     0
   RUN                  0        1
   RUN BACK             0       -1
   LEFT RUN            -1        0
   RIGHT RUN            1         0


   Salto → Blend Tree
   Salto ← Blend tree

   _IsJumping  ← Bool

       void Movement()
       {

           _animator.SetFloat("Velx", 0);
           _animator.SetFloat("VelZ", direction.magnitude);
       }

       void Jump()
       {
           _animator.SetBool(“IsJumping”, !_isGrounded);

       }*/

    /* //Raycast

    //Raycast simple
    if(Physics.Raycast(transform.position, transform.forward, 10))
    {
        Debug.Log("Hit");
        Debug.DrawRay(tramsform.position, transform.forward* 10, color.green);
    }
    else
    {
        Debug.DrawRay(tramsform.position, transform.forward * 10, color.green);
    }

    //Raycast bueno

    RaycastHit hit;
    if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
    {
        Debug.Log(hit.transform.name);
        Debug.Log(hit.transform.position);
        Debug.Log(hit.transform.gameObject.layer);
        Debug.Log(hit.transform.gameObject.tag);

        if (hit.transform.gameObject.tag == "caja")
        {
            //Box = script que tiene la caja
            Box caja = hit.transform.GetComponent<Box>();
            if (caja != null)
            {
                caja.TakeDamage(shootDamage)
            }
        }

        else if (hit.transform.gameObject.tag == "Zapato")
        {
            Destroy(hit.transform.gameObject);
        }
    }

    //Raycast de la camara

    public Camara camaraPrincipal;

    Ray ray = camaraPrincipal.ScreenPointToRay(Input.mousePosition)
        RaycastHit hit;

    if (Physics.Raycast(ray out hit.Mathf.infinity) && Input.GetButtonDown("Submit")) //Lo ultimo para activar el rayo con un boton (enter) si no no se pone
    {
        Debug.DrawLine(Camera.main.transform.position, hit.position)
            if (hit.transform.gameObject.layer == 7)
        {
            Debug.Log("impacto")
                //Box = script que tiene la caja
                Box box = hit.transform.gameObject.GetComponent<Box>();
            if (box != null)
            {
                //TakeDamage = funcion del script "Box"
                box.TakeDamage(2);
            }
        }

RayCast
    public class RayoMagico : MonoBehaviour
{
    public Camera camaraPrincipal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Rayo();
        }
    }

    void Rayo()
    {
        Ray ray = camaraPrincipal.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitRayo;

        if(Physics.Raycast(ray, out hitRayo, Mathf.Infinity))
        {
            Debug.DrawLine(Camera.main.transform.position, hitRayo.point);
            
            if(hitRayo.transform.gameObject.layer == 6)
            {
                Debug.Log("impacto bola");
                StartCoroutine(TimerCoroutine1());
                
                
            }

            if(hitRayo.transform.gameObject.layer == 7)
            {
                Debug.Log("impacto cubo grande");
                StartCoroutine(TimerCoroutine2());
            }

            if(hitRayo.transform.gameObject.layer == 8)
            {
                Debug.Log("impacto cubo pequeÃ±o");
                StartCoroutine(TimerCoroutine3());
            }
        } 
    }

    IEnumerator TimerCoroutine1()
    {
        Debug.Log("5");
        yield return new WaitForSeconds (1);
        Debug.Log("4");
        yield return new WaitForSeconds (1);
        Debug.Log("3");
        yield return new WaitForSeconds (1);
        Debug.Log("2");
        yield return new WaitForSeconds (1);
        Debug.Log("1");
        yield return new WaitForSeconds (1);
        SceneManager.LoadScene(1);
    }

    IEnumerator TimerCoroutine2()
    {
        Debug.Log("5");
        yield return new WaitForSeconds (1);
        Debug.Log("4");
        yield return new WaitForSeconds (1);
        Debug.Log("3");
        yield return new WaitForSeconds (1);
        Debug.Log("2");
        yield return new WaitForSeconds (1);
        Debug.Log("1");
        yield return new WaitForSeconds (1);
        SceneManager.LoadScene(2);
    }

    IEnumerator TimerCoroutine3()
    {
        Debug.Log("5");
        yield return new WaitForSeconds (1);
        Debug.Log("4");
        yield return new WaitForSeconds (1);
        Debug.Log("3");
        yield return new WaitForSeconds (1);
        Debug.Log("2");
        yield return new WaitForSeconds (1);
        Debug.Log("1");
        yield return new WaitForSeconds (1);
        SceneManager.LoadScene(3);
    }
}


    }*/


}
