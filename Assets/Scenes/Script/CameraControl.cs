using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFllowObject : MonoBehaviour
{
    public GameObject be_followed;//玩家对象
    public float speed;//移到到玩家的速度

    public Tilemap tm;

    // Update is called once per frame
    void Update()
    {
        followPlayer();

        limitBound();
        
    }
    void followPlayer()//跟随玩家组件
    {
        //获取目标位置，z轴和摄像头本身一致
        Vector3 target = new(be_followed.transform.position.x, be_followed.transform.position.y/2, transform.position.z);
        //获取自身位置
        Vector3 self = transform.position;
        //线性插值移动
        transform.position = Vector3.Lerp(self, target, speed * Time.deltaTime);
    }

    void limitBound()//防止摄像头越界
    {
        Camera cam = GetComponent<Camera>();
        float screen_width_div_2 = cam.orthographicSize * cam.aspect;//视野的的宽度的一半
        float screen_high_div_2 = cam.orthographicSize;//视野高度的一半

        TilemapRenderer renderer = tm.GetComponent<TilemapRenderer>();
        float bound_left = renderer.bounds.min.x + 1;//获取地图左边界坐标
        float bound_right = renderer.bounds.max.x - 1;//获取地图右边界
        float bound_up = renderer.bounds.max.y - 1;//获取地图上边界
        float bound_down = renderer.bounds.min.y + 1;//获取地图下边界


        if (transform.position.x - screen_width_div_2 < bound_left)//如果视野的左边界小于地图的左边界，就将x设为地图的左边界+视野宽度的一半
        {
            transform.position = new Vector3(bound_left + screen_width_div_2, transform.position.y, transform.position.z);
        }
        if (transform.position.x + screen_width_div_2 > bound_right)//防止越过右边界
        {
            transform.position = new Vector3(bound_right - screen_width_div_2, transform.position.y, transform.position.z);
        }

        if (transform.position.y - screen_high_div_2 < bound_down)//防止越过下边界
        {
            transform.position = new Vector3(transform.position.x, bound_down + screen_high_div_2, transform.position.z);
        }
        if (transform.position.y + screen_high_div_2 > bound_up)//防止越过上边界
        {
            transform.position = new Vector3(transform.position.x, bound_up - screen_high_div_2, transform.position.z);
        }
    }
}
