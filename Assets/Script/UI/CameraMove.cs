using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Header("�ƶ�����")]
    public float maxDistance = 3f;    // ����ƶ����루���ھֲ�����ϵ��
    public float returnSpeed = 5f;    // �����ٶ�
    public float moveSpeed = 50f;     // �ƶ��ٶ�

    private Vector3 startLocalPos;    // �洢��ʼ�ֲ�λ��

    public static CameraMove Instance; // ��̬ʵ��

    void Awake()
    {
        Instance = this; // ���õ���
        DontDestroyOnLoad(gameObject);

    }
        void Start()
    {
        // ��¼��ʼ�ֲ�λ�ã�����ڸ����壩
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

        // ������
        if (Input.GetKey(KeyCode.W)) inputDir.y = +1f;
        if (Input.GetKey(KeyCode.S)) inputDir.y = -1f;

        // ����Ŀ��ֲ�λ��
        Vector3 targetLocalPos = transform.localPosition + inputDir * moveSpeed * Time.deltaTime;

        // �������ʼλ�õľֲ�����
        float currentDistance = Mathf.Abs(targetLocalPos.y - startLocalPos.y);

        // �����ƶ���Χ
        if (currentDistance <= maxDistance)
        {
            // �޸ľֲ������Y��
            transform.localPosition = new Vector3(
                startLocalPos.x, // ����X���븸����ͬ��
                targetLocalPos.y,
                startLocalPos.z
            );
        }
    }

    void HandleReturn()
    {
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            // ƽ�����س�ʼ�ֲ�λ��
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                startLocalPos,
                returnSpeed * Time.deltaTime
            );
        }
    }
}
