using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    //플레이어에 대한 입력과 그에 필요한 변수들을 설정하는 책임을 갖는다.
    public float walkSpeed = 4f;
    public float jumpForce = 5f;
    public AudioClip jumpSound;
    private Rigidbody2D thisrgd = null;
    private Vector3 pos = Vector3.zero;
    private bool isGrounded = false;
    private bool isRun = false;
    private float isDoubleJumped = 0f;
    private PlayerManager player;
    // Use this for initialization
    void Start () {
        player = PlayerManager.Player;
        thisrgd = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update() {
        
        if (PlayerManager.Player.U_A_S != Unit.UnitActionState.DEATH)
        {
            if (isGrounded)
            {
                if (Input.GetKeyDown("space") && isDoubleJumped > 0)
                {
                    thisrgd.velocity = new Vector2(0.0f, jumpForce);
                    //rigidbody의 y방향에 대한 속도를 직접 바꿔쓴다.
                    isDoubleJumped--;
                    
                    //점프 사운드 출력
                    SuperManager superManager;
                    GameManager.Instance.managers.TryGetValue("SoundManager", out superManager);//GameManager에서 사운드매니저에 대한 정보를 얻는다.
                    SoundManager soundManager = superManager.gameObject.GetComponent<SoundManager>();
                    soundManager.soundEffectSource.PlayOneShot(jumpSound);
                }
            }//jump
            if (Input.GetKey("w") && isRun == false)
            {
                walkSpeed *= 2;
                isRun = true;
            }//runTrue
            if (Input.GetKeyUp("w"))
            {
                walkSpeed /= 2;
                isRun = false;

            }//runfalse
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                player.ReturnInventoryscript().ChangeSelectItemScrollStar(true);//별 이동
                player.ReturnInventoryscript().ChangeItem();//아이템 선택
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                player.ReturnInventoryscript().ChangeSelectItemScrollStar(false);//별 이동
                player.ReturnInventoryscript().ChangeItem();//아이템 선택
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                player.ReturnInventoryscript().ChangeSelectItemKeypadStar(1);//별 이동
                player.ReturnInventoryscript().ChangeItem();//아이템 선택
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                player.ReturnInventoryscript().ChangeSelectItemKeypadStar(2);//별 이동
                player.ReturnInventoryscript().ChangeItem();//아이템 선택
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                player.ReturnInventoryscript().ChangeSelectItemKeypadStar(3);//별 이동
                player.ReturnInventoryscript().ChangeItem();//아이템 선택
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                player.ReturnInventoryscript().ChangeSelectItemKeypadStar(4);//별 이동
                player.ReturnInventoryscript().ChangeItem();//아이템 선택
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                player.ReturnInventoryscript().ChangeSelectItemKeypadStar(5);//별 이동
                player.ReturnInventoryscript().ChangeItem();//아이템 선택
            }
            if (Input.GetButtonDown("Fire1"))
            {
                if (player.ReturnInventoryscript().ReturnSelectItem() != null)//현재 셀렉하고있는 아이템의정보가 null이 아닐경우
                    player.ReturnInventoryscript().UseItem();
                //현재 셀렉중인 아이템을 사용한다.
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (player.ReturnInventoryscript().ReturnSelectItem() != null)
                {//현재 셀렉하고있는 아이템의정보가 null이 아닐경우
                    player.ReturnInventoryscript().RemoveInventory(player.ReturnInventoryscript().ReturnSelectItem());

                }
            }
        }//플레이어가 죽지 않았을때 발동하는 함수
        {
            if (Input.GetKey("w") && PlayerManager.Player.U_A_S == Unit.UnitActionState.DEATH)
            {
                pos.y += walkSpeed * Time.deltaTime;
            }
            if (Input.GetKey("s") && PlayerManager.Player.U_A_S == Unit.UnitActionState.DEATH)
            {
                pos.y -= walkSpeed * Time.deltaTime;
            }
            if (Input.GetKey("a"))
            {
                pos.x -= walkSpeed * Time.deltaTime;
                player.P_S_S = PlayerManager.PlayerSeeState.SEELEFT;
                gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);
                if (PlayerManager.Player.ChangeState(PlayerManager.UnitActionState.IDLE))
                {
                    player.animator.SetTrigger("PlayerMove");
                }
            }//left
            if (Input.GetKey("d"))
            {
                pos.x += walkSpeed * Time.deltaTime;
                player.P_S_S = PlayerManager.PlayerSeeState.SEERIGHT;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                if (PlayerManager.Player.ChangeState(PlayerManager.UnitActionState.IDLE))
                {
                    player.animator.SetTrigger("PlayerMove");
                }
            }//right
        }//플레이어가 고스트 상태(죽었을때)에도 발동하는 함수
        transform.position += pos;
        pos = Vector3.zero;
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            isGrounded = true;
            isDoubleJumped = 1;
        }
    }
}
