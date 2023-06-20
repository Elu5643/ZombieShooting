using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Slider hp = null;
    [SerializeField] Image image = null;

    [SerializeField] AudioClip walkSE = null;
    [SerializeField] AudioClip addBulletSE = null;

    [SerializeField] Camera mainCamera = null;
    [SerializeField] ShakeCamera shake = null;
    [SerializeField] CurveControlledBob bob = null;


    Animator anim = null;
    Rigidbody rb = null;
    AudioSource audioSource = null;


    Vector3 hitPos;     // レイの当たった位置を取得

    public Vector3 HitPos
    {
        get { return hitPos; }
    }

    int currentHitPoint;      // 現在のPlayerのHitPoint

    const int maxHitPoint = 100;  // 最大HitPoint

    float inputHorizontal;  // 水平方向の入力
    float inputVertical;    // 垂直方向の入力

    float moveSpeed = 3f;   // 歩くのスピード

    float jumpPower = 3.5f; // ジャンプの高さ

    float rayLength = 200f; // Rayの長さ

    bool isStop = true;     // Playerの動きを制限（攻撃を喰らった時）

    bool canJump = false;    // ジャンプボタンを押したか

    public bool IsStop
    {
        get { return isStop; }
        set { isStop = value; }
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
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(walkSE);
            }
        }
        else
        {
            audioSource.Stop();
        }

        if (isStop == false)
        {
            inputHorizontal = Input.GetAxisRaw("Horizontal");
            inputVertical = Input.GetAxisRaw("Vertical");

            Jump();
            RayCasting();
            ControlCursor();

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
    }

    // ジャンプ
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            canJump = false;
        }
    }

    // エイム（左クリック）しているかどうか
    public bool IsAim()
    {
        if (Input.GetMouseButton(1) && !Cursor.visible)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // 動いているか判定
    public bool IsMove()
    {
        if (rb.velocity.z == 0 || rb.velocity.x == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // Rayの当たった位置を取得
    void RayCasting()
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
        }
    }

    // カーソルを調整
    void ControlCursor()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !Cursor.visible || 
            Input.GetKeyDown(KeyCode.Escape) && !Cursor.visible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && Cursor.visible ||
                 Input.GetKeyDown(KeyCode.Escape) && Cursor.visible)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // アニメーターのイベントで使用
    void DamageEvent()
    {
        isStop = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyAttack")
        {
            currentHitPoint -= 100;
            hp.value = currentHitPoint;

            // HitPointが1以上の場合ダメージ・1以下の場合死亡
            if (currentHitPoint >= 1)
            {
                image.color = new Color(0.5f, 0f, 0f, 0.5f);
                isStop = true;
                anim.SetTrigger("Damage");
                shake.BeginShake(0.25f, 0.1f);
            }
            else
            {
                image.color = new Color(0.5f, 0f, 0f, 0.5f);
                isStop = true;
                anim.SetTrigger("Destroy");
                shake.BeginShake(0.25f, 0.1f);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                GameObject.Find("GameController")?.GetComponent<GameController>()?.FailureGame();
            }
        }
        else if(other.gameObject.tag == "BulletObj")
        {
            audioSource.PlayOneShot(addBulletSE);
        }
        else if(other.gameObject.tag == "Clear")
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
            canJump = true;
        }
    }
}
