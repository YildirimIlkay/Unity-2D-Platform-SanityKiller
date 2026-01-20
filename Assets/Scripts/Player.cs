using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField] float m_speed = 6.0f;
    [SerializeField] float m_jumpForce = 12f;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Player m_groundSensor;
    private bool m_grounded = false;
    private bool m_combatIdle = false;
    private bool m_isDead = false;

    // Use this for initialization
    void Start()
    {

        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Player>();
        transform.localScale = new Vector3(-1f, 1f, 1f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick") && !m_isDead)
        {
            // Brick yukarıdan aşağı düşüyorsa
            if (collision.relativeVelocity.y < -1f)
            {
                Die();
            }
        }
        else if (collision.gameObject.CompareTag("Pillar") && !m_isDead)
{
            {
                if (collision.relativeVelocity.y < -1f)
                {
                    Die();
                }
            }
        }
    }
        // Update is called once per frame
        void Update()
        {
            if (GameManager.instance != null && GameManager.instance.inputLocked)
                return;
            //Check if character just landed on the ground
            if (!m_grounded && m_groundSensor.State())
            {
                m_grounded = true;
                m_animator.SetBool("Grounded", m_grounded);
            }

            //Check if character just started falling
            if (m_grounded && !m_groundSensor.State())
            {
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
            }

            // -- Handle input and movement --
            float inputX = Input.GetAxis("Horizontal");

            // Swap direction of sprite depending on walk direction
            if (inputX > 0)
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else if (inputX < 0)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            // Move
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

            //Set AirSpeed in animator
            m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

            // -- Handle Animations --
            //Death
            if (Input.GetKeyDown("e"))
            {
                if (!m_isDead)
                    m_animator.SetTrigger("Death");
                else
                    m_animator.SetTrigger("Recover");

                m_isDead = !m_isDead;
            }


            //Hurt
            else if (Input.GetKeyDown("q"))
                m_animator.SetTrigger("Hurt");

            //Attack
            else if (Input.GetMouseButtonDown(0))
            {
                m_animator.SetTrigger("Attack");
            }

            //Change between idle and combat idle
            else if (Input.GetKeyDown("f"))
                m_combatIdle = !m_combatIdle;

            //Jump
            else if (Input.GetKeyDown("space") && m_grounded)
            {
                m_animator.SetTrigger("Jump");
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                m_groundSensor.Disable(0.2f);
            }

            //Run
            else if (Mathf.Abs(inputX) > Mathf.Epsilon)
                m_animator.SetInteger("AnimState", 2);

            //Combat Idle
            else if (m_combatIdle)
                m_animator.SetInteger("AnimState", 1);

            //Idle
            else
                m_animator.SetInteger("AnimState", 0);
        }

    public IEnumerator ResetPlayerState()
    {
        yield return null; // 1 frame bekle (trigger / collider temizlensin)

        // Hareketi tamamen durdur
        m_body2d.velocity = Vector2.zero;

        // Animator state'lerini resetle
        m_animator.Rebind();
        m_animator.Update(0f);

        // Yerde kabul et
        m_grounded = true;
        m_animator.SetBool("Grounded", true);

        // Ölüm vb. durumlar kapansın
        m_isDead = false;
    }

    void Die()
    {
        if (m_isDead) return; // Çift tetiklenmesin

        m_isDead = true;

        m_animator.SetTrigger("Death"); //  Ölüm animasyonu

        // Hareketi tamamen kes
        m_body2d.velocity = Vector2.zero;
        m_body2d.simulated = false;

        // Sahneyi biraz bekleyip resetle
        StartCoroutine(DieRoutine());
    }
    IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(0.6f); // ⬅️ animasyona göre ayarla

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

