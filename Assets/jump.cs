using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    /// <summary>���E�ړ������</summary>
    [SerializeField] float m_movePower = 5f;
    /// <summary>�W�����v�����</summary>
    [SerializeField] float m_jumpPower = 15f;
    /// <summary>���͂ɉ����č��E�𔽓]�����邩�ǂ����̃t���O</summary>
    [SerializeField] bool m_flipX = false;
    Rigidbody2D m_rb = default;
    SpriteRenderer m_sprite = default;
    /// <summary>���������̓��͒l</summary>
    float m_h;
    float m_scaleX;
 
    int _wjump = 0;


    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���͂��󂯎��
        m_h = Input.GetAxisRaw("Horizontal");
        // �e����͂��󂯎��
        if (Input.GetButtonDown("Jump") && _wjump < 1)
        {

            m_rb.AddForce(Vector2.up * m_jumpPower, ForceMode2D.Impulse);

            _wjump += 1;
        }
        // �ݒ�ɉ����č��E�𔽓]������
        if (m_flipX)
        {
            FlipX(m_h);
        }

        Vector2 velocity = m_rb.velocity;
        velocity.x = m_h * m_movePower;
        m_rb.velocity = velocity; 
    }
    private void FixedUpdate()
    {
        // �͂�������̂� FixedUpdate �ōs��
       // m_rb.AddForce(Vector2.right * m_h * m_movePower, ForceMode2D.Force);
    }

    /// <summary>
    /// ���E�𔽓]������
    /// </summary>
    /// <param name="horizontal">���������̓��͒l</param>
    void FlipX(float horizontal)
    {
        /*
         * ������͂��ꂽ��L�����N�^�[�����Ɍ�����B
         * ���E�𔽓]������ɂ́ATransform:Scale:X �� -1 ���|����B
         * Sprite Renderer �� Flip:X �𑀍삵�Ă����]����B
         * */
        m_scaleX = this.transform.localScale.x;

        if (horizontal > 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else if (horizontal < 0)
        {
            this.transform.localScale = new Vector3(-1 * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //_isJump = true;
        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    _isJump = true;
        //}
        if (collision.gameObject.CompareTag("Ground"))
        {


            _wjump = 0;
        }
    }
}



