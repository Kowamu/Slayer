using System;
using UniRx;
using UnityEngine.InputSystem;

namespace Common.Extensions.InputSystems
{
    /// <summary>
    /// InputActionの拡張クラス
    /// </summary>
    public static class InputActionExtensions
    {
        /// <summary>
        /// InputAction.startedをIObservableに変換する
        /// </summary>
        /// <param name="action">入力アクション</param>
        /// <typeparam name="TValue">入力値の型</typeparam>
        /// <returns>入力値のIObservable</returns>
        public static IObservable<TValue> StartedAsObservable<TValue>(this InputAction action)
            where TValue : struct
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                    h => action.started += h,
                    h => action.started -= h)
                .Select(context => context.ReadValue<TValue>());
        }
        
        /// <summary>
        /// InputAction.canceledをIObservableに変換する
        /// </summary>
        /// <param name="action">入力アクション</param>
        /// <typeparam name="TValue">入力値の型</typeparam>
        /// <returns>入力値のIObservable</returns>
        public static IObservable<TValue> CanceledAsObservable<TValue>(this InputAction action)
            where TValue : struct
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                    h => action.canceled += h,
                    h => action.canceled -= h)
                .Select(context => context.ReadValue<TValue>());
        }
        
        /// <summary>
        /// InputAction.performedをIObservableに変換する
        /// </summary>
        /// <param name="action">入力アクション</param>
        /// <typeparam name="TValue">入力値の型</typeparam>
        /// <returns>入力値のIObservable</returns>
        public static IObservable<TValue> PerformedAsObservable<TValue>(this InputAction action)
            where TValue : struct
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                    h => action.performed += h,
                    h => action.performed -= h)
                .Select(context => context.ReadValue<TValue>());
        }
    }
}