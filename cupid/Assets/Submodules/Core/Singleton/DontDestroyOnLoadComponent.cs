using UnityEngine;

namespace Wayway.Engine.Singleton
{
    public class DontDestroyOnLoadComponent : MonoBehaviour
    {
        public bool ShowDebugMessage;
        public MonoBehaviour TargetBehaviour;

        private void Awake()
        {
            if (!transform.root.gameObject.Equals(gameObject))
            {
                if (ShowDebugMessage)
                {
                    Debug.LogError($"Don't Destroyed Component must be in root gameObject! \n" +
                    $"Root Object is : {transform.root.gameObject} \n" +
                    $"Current Object is : {gameObject}");
                }

                return;
            }

            var components = FindObjectsOfType(TargetBehaviour.GetType());

            switch (components.Length)
            {
                case > 1:
                {
                    if (ShowDebugMessage)
                    {
                        Debug.Log($"{TargetBehaviour.GetType()} is came from another scene. \n" +
                                  $"In this scene {TargetBehaviour.GetType()} gameObject Destroy");
                    }

                    /* Annotation */
                    DestroyImmediate(gameObject);
                    break;
                }
                case 1:
                {
                    if (ShowDebugMessage)
                    {
                        Debug.Log($"Don't Destroyed On Load :: {TargetBehaviour.GetType()} registered");
                    }

                    DontDestroyOnLoad(gameObject);
                    break;
                }
                default:
                {
                    if (ShowDebugMessage)
                    {
                        Debug.LogError($"Can't Find {TargetBehaviour.GetType()}.");
                    }
                    
                    break;
                }
            }
        }
    }
}

/* Annotation
MainGame 프리팹이 있는 씬에서 시작하여 다시 돌아올 때 새로 불려지는 씬의 MainGame 프리팹을 바로 삭제하기 위하여 
해당 함수내에(38) DestroyImmediately를 사용했다. 조금 찝찝한 구석이 있다. */
