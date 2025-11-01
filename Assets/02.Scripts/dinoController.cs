using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoController : MonoBehaviour
{
    public float jumpForce;  // 점프하는 힘
    public bool isGround;    // 땅에 닿은 상태인지 아닌지
    public bool isDown;      // Down상태인지 아닌지

    // offset과 size 값을 저장할 변수
    private Vector2 savedOffset;
    private Vector2 savedSize;
    // BoxCollider2D를 참조할 변수
    private BoxCollider2D boxCollider;


    private Animator anim;
    private Rigidbody2D rb; //물리를 담당하고 있는 Rigidbody2D 타입의 데이터 변수 rb 설정.

    public Transform groundCheckPoint;  // 빨간 점의 위치
    public LayerMask whatIsGround;       // Ground인지 비교할 LayerMask

    

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();// BoxCollider2D 컴포넌트를 가져옴
        SaveColliderSettings();  // 시작할때 현재 서있을때 BoxCollider값 저장.

        // 현재 DinoController스크립트가 적용된 오브젝트에서(Dino) Rigidbody2D 컴포넌트를 얻어 rb 변수에 할당 해줌.
        rb = GetComponent<Rigidbody2D>();   

        anim = GetComponent<Animator>();

        // 처음에 Run 애니메이션 세팅(Animator에서 Bool 타입의 파라미터 isGround를 설정 했기 때문에 anim.SetBool 함수 이용
        anim.SetBool("isGround", true); 
    }

    void Update()
    {
        isGround = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);

        if (Input.GetKeyDown(KeyCode.Space) && isGround.Equals(true) && isDown.Equals(false))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Dino의 가속도를 y방향으로 jumpForce만큼 준다.
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && isGround.Equals(true))  //땅에 닿은 상태에서 아래 화살표 키를 누르면.
        {
            SetDownArrowDown();
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) && isGround.Equals(true)) // 아래 화살표 키를 떼면
        {
            SetDownArrowUp();
        }

        anim.SetBool("isGround", isGround); // isGround의 값에 따라 자동으로 애니메이션 실행
    }

    void SaveColliderSettings()
    {
        // 현재 offset과 size 값을 저장
        savedOffset = boxCollider.offset;
        savedSize = boxCollider.size;
    }

    void LoadColliderSettings()
    {
        // 저장된 offset과 size 값을 BoxCollider2D에 다시 적용
        boxCollider.offset = savedOffset;
        boxCollider.size = savedSize;
    }

    void SetDownArrowDown()
    {
        isDown = true;
        anim.SetBool("isDown", isDown); // Dino에니메이터에서 조건 isDown을 true로
        boxCollider.offset = new Vector2(0, -0.25f);
        boxCollider.size = new Vector2(1.39f, 0.76f);
    }

    void SetDownArrowUp()
    {
        isDown = false;
        anim.SetBool("isDown", isDown);  // Dino에니메이터에서 조건 isDown을 false로
        LoadColliderSettings();
    }


    void OnDrawGizmos() // 범위 그리기
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("게임 오버");
        }
        else if (collision.CompareTag("Point"))
        {
            GameManager.instance.Score_UI_Update();
        }
    }

}

