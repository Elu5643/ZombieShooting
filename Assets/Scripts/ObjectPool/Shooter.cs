using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter: MonoBehaviour
{
    [SerializeField] Generator generatorObj = null;
    [SerializeField] Player player = null;
    [SerializeField] MagazineController magazine = null;
    [SerializeField] GunAnimation gun = null;
    [SerializeField] Transform playerPos = null;

    [SerializeField] AudioClip shootSE = null;
    [SerializeField] AudioClip noBulletSE = null;
    [SerializeField] AudioClip reLoadSE = null;

    AudioSource audioSource = null;

    float shotInterval;     // 弾を打つインターバル


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
            if (shotInterval > 0.2f && magazine.MainNum > 0)
            {
                magazine.Shot();
                shotInterval = 0;

                generatorObj.GetComponent<Generator>().PullObject
                   (transform.position, transform.forward, player.IsMove(), playerPos, player.HitPos);

                audioSource.PlayOneShot(shootSE);
            }
            else if(magazine.MainNum == 0)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(noBulletSE);
                }
            }
        }
    }

    // リロードしているか判定
    public bool IsReload()
    {
        if (magazine.MainNum < 30 && magazine.SubNum > 0 &&
           Input.GetKeyDown(KeyCode.R) && !Cursor.visible)
        {
            audioSource.PlayOneShot(reLoadSE);
            magazine.Reload();
            return true;
        }
        return false;
    }
}
