using System;
using UniRx;

namespace Slayer.Extensions.InputSystems.Operetors
{
    public static partial class OperatorExtensions 
    {
        /// <summary>
        /// 最初の値を無視する
        /// </summary>
        /// <param name="stream">イベントストリーム</param>
        /// <typeparam name="TEvent">イベントの型</typeparam>
        /// <returns>最初の値を無視するストリーム</returns>
        public static IObservable<TEvent> SkipFirst<TEvent>(this IObservable<TEvent> stream)
        {
            return stream.Skip(1);
        }
    }
}