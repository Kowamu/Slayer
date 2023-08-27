using System;
using UniRx;

namespace WanwanLand.Slayer.Extensions.InputSystems.Operetors
{
    public static partial class OperatorExtensions 
    {
        /// <summary>
        /// 最初の値のみを取得する
        /// </summary>
        /// <param name="stream">イベントストリーム</param>
        /// <typeparam name="TEvent">イベントの型</typeparam>
        /// <returns>最初の値のみを取得するストリーム</returns>
        public static IObservable<TEvent> TakeFirst<TEvent>(this IObservable<TEvent> stream)
        {
            return stream.Take(1);
        }

        /// <summary>
        /// 条件を満たす最初の値のみを取得する
        /// </summary>
        /// <param name="stream">イベントストリーム</param>
        /// <param name="predicate">条件を満たしているかどうかを判断するメソッド</param>
        /// <typeparam name="TEvent">イベントの型</typeparam>
        /// <returns>条件を満たす最初の値のみを取得するストリーム</returns>
        public static IObservable<TEvent> TakeFirst<TEvent>(this IObservable<TEvent> stream, Func<TEvent, bool> predicate)
        {
            return stream.Where(predicate).Take(1);
        }
    }
}