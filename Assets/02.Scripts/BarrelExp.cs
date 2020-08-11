using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class BarrelExp : MonoBehaviour
{

    private int hitCount = 0;
    public GameObject expEffect;

    [System.NonSerialized] // private이지만 inspector 노출시
    private new AudioSource audio;
    public AudioClip barrelSfx;

    public Texture[] textures;
    public MeshRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        expEffect = Resources.Load<GameObject>("BigExplosionEffect");
        audio = GetComponent<AudioSource>();
        _renderer = this.gameObject.GetComponentInChildren<MeshRenderer>();

        int idx = Random.Range(0, textures.Length);
        _renderer.material.mainTexture = textures[idx];

    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            hitCount = hitCount + 1;
            if(hitCount == 3)
            {
                ExpBarrel();
            }
        }

    }

    void ExpBarrel()
    {
        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 2000.0f);

        GameObject effect = Instantiate(expEffect, transform.position, Quaternion.identity);
        audio.PlayOneShot(barrelSfx);
        Destroy(this.gameObject, 2.0f);
        Destroy(effect, 5.0f);
    }
}
