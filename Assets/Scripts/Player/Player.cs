using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Slider hp = null;
    [SerializeField] Slider stm = null;
    public Slider Stm
    { 
        get { return stm; }
    }

    [SerializeField] Image image = null;

    [SerializeField] AudioClip walkSE = null;
    [SerializeField] AudioClip addBulletSE = null;

    [SerializeField] Camera mainCamera = null;
    [SerializeField] ShakeCamera shake = null;
    [SerializeField] CurveControlledBob bob = null;


    Animator anim = null;
    Rigidbody rb = null;
    BoxCollider box = null;
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

    bool isStop = true;     // Playerの動きを制限（攻撃を喰らった時）

    public bool IsStop
    {
        get { return isStop; }
        set { isStop = value; }
    }

    bool canJump = false;    // ジャンプボタンを押したか

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        box= GetComponent<BoxCollider>();

        currentHitPoint = maxHitPoint;
        image.color = Color.clear;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        bob.Setup(mainCamera, 1f);

        box.enabled = false;
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

            Move();
            Jump();
            RayCasting();
            ControlCursor();

            Vector3 vecBob = bob.DoHeadBob(0.8f, IsMove());
            mainCamera.transform.localPosition = vecBob;
        }

        if (currentHitPoint >= 1) image.color = Color.Lerp(image.color, Color.clear, Time.deltaTime);
    }

    void Move()
    {
        float moveSpeed = 3f;   // 歩くのスピード
        float runSpeed = 1.5f;  // 走った時のスピード
        float aimSpeed = 1.5f;  // エイムした時のスピード
        float stmDecreaseSpeed = 25.0f; // スタミナが減る速度
        float stmRecoverSpeed = 10.0f;  // スタミナが増える速度

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

        // 移動方向にスピードを掛ける。
        // 照準した時は速度は下げる
        if (IsAim())
        {
            rb.velocity = moveForward * (moveSpeed - aimSpeed) + new Vector3(0, rb.velocity.y, 0);
        }
        // 走ったらスタミナを減らす
        else if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && stm.value > 0)
        {
            rb.velocity = moveForward * (moveSpeed + runSpeed) + new Vector3(0, rb.velocity.y, 0);
            stm.value -= Time.deltaTime * stmDecreaseSpeed;
            audioSource.pitch = 1.5f;
            box.enabled = true;
        }
        // 歩くスピード
        else
        {
            rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);
            audioSource.pitch = 1.0f;
        }

        // 走っていない場合スタミナを回復する
        if(Input.GetKey(KeyCode.LeftShift) == false)
        {
            stm.value += Time.deltaTime * stmRecoverSpeed;
            box.enabled = false;
        }
    }

    // ジャンプ
    void Jump()
    {
        float jumpPower = 3.5f; // ジャンプの高さ

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.velocity = transform.up * jumpPower;
            // rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            canJump = false;
        }
    }

    // エイム（左クリック）しているかどうか
    public bool IsAim()
    {
        if (Input.GetMouseButton(1) && Cursor.visible == false)
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
        float rayLength = 200f; // Rayの長さ

        if (Cursor.visible == false)
        {
            // 今自分が見ている方向
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layerMask = 1 << 8;
            // 当たったオブジェクトがlayerMaskの1～8か
            if (Physics.Raycast(ray, out hit, rayLength, layerMask))
            {
                // 座標を渡す
                hitPos = hit.point;
            }
        }
    }

    // カーソルを調整
    void ControlCursor()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Cursor.visible == false || 
            Input.GetKeyDown(KeyCode.Escape) && Cursor.visible == false)
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
            currentHitPoint -= 20;
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

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }
}
