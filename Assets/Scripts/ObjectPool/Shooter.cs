using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter: MonoBehaviour
{
    [SerializeField] Generator generatorObj = null;
    [SerializeField] Player player = null;
    [SerializeField] BulletController bulletController = null;
    [SerializeField] Transform playerPos = null;
    [SerializeField] Gun gun = null;

    [SerializeField] AudioClip shootSe = null;
    [SerializeField] AudioClip noBulletSe = null;
    [SerializeField] AudioClip reLoadSe = null;

    AudioSource audioSource;

    float shotInterval;     //弾を打つインターバル


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
        if (Input.GetKey(KeyCode.Mouse0) && !Cursor.visible && gun.IsReload())
        {
            if (shotInterval > 0.2f && bulletController.MainNum > 0)
            {
                bulletController.ShotBullet();
                shotInterval = 0;

                generatorObj.GetComponent<Generator>().PullObject
                   (transform.position, transform.forward, player.IsAim(), player.IsMove(), playerPos, player.HitPos);

                audioSource.PlayOneShot(shootSe);
            }
            else if(bulletController.MainNum == 0)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(noBulletSe);
                }
            }
        }

        Reload();
    }

    void Reload()
    {
        if (bulletController.MainNum < 30 && bulletController.SubNum > 0 &&
            Input.GetKeyDown(KeyCode.R) && !Cursor.visible)
        {
            audioSource.PlayOneShot(reLoadSe);
        }
    }
}
