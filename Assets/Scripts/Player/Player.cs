using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Slider hp;
    [SerializeField] Image image;

    [SerializeField] AudioClip walkSe = null;
    [SerializeField] AudioClip addBulletSe = null;

    [SerializeField] Camera mainCamera;
    [SerializeField] FPSCamera fpsCamera;
    [SerializeField] ShakeCamera shake;
    [SerializeField] CurveControlledBob bob;


    Animator anim;
    Rigidbody rb;
    AudioSource audioSource;


    Vector3 hitPos;     //レイの当たった位置を取得

    public Vector3 HitPos
    {
        get { return hitPos; }
    }

    int currentHitPoint;      //現在のPlayerのHitPoint
    public int CurrentHitPoint
    {
        get { return currentHitPoint; }
        set { currentHitPoint = value; }
    }

    const int maxHitPoint = 100;  //最大HitPoint

    float inputHorizontal;  //水平方向の入力
    float inputVertical;    //垂直方向の入力

    float moveSpeed = 3f;   //歩くのスピード

    float jumpPower = 3.5f; //ジャンプの高さ

    float rayLength = 200f; //Rayの長さ

    bool isStop = true;     //Playerの動きを制限（攻撃を喰らった時）

    bool isJump = false;    //ジャンプボタンを押したか

    public bool IsStop
    {
        get { return isStop; }
        set { isStop = value; }
    }

    enum State
    {
        Walk,   //歩く
        Aim,    //照準
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        currentHitPoint = maxHitPoint;
        image.color = Color.clear;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        bob.Setup(mainCamera, 1f);
    }

    void Update()
    {
        if (IsMove())
        {
            audioSource.Stop();
        }
        else
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(walkSe);
            }
        }

        if (isStop == false)
        {
            inputHorizontal = Input.GetAxisRaw("Horizontal");
            inputVertical = Input.GetAxisRaw("Vertical");

            IsAim();

            Jump();
            RayShooter();
            CursorController();

            Vector3 vecBob = bob.DoHeadBob(0.8f, IsMove());
            mainCamera.transform.localPosition = vecBob;
        }
        if (currentHitPoint >= 1) image.color = Color.Lerp(image.color, Color.clear, Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (isStop == false)
        {
            Walk();
        }
    }

    void Walk()
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        // 標準した時は速度は下げる
        if (IsAim())
        {
            rb.velocity = moveForward * (moveSpeed - 1.5f) + new Vector3(0, rb.velocity.y, 0);
        }
        else
        {
            rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);
        }

        //// キャラクターの向きを進行方向に
        //if (moveForward != Vector3.zero)
        //{
        //    transform.rotation = Quaternion.LookRotation(moveForward);
        //}
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJump)
        {
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            isJump = false;
        }
    }

    public bool IsAim()
    {
        if (Input.GetMouseButton(1) && !Cursor.visible)
        {
            anim.SetInteger("State", (int)State.Aim);
            return true;
        }
        else
        {
            anim.SetInteger("State", (int)State.Walk);
            return false;
        }
    }

    public bool IsMove()
    {
        if (rb.velocity.z == 0 || rb.velocity.x == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void RayShooter()
    {
        if (!Cursor.visible)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layerMask = 1 << 8;
            if (Physics.Raycast(ray, out hit, rayLength, layerMask))
            {
                hitPos = hit.point;
            }
            //Debug.Log(hit.point);
            //string name = hit.collider.gameObject.name; // 衝突した相手オブジェクトの名前を取得
            //Debug.Log(name);
            Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red, 5);
        }
    }

    void CursorController()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !Cursor.visible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && Cursor.visible)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void DamageEvent()
    {
        isStop = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyAttack" && currentHitPoint >= 1)
        {
            currentHitPoint -= 10;
            hp.value = currentHitPoint;

            if (currentHitPoint >= 1)
            {
                image.color = new Color(0.5f, 0f, 0f, 0.5f);
                isStop = true;
                anim.SetTrigger("Damage");
                shake.Shake(0.25f, 0.1f);
            }
            else
            {
                image.color = new Color(0.5f, 0f, 0f, 0.5f);
                isStop = true;
                anim.SetTrigger("Destroy");
                shake.Shake(0.25f, 0.1f);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                GameObject.Find("GameController")?.GetComponent<GameController>()?.FailedGame();
            }
        }

        if(other.gameObject.tag == "BulletObj")
        {
            audioSource.PlayOneShot(addBulletSe);
        }

        if(other.gameObject.tag == "Clear")
        {
            isStop = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameObject.Find("GameController")?.GetComponent<GameController>()?.ClearGame();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isJump = true;
        }
    }
}
