using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    Camera cam;
    SphereCollider parentCollider;
    bool impactFlag = false;
    Missile missile;
    void Start()
    {
        cam = Camera.main;
        parentCollider = gameObject.transform.GetComponentInParent<SphereCollider>();
        missile = gameObject.GetComponentInParent<Missile>();
        transform.localScale = new Vector3(1, 0, 1);
        parentCollider.center = new Vector3(0, 20, 0);
    }

    private void OnEnable()
    {
        parentCollider = gameObject.transform.GetComponentInParent<SphereCollider>();
        transform.localScale = new Vector3(1, 0, 1);
        parentCollider.center = new Vector3(0, 20, 0);
        impactFlag = false;
    }

    private void OnDisable()
    {
        GameObject impact = ObjectPool.Instance.Allocate("LightningImpact");
        ImpactV2 comp = impact.GetComponent<ImpactV2>();
        comp.Duration = 0.3f;
        impact.transform.position = transform.parent.transform.position;
    }

    void FixedUpdate()
    {
        // 스케일 키우기
        float yScale = transform.localScale.y;
        yScale += Time.fixedDeltaTime * 2 / missile.Duration;
        transform.localScale = new Vector3(1, yScale, 1);

        // 콜라이더 위치 조정
        float yPos = transform.localPosition.y - transform.localScale.y * 10f;
        parentCollider.center = new Vector3(0, yPos, 0);
        
        // 빌보드
        Vector3 camForward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
        transform.forward = camForward.normalized;

        if(impactFlag ==false && yScale >= 1.9f)
        {
            //impactFlag = true;
            //GameObject impact = ObjectPool.Instance.Allocate("LightningImpact");
            //ImpactV2 comp = impact.GetComponent<ImpactV2>();
            //comp.Duration = 0.3f;
            //impact.transform.position = transform.parent.transform.position;
        }
    }
}
