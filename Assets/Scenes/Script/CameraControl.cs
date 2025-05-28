using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFllowObject : MonoBehaviour
{
    public GameObject be_followed;//��Ҷ���
    public float speed;//�Ƶ�����ҵ��ٶ�

    public Tilemap tm;

    // Update is called once per frame
    void Update()
    {
        followPlayer();

        limitBound();
        
    }
    void followPlayer()//����������
    {
        //��ȡĿ��λ�ã�z�������ͷ����һ��
        Vector3 target = new(be_followed.transform.position.x, be_followed.transform.position.y/2, transform.position.z);
        //��ȡ����λ��
        Vector3 self = transform.position;
        //���Բ�ֵ�ƶ�
        transform.position = Vector3.Lerp(self, target, speed * Time.deltaTime);
    }

    void limitBound()//��ֹ����ͷԽ��
    {
        Camera cam = GetComponent<Camera>();
        float screen_width_div_2 = cam.orthographicSize * cam.aspect;//��Ұ�ĵĿ�ȵ�һ��
        float screen_high_div_2 = cam.orthographicSize;//��Ұ�߶ȵ�һ��

        TilemapRenderer renderer = tm.GetComponent<TilemapRenderer>();
        float bound_left = renderer.bounds.min.x + 1;//��ȡ��ͼ��߽�����
        float bound_right = renderer.bounds.max.x - 1;//��ȡ��ͼ�ұ߽�
        float bound_up = renderer.bounds.max.y - 1;//��ȡ��ͼ�ϱ߽�
        float bound_down = renderer.bounds.min.y + 1;//��ȡ��ͼ�±߽�


        if (transform.position.x - screen_width_div_2 < bound_left)//�����Ұ����߽�С�ڵ�ͼ����߽磬�ͽ�x��Ϊ��ͼ����߽�+��Ұ��ȵ�һ��
        {
            transform.position = new Vector3(bound_left + screen_width_div_2, transform.position.y, transform.position.z);
        }
        if (transform.position.x + screen_width_div_2 > bound_right)//��ֹԽ���ұ߽�
        {
            transform.position = new Vector3(bound_right - screen_width_div_2, transform.position.y, transform.position.z);
        }

        if (transform.position.y - screen_high_div_2 < bound_down)//��ֹԽ���±߽�
        {
            transform.position = new Vector3(transform.position.x, bound_down + screen_high_div_2, transform.position.z);
        }
        if (transform.position.y + screen_high_div_2 > bound_up)//��ֹԽ���ϱ߽�
        {
            transform.position = new Vector3(transform.position.x, bound_up - screen_high_div_2, transform.position.z);
        }
    }
}
