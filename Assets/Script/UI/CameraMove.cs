using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Header("移动设置")]
    public float maxDistance = 3f;    // 最大移动距离（基于局部坐标系）
    public float returnSpeed = 5f;    // 返回速度
    public float moveSpeed = 50f;     // 移动速度

    private Vector3 startLocalPos;    // 存储初始局部位置

    public static CameraMove Instance; // 静态实例

    void Awake()
    {
        Instance = this; // 设置单例
        DontDestroyOnLoad(gameObject);

    }
        void Start()
    {
        // 记录初始局部位置（相对于父物体）
        startLocalPos = transform.localPosition;
    }

    void Update()
    {
        if (PlayerManager.instance.player.isClimbWall)
            return;

        HandleVerticalMovement();
        HandleReturn();
    }

    void HandleVerticalMovement()
    {
        Vector3 inputDir = Vector3.zero;

        // 输入检测
        if (Input.GetKey(KeyCode.W)) inputDir.y = +1f;
        if (Input.GetKey(KeyCode.S)) inputDir.y = -1f;

        // 计算目标局部位置
        Vector3 targetLocalPos = transform.localPosition + inputDir * moveSpeed * Time.deltaTime;

        // 计算与初始位置的局部距离
        float currentDistance = Mathf.Abs(targetLocalPos.y - startLocalPos.y);

        // 限制移动范围
        if (currentDistance <= maxDistance)
        {
            // 修改局部坐标的Y轴
            transform.localPosition = new Vector3(
                startLocalPos.x, // 保持X轴与父物体同步
                targetLocalPos.y,
                startLocalPos.z
            );
        }
    }

    void HandleReturn()
    {
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            // 平滑返回初始局部位置
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                startLocalPos,
                returnSpeed * Time.deltaTime
            );
        }
    }
}
