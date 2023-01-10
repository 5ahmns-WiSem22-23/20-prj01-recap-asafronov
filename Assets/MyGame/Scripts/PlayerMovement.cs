using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Controls")]
    public KeyCode w;
    public KeyCode a;
    public KeyCode s;
    public KeyCode d;
    public KeyCode shift;

    [Header("Movement Settings")]
    public AnimationCurve speedCurve;
    public float speedControll;
    public float speedMultiplier;
    public float elapsedTime;

    private float speed;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool shiftKeyDown = false;

    [Header("Health")]
    public int health = 3;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;

    [Header("Animation Settings")]
    public string IDLE;
    public string IDLE_PRESENT;
    public string RUNNING;
    public string RUNNING_PRESENT;

    private Animator animator;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) shiftKeyDown = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) shiftKeyDown = false;


        movement = new Vector2(Input.GetKey(d) ? 1 : Input.GetKey(a) ? -1 : 0, Input.GetKey(w) ? 1 : Input.GetKey(s) ? -1 : 0);

        transform.localEulerAngles = new Vector3(0f, 0f, movement.x == 1 ? 90f : movement.x == -1 ? -90f : movement.y == -1 ? 0f : 180f);

        animator.Play(movement != Vector2.zero ? Collectable.canCollect ? RUNNING : RUNNING_PRESENT : Collectable.canCollect ? IDLE : IDLE_PRESENT);

        if (movement == new Vector2(0f, 0f))
        {
            elapsedTime = 0f;
        }
        else elapsedTime += Time.deltaTime;

        speed = speedCurve.Evaluate(elapsedTime) * speedControll;

        if (health == 2) health1.GetComponent<Animator>().Play("HealthLose");
        if (health == 1) health2.GetComponent<Animator>().Play("HealthLose");

        if (health == 0) StartCoroutine(WaitUntilPause());
    }

    public void FixedUpdate()
    {
        movement = movement.normalized;
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }


    public void RemoveCollectableFromAnimation()
    {
        animator.SetBool("Collected", false);
    }
    
    IEnumerator WaitUntilPause()
    {
        health3.GetComponent<Animator>().Play("HealthLose");
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
    }
}