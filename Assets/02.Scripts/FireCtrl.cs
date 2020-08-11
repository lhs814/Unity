using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform firePos;

    [SerializeField] // private이지만 inspector 노출시
    private new AudioSource audio;
    public AudioClip fireSfx;

    //MuzzleFlash
    public MeshRenderer muzzleFlash;

    private RaycastHit hit;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        muzzleFlash.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(firePos.position, firePos.forward * 10.0f, Color.green);

        if (Input.GetMouseButtonDown(0)  )
        {
            Fire();


            if(Physics.Raycast(firePos.position,firePos.forward,out hit, 10.0f))
            {
                Debug.Log("Hit=" + hit.collider.gameObject.name);
                if (hit.collider.CompareTag("MONSTER"))
                {
                    hit.collider.gameObject.GetComponent<MonsterCtrl>().OnDamage();
                }
            }
        }

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Application.Quit();
        //}
    }

    void Fire()
    {
            //Instatiate(생성할객체, 위치, 각도), 동적생p
        Instantiate(bulletPrefab,firePos.position,firePos.rotation);
        audio.PlayOneShot(fireSfx,0.8f);
        StartCoroutine(ShowMuzzleFlash());

    }

    IEnumerator ShowMuzzleFlash()
    {

        //randon texture offset
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        muzzleFlash.material.mainTextureOffset = offset;
        //muzzleFlash.material.SetTextureOffset("_MainTex", offset);

        //randon texture scaling
        muzzleFlash.transform.localScale = Vector3.one * Random.Range(0.5f, 2.0f);

        //random rotation
        muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(0.25f);
        muzzleFlash.enabled = false;
    }
}
