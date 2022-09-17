using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;//����Ŀ�����
    public float moveSpeed = 10;//�ƶ��ٶ�
    public float spring = 0.01f;//����ϵ��
    private Vector3 offset = Vector3.zero;
    void FixedUpdate()
    {
        offset.z = this.transform.position.z - target.transform.position.z;
        float distance = Vector3.Distance(this.transform.position, target.position + offset);
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.position + offset, distance * spring + moveSpeed * Time.deltaTime);
    }

}
