#nullable enable
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UniVerse.Core
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> InstanceDict = new();

        /// <summary>
        /// 指定された型のインスタンスを ServiceLocator に登録します。
        /// 後で Resolve メソッドを使ってインスタンスを解決できます。
        /// </summary>
        /// <typeparam name="T">登録するインスタンスの型。</typeparam>
        /// <param name="instance">登録するインスタンス。</param>
        /// <returns>
        /// ServiceLocatorBuilder オブジェクトを返します。
        /// </returns>
        public static ServiceLocatorBuilder Register<T>(T instance) where T : class
        {
            InstanceDict[typeof(T)] = instance ?? throw new ArgumentNullException(nameof(instance));
            return new ServiceLocatorBuilder(instance);
        }

        /// <summary>
        /// 指定された型のインスタンスを ServiceLocator から解除します
        /// これにより、以後 Resolve メソッドを使用してその型のインスタンスを取得できなくなります
        /// </summary>
        /// <typeparam name="T">解除するインスタンスの型</typeparam>
        public static void Unregister<T>() where T : class
        {
            InstanceDict.Remove(typeof(T));
        }

        /// <summary>
        /// 登録されているすべてのインスタンスを ServiceLocator から解除します
        /// これにより、すべての型のインスタンスが解決不可となります
        /// </summary>
        public static void Clear()
        {
            InstanceDict.Clear();
        }
        
        /// <summary>
        /// UnityのComponent型を登録します。
        /// </summary>
        /// <param name="component">登録する Component。</param>
        /// <typeparam name="T">登録する Component のクラス型。</typeparam>
        /// <returns>
        /// ServiceLocatorBuilder オブジェクトを返します。
        /// </returns>
        public static ServiceLocatorBuilder RegisterComponent<T>(T component) where T : Component
        {
            return Register(component);
        }

        /// <summary>
        /// ServiceLocator から指定された型のインスタンスを解決します。
        /// </summary>
        /// <typeparam name="T">解決する型。</typeparam>
        /// <returns>登録されたインスタンス、または null。</returns>
        public static T? Resolve<T>() where T : class
        {
            return InstanceDict.TryGetValue(typeof(T), out var value) ? value as T : LogAndReturnNull<T>();
        }

        private static T? LogAndReturnNull<T>() where T : class
        {
            Debug.LogWarning($"ServiceLocator: {typeof(T).Name} not found.");
            return null;
        }

        /// <summary>
        /// ServiceLocator と一緒に使用されるビルダークラスで、追加設定を可能にします。
        /// </summary>
        public class ServiceLocatorBuilder
        {
            private readonly object instance;

            public ServiceLocatorBuilder(object instance)
            {
                this.instance = instance ?? throw new ArgumentNullException(nameof(instance));
            }

            /// <summary>
            /// 同一インスタンスを別のインターフェースとして登録します。
            /// </summary>
            /// <typeparam name="TInterface">登録するインターフェースの型。</typeparam>
            /// <returns>現在の ServiceLocatorBuilder インスタンス。</returns>
            public ServiceLocatorBuilder As<TInterface>() where TInterface : class
            {
                InstanceDict[typeof(TInterface)] = instance;
                return this;
            }

            /// <summary>
            /// UnityのDontDestroyOnLoadを設定します。
            /// </summary>
            /// <returns>現在の ServiceLocatorBuilder インスタンス。</returns>
            public ServiceLocatorBuilder DontDestroyOnLoad()
            {
                switch (instance)
                {
                    case Component componentInstance:
                        GameObject.DontDestroyOnLoad(componentInstance.gameObject);
                        break;
                    case GameObject gameObjectInstance:
                        GameObject.DontDestroyOnLoad(gameObjectInstance);
                        break;
                    default:
                        throw new InvalidOperationException(
                            "DontDestroyOnLoad can only be applied to Component or GameObject instances."
                        );
                }
                return this;
            }
        }
    }
}