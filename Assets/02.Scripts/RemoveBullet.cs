using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;


    void OnCollisionEnter(Collision coll)  // 충돌이 한번일어날때 ,붙어있을때(stay), Exit
    {
        //if(coll.gameObject.tag == "BULLET")
        //{
        //    Destroy(coll.gameObject);
        //}

        if(coll.collider.CompareTag("BULLET")) //c#에선 메모리 감소를위해 이 스트럭쳐가 더 좋음
        {
            ContactPoint[] cp = coll.contacts;
            Vector3 _normal = cp[0].normal; //법선벡터
            Vector3 _pos = cp[0].point;   //충돌위치

            Quaternion rot = Quaternion.LookRotation(_normal);
            var effect = Instantiate(sparkEffect, _pos, rot);

            Destroy(effect,0.25f);
            Destroy(coll.gameObject);
        }
    }
}
