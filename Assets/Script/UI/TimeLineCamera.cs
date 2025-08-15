using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
public class TimeLineCamera : MonoBehaviour
{
    [RequireComponent(typeof(PlayableDirector))]
    public class TimelineCameraBinder : MonoBehaviour
    {
        public string cameraTrackName = "CameraMove Track"; // Timeline�������

        void Start()
        {
            var director = GetComponent<PlayableDirector>();

            if (CameraMove.Instance == null)
            {
                Debug.LogError("PlayerCamera not found! Ensure Player scene is loaded first.");
                return;
            }

            // ��̬�󶨹��
            foreach (var output in director.playableAsset.outputs)
            {
                if (output.outputTargetType == typeof(Animator) && output.streamName == cameraTrackName)
                {
                    director.SetGenericBinding(output.sourceObject, CameraMove.Instance.GetComponent<Animator>());
                }
            }

            director.Play(); // �Զ�����
        }
    }
}

