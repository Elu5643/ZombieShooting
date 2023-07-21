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


    Vector3 hitPos;     // ���C�̓��������ʒu���擾

    public Vector3 HitPos
    {
        get { return hitPos; }
    }

    int currentHitPoint;      // ���݂�Player��HitPoint

    const int maxHitPoint = 100;  // �ő�HitPoint

    float inputHorizontal;  // ���������̓���
    float inputVertical;    // ���������̓���

    bool isStop = true;     // Player�̓����𐧌��i�U�������������j

    public bool IsStop
    {
        get { return isStop; }
        set { isStop = value; }
    }

    bool canJump = false;    // �W�����v�{�^������������

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
        float moveSpeed = 3f;   // �����̃X�s�[�h
        float runSpeed = 1.5f;  // ���������̃X�s�[�h
        float aimSpeed = 1.5f;  // �G�C���������̃X�s�[�h
        float stmDecreaseSpeed = 25.0f; // �X�^�~�i�����鑬�x
        float stmRecoverSpeed = 10.0f;  // �X�^�~�i�������鑬�x

        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

        // �ړ������ɃX�s�[�h���|����B
        // �Ə��������͑��x�͉�����
        if (IsAim())
        {
            rb.velocity = moveForward * (moveSpeed - aimSpeed) + new Vector3(0, rb.velocity.y, 0);
        }
        // ��������X�^�~�i�����炷
        else if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && stm.value > 0)
        {
            rb.velocity = moveForward * (moveSpeed + runSpeed) + new Vector3(0, rb.velocity.y, 0);
            stm.value -= Time.deltaTime * stmDecreaseSpeed;
            audioSource.pitch = 1.5f;
            box.enabled = true;
        }
        // �����X�s�[�h
        else
        {
            rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);
            audioSource.pitch = 1.0f;
        }

        // �����Ă��Ȃ��ꍇ�X�^�~�i���񕜂���
        if(Input.GetKey(KeyCode.LeftShift) == false)
        {
            stm.value += Time.deltaTime * stmRecoverSpeed;
            box.enabled = false;
        }
    }

    // �W�����v
    void Jump()
    {
        float jumpPower = 3.5f; // �W�����v�̍���

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.velocity = transform.up * jumpPower;
            // rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            canJump = false;
        }
    }

    // �G�C���i���N���b�N�j���Ă��邩�ǂ���
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

    // �����Ă��邩����
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

    // Ray�̓��������ʒu���擾
    void RayCasting()
    {
        float rayLength = 200f; // Ray�̒���

        if (Cursor.visible == false)
        {
            // �����������Ă������
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layerMask = 1 << 8;
            // ���������I�u�W�F�N�g��layerMask��1�`8��
            if (Physics.Raycast(ray, out hit, rayLength, layerMask))
            {
                // ���W��n��
                hitPos = hit.point;
            }
        }
    }

    // �J�[�\���𒲐�
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

    // �A�j���[�^�[�̃C�x���g�Ŏg�p
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

            // HitPoint��1�ȏ�̏ꍇ�_���[�W�E1�ȉ��̏ꍇ���S
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
