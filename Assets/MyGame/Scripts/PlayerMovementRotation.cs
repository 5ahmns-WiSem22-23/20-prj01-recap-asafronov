using UnityEngine;
using System.Collections;

public class PlayerMovementRotation : MonoBehaviour
{
    [Header("Controls")]
    public KeyCode w;
    public KeyCode a;
    public KeyCode s;
    public KeyCode d;

    [Header("Movement Settings")]
    public float rotateSpeed = 1f;
    public AnimationCurve speedCurve;
    public float speedControll;
    public float elapsedTime;

    private float speed;
    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("Health")]
    public int health = 3;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;

    [Header("UI")]
    public GameObject[] gameOverScreen;

    [Header("Animation Settings")]
    public string idle;
    public string idlePresent;
    public string running;
    public string runningPresent;
    public string death;

    private Animator animator;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        Debug.Log("health " + health);

        movement = new Vector2(0f, Input.GetKey(w) ? 1 : Input.GetKey(s) ? -1 : 0);

        animator.Play(movement != Vector2.zero ? Collectable.canCollect ? running : runningPresent : Collectable.canCollect ? idle : idlePresent);

        if (Input.GetKey(d)) transform.Rotate(Vector3.back, rotateSpeed * Time.deltaTime);
        if (Input.GetKey(a)) transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

        if (movement == new Vector2(0f, 0f))
        {
            elapsedTime = 0f;
        }
        else elapsedTime += Time.deltaTime;

        speed = speedCurve.Evaluate(elapsedTime) * speedControll;

        if (health == 2) health1.GetComponent<Animator>().Play("HealthLose");
        if (health == 1) health2.GetComponent<Animator>().Play("HealthLose");

        if (health <= 0)
        {
            animator.Play(death);
            rb.bodyType = RigidbodyType2D.Static;

            //var screen = Random.Range(0, 10);
            //if (screen >= 2) gameOverScreen[1].SetActive(true);
            //else gameOverScreen[0].SetActive(true);
            gameOverScreen[0].SetActive(true);

            StartCoroutine(WaitUntilPause());
        }
    }

    public void FixedUpdate()
    {
        Vector2 localMovement = transform.TransformDirection(-movement);
        rb.MovePosition(rb.position + speed * Time.deltaTime * localMovement);
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