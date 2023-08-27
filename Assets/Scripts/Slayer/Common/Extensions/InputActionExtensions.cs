using System;
using UniRx;
using UnityEngine.InputSystem;

namespace WanwanLand.Slayer.Extensions.InputSystems
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
        /// <returns>入力値のIObservable</returns>
        public static IObservable<InputAction.CallbackContext> StartedAsObservable(this InputAction action)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => action.started += h,
                h => action.started -= h);
        }
        
        /// <summary>
        /// InputAction.canceledをIObservableに変換する
        /// </summary>
        /// <param name="action">入力アクション</param>
        /// <returns>入力値のIObservable</returns>
        public static IObservable<InputAction.CallbackContext> CanceledAsObservable(this InputAction action)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => action.canceled += h,
                h => action.canceled -= h);
        }
        
        /// <summary>
        /// InputAction.performedをIObservableに変換する
        /// </summary>
        /// <param name="action">入力アクション</param>
        /// <returns>入力値のIObservable</returns>
        public static IObservable<InputAction.CallbackContext> PerformedAsObservable(this InputAction action)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => action.performed += h,
                h => action.performed -= h);
        }
    }
}