using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter: MonoBehaviour
{
    [SerializeField] Generator generatorObj = null;
    [SerializeField] Player player = null;
    [SerializeField] BulletController bulletController = null;
    [SerializeField] Gun gun = null;
    [SerializeField] Transform playerPos = null;

    [SerializeField] AudioClip shootSE = null;
    [SerializeField] AudioClip noBulletSE = null;
    [SerializeField] AudioClip reLoadSE = null;

    AudioSource audioSource = null;

    float shotInterval;     // �e��łC���^�[�o��


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!player.IsStop)
        {
            ShotBullet();
        }
    }

    void ShotBullet()
    {
        shotInterval += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0) && !Cursor.visible && gun.IsNotShot() == false)
        {
            if (shotInterval > 0.2f && bulletController.MainNum > 0)
            {
                bulletController.ShotBullet();
                shotInterval = 0;

                generatorObj.GetComponent<Generator>().PullObject
                   (transform.position, transform.forward, player.IsMove(), playerPos, player.HitPos);

                audioSource.PlayOneShot(shootSE);
            }
            else if(bulletController.MainNum == 0)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(noBulletSE);
                }
            }
        }
    }

    // �����[�h���Ă��邩����
    public bool Reload()
    {
        if (bulletController.MainNum < 30 && bulletController.SubNum > 0 &&
           Input.GetKeyDown(KeyCode.R) && !Cursor.visible)
        {
            audioSource.PlayOneShot(reLoadSE);
            bulletController.ReloadBullet();
            return true;
        }
        return false;
    }
}
