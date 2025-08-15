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
        public string cameraTrackName = "CameraMove Track"; // Timeline轨道名称

        void Start()
        {
            var director = GetComponent<PlayableDirector>();

            if (CameraMove.Instance == null)
            {
                Debug.LogError("PlayerCamera not found! Ensure Player scene is loaded first.");
                return;
            }

            // 动态绑定轨道
            foreach (var output in director.playableAsset.outputs)
            {
                if (output.outputTargetType == typeof(Animator) && output.streamName == cameraTrackName)
                {
                    director.SetGenericBinding(output.sourceObject, CameraMove.Instance.GetComponent<Animator>());
                }
            }

            director.Play(); // 自动播放
        }
    }
}

