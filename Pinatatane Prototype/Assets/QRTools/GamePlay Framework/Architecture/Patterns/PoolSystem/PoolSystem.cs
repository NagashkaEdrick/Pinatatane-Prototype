using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Events;

using Sirenix.OdinInspector;

using GameplayFramework.Singletons;

namespace GameplayFramework
{
    public class PoolSystem : MonobehaviourSingleton<PoolSystem>
    {
        [BoxGroup("Pool")]
        public Dictionary<Type, List<IPoolable>> poolables = new Dictionary<Type, List<IPoolable>>();        

        public IPoolable Pool<T>() where T : MonoBehaviour => Pool<T>(Vector3.zero, Quaternion.identity);

        /// <summary>
        /// Pool an object 
        /// </summary>
        public IPoolable Pool<T>(Vector3 _pos, Quaternion _rot) where T : MonoBehaviour
        {
            for (int i = 0; i < poolables[typeof(T)].Count; i++)
            {
                if (!poolables[typeof(T)][i].IsPool)
                {
                    IPoolable _poolable = poolables[typeof(T)][i];
                    _poolable.IsPool = true;
                    _poolable.OnPool();
                    MonoBehaviour _mPoolable = _poolable as MonoBehaviour;
                    _mPoolable.transform.position = _pos;
                    _mPoolable.transform.rotation = _rot;
                    return _poolable;
                }
            }

            throw new Exception(string.Format(
                "Impossible to pull this cause : there are not enough {0} in pool.",
                typeof(T).ToString()
                ));
        }

        /// <summary>
        /// Pool an object 
        /// </summary>
        public IPoolable Pool(Type _type) => Pool(_type, Vector3.zero, Quaternion.identity);

        /// <summary>
        /// Pool an object 
        /// </summary>
        public IPoolable Pool(Type _type, Vector3 _pos, Quaternion _rot)
        {
            for (int i = 0; i < poolables[_type].Count; i++)
            {
                if (!poolables[_type][i].IsPool)
                {
                    IPoolable _poolable = poolables[_type][i];
                    _poolable.IsPool = true;
                    _poolable.OnPool();
                    MonoBehaviour _mPoolable = _poolable as MonoBehaviour;
                    _mPoolable.transform.position = _pos;
                    _mPoolable.transform.rotation = _rot;
                    return _poolable;
                }
            }

            throw new Exception(string.Format(
                "Impossible to pull this cause : there are not enough {0} in pool.",
                _type.ToString()
                ));
        }

        /// <summary>
        /// Push an object 
        /// </summary>
        public void Push(IPoolable _poolable)
        {
            _poolable.IsPool = false;
            _poolable.OnPush();
        }

        /// <summary>
        /// Return if the obect is referenced as a pool object in <see cref="poolables"/>.
        /// </summary>
        /// <param name="_type"></param>
        /// <returns></returns>
        public bool TypePooledExist(Type _type)
        {
            for (int i = 0; i < poolables.Keys.Count; i++)
            {
                if (poolables.ElementAt(i).Key.GetType() == _type)
                    return true;
            }

            return false;
        }

#if UNITY_EDITOR
        [HideInInspector]
        public UnityEvent Changed;

        [Button]
        void AddPoolables(IPoolable prefab, int qte = 25)
        {
            Transform folder = FolderExist(transform, ((MonoBehaviour)prefab).name);
            List<GameObject> gos = new List<GameObject>();
            for (int i = 0; i < folder.childCount; i++)
                gos.Add(folder.GetChild(i).gameObject);

            if(gos.Count != 0)
                for (int i = 0; i < gos.Count; i++)
                    DestroyImmediate(gos[i]);

            for (int i = 0; i < qte; i++)
                PrefabUtility.InstantiatePrefab(prefab as UnityEngine.Object);

            FindAllPoolables();
        }

        [Button]
        void FindAllPoolables()
        {
            poolables.Clear();

            var _poolables = FindObjectsOfType<MonoBehaviour>().OfType<IPoolable>();

            if (_poolables == null)
                throw new Exception("No IPoolable Founded in the scene");

            foreach(IPoolable p in _poolables)
            {
                if (!poolables.ContainsKey(p.GetType()))
                    poolables.Add(p.GetType(), new List<IPoolable>());

                poolables[p.GetType()].Add(p as IPoolable);

                ((MonoBehaviour)p).transform.parent = FolderExist(transform, ((MonoBehaviour)p).name);
            }

            Changed.Invoke();
            EditorApplication.RepaintHierarchyWindow();
        }

        Transform FolderExist(Transform _obj, string _name)
        {
            for (int i = 0; i < _obj.childCount; i++)
            {
                if (_obj.GetChild(i).name == "Folder : " + _name)
                    return _obj.GetChild(i);
            }

            Transform _newFolder = new GameObject().transform;
            _newFolder.parent = _obj;
            _newFolder.name = "Folder : " + _name;
            _newFolder.gameObject.AddComponent<PoolFolder>();

            return _newFolder;
        }

        [OnInspectorGUI]
        private void InfomationGUI()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int x = 0;

                if (transform.GetChild(i).GetComponent<PoolFolder>())
                    for (int y = 0; y < transform.GetChild(i).childCount; y++)
                        if (!transform.GetChild(i).GetChild(y).GetComponent<IPoolable>().IsPool)
                            x++;

                GUILayout.Label(transform.GetChild(i).name + ": " + x + " Poolables.");
            }
        }
#endif
    }
}