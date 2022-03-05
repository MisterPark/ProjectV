using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    Camera cam;
    SphereCollider parentCollider;
    Missile missile;
    float duration = 0.1f;
    void Start()
    {
        cam = Camera.main;
        parentCollider = gameObject.transform.GetComponentInParent<SphereCollider>();
        transform.localScale = new Vector3(1, 0, 1);
        parentCollider.center = new Vector3(0, 20, 0);
    }

    private void OnEnable()
    {
        parentCollider = gameObject.transform.GetComponentInParent<SphereCollider>();
        transform.localScale = new Vector3(1, 0, 1);
        parentCollider.center = new Vector3(0, 20, 0);
    }

    void FixedUpdate()
    {
        // 스케일 키우기
        float yScale = transform.localScale.y;
        yScale += 2f * Time.fixedDeltaTime * (1f / duration);
        transform.localScale = new Vector3(1, yScale, 1);

        // 콜라이더 위치 조정
        float yPos = transform.localPosition.y - transform.localScale.y * 10f;
        parentCollider.center = new Vector3(0, yPos, 0);
        
        // 빌보드
        Vector3 camForward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
        transform.forward = camForward.normalized;

    }
}
