using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Wayway.Engine
{
    public static class GameObjectExtension
    {
        public static void DestroyAllChildren(this GameObject gameObject, bool includeInactive = false)
        {
            foreach (Transform child in gameObject.GetComponentInChildren<Transform>(includeInactive))
            {
                Object.Destroy(child.gameObject);
            }
        }
        public static void DestroyImmediateAllChildren(this GameObject gameObject, bool includeInactive = false)
        {
            foreach (Transform child in gameObject.GetComponentInChildren<Transform>(includeInactive))
            {
                Object.DestroyImmediate(child.gameObject);
            }
        }

        public static T GetComponentOnlyChildren<T>(this GameObject gameobject) where T : MonoBehaviour
        {
            return gameobject.GetComponentsInChildren<T>().First(x => x.gameObject != gameobject);
        }

        public static List<T> GetComponentsInOnlyChildren<T>(this GameObject gameobject) where T : MonoBehaviour
        {
            var selfBehaviour = gameobject.GetComponent<T>();
            var result = new List<T>();

            gameobject.GetComponentsInChildren(result);

            if (selfBehaviour) result.Remove(selfBehaviour);

            return result;
        }
        
        public static IEnumerator Moving(this GameObject gameObject, Vector3 a, Vector3 b, Action postMove = null)
        {
            var elapsedTime = .0f;
            var expectedTime = (b - a).magnitude * 2f / Mathf.Sqrt(5f);

            while (elapsedTime < expectedTime)
            {
                elapsedTime += Time.deltaTime;
                var t = elapsedTime / expectedTime;

                gameObject.transform.position = Vector3.Lerp(a, b, t);

                yield return null;
            }

            postMove?.Invoke();
        }
        
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : MonoBehaviour
        {
            var component = gameObject.GetComponent<T>();
            if (component == null) gameObject.AddComponent<T>();
            return component;
        }
        
        public static bool HasComponent<T>(this GameObject gameObject) where T : MonoBehaviour
        {
            return gameObject.GetComponent<T>() != null;
        }
    }
}


