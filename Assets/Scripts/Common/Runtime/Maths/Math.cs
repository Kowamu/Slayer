using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Common.Maths
{
    /// <summary>
    /// 数学関数を定義するクラス
    /// </summary>
    public static class Math
    {
        #region Constants
        
        /// <summary>
        /// 小数点誤差の許容値の最小値
        /// </summary>
        public const float Epsilon = float.Epsilon == 0f ? 1.1754944E-38f : float.Epsilon;
        
        /// <summary>
        /// 小数点誤差の許容値
        /// </summary>
        public const float Tolerance = 0.0000001f;

        #endregion
        #region IsAlmostEqual
        
        /// <summary>
        /// 小数点誤差を許容した等値比較をする
        /// </summary>
        /// <param name="a">比較対象の左辺値</param>
        /// <param name="b">比較対象の右辺値</param>
        /// <returns>値がほぼ等しいか</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostEqual(float a, float b) =>
            MathF.Abs(b - a) < MathF.Max(Tolerance * MathF.Max(MathF.Abs(a), MathF.Abs(b)), Epsilon * 8f);
        
        /// <summary>
        /// 小数点誤差を許容した等値比較をする
        /// </summary>
        /// <param name="a">比較対象の左辺値</param>
        /// <param name="b">比較対象の右辺値</param>
        /// <returns>値がほぼ等しいか</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostEqual(Vector2 a, Vector2 b) => IsAlmostEqual(a.x, b.x) && IsAlmostEqual(a.y, b.y);
        
        /// <summary>
        /// 小数点誤差を許容した等値比較をする
        /// </summary>
        /// <param name="a">比較対象の左辺値</param>
        /// <param name="b">比較対象の右辺値</param>
        /// <returns>値がほぼ等しいか</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostEqual(Vector3 a, Vector3 b) =>
            IsAlmostEqual(a.x, b.x) && IsAlmostEqual(a.y, b.y) && IsAlmostEqual(a.z, b.z);
        
        #endregion
        #region IsAlmostZero
        
        /// <summary>
        /// 小数点誤差を許容した0との等値比較をする
        /// </summary>
        /// <param name="v">比較対象</param>
        /// <returns>値が0とほぼ等しいか</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostZero(this float v) => IsAlmostEqual(v, 0f);
        
        /// <summary>
        /// 小数点誤差を許容した0との等値比較をする
        /// </summary>
        /// <param name="v">比較対象</param>
        /// <returns>値が0とほぼ等しいか</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostZero(this Vector2 v) => IsAlmostEqual(v.x, 0f) && IsAlmostEqual(v.y, 0f);
        
        /// <summary>
        /// 小数点誤差を許容した0との等値比較をする
        /// </summary>
        /// <param name="v">比較対象</param>
        /// <returns>値が0とほぼ等しいか</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostZero(this Vector3 v) =>
            IsAlmostEqual(v.x, 0f) && IsAlmostEqual(v.y, 0f) && IsAlmostEqual(v.z, 0f);
        
        #endregion
        #region Power
        
        /// <summary>
        /// 指定された値を2乗する
        /// </summary>
        /// <param name="f">2乗する値</param>
        /// <returns>2乗した値</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqr(this float f) => f * f;
        
        /// <summary>
        /// 指定された値を3乗する
        /// </summary>
        /// <param name="f">3乗する値</param>
        /// <returns>3乗した値</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cube(this float f) => f * f * f;
        
        /// <summary>
        /// 指定された値を累乗する
        /// </summary>
        /// <param name="f">累乗する値</param>
        /// <param name="power">累乗する値</param>
        /// <returns>累乗した値</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Pow(this float f, float power) => MathF.Pow(f, power);

        /// <summary>
        /// 指定された値を累乗する
        /// </summary>
        /// <param name="f">累乗する値</param>
        /// <param name="power">累乗する値</param>
        /// <returns>累乗した値</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Pow(this float f, int power)
        {
            switch (power)
            {
                case 0: return 1f;
                case 1: return f;
                case 2: return f.Sqr();
                case 3: return f.Cube();
                default:
                {
                    var result = f * f * f * f;
                    for (var i = 4; i < power; i++)
                    {
                        result *= f;
                    }
                    return result;
                }
            }
        }

        #endregion
        #region Root

        /// <summary>
        /// 指定された値の平方根を求める
        /// </summary>
        /// <param name="f">平方根を求める値</param>
        /// <returns>平方根</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqrt(this float f) => MathF.Sqrt(f);

        /// <summary>
        /// 指定された値の立方根を求める
        /// </summary>
        /// <param name="f">立方根を求める値</param>
        /// <returns>立方根</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cbrt(this float f) => MathF.Cbrt(f);

        /// <summary>
        /// 指定された値の累乗根を求める
        /// </summary>
        /// <param name="f">累乗根を求める値</param>
        /// <param name="power">累乗根</param>
        /// <returns>累乗根</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Root(this float f, float power) => MathF.Pow(f, 1f / power);

        #endregion
        #region Clamp
        
        /// <summary>
        /// 値を指定された範囲内に収める
        /// </summary>
        /// <param name="f">収める値</param>
        /// <param name="min">最小値</param>
        /// <param name="max">最大値</param>
        /// <returns>収めた値</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(this float f, float min, float max) => MathF.Max(min, MathF.Min(max, f));
        
        #endregion
        #region ClampMagnitude

        /// <summary>
        /// ベクトルの大きさを指定された範囲内に収める
        /// </summary>
        /// <param name="v">収めるベクトル</param>
        /// <param name="min">最小値</param>
        /// <param name="max">最大値</param>
        /// <returns>収めたベクトル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClampMagnitude(this Vector2 v, float min, float max)
        {
            var magnitude = v.magnitude;
            return magnitude == 0f ? Vector2.zero : v / magnitude * magnitude.Clamp(min, max);
        }
        
        /// <summary>
        /// ベクトルの大きさを指定された範囲内に収める
        /// </summary>
        /// <param name="v">収めるベクトル</param>
        /// <param name="max">最大値</param>
        /// <returns>収めたベクトル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClampMagnitude(this Vector2 v, float max = 1f) => ClampMagnitude(v, 0f, max);
        
        /// <summary>
        /// ベクトルの大きさを指定された範囲内に収める
        /// </summary>
        /// <param name="v">収めるベクトル</param>
        /// <param name="min">最小値</param>
        /// <param name="max">最大値</param>
        /// <returns>収めたベクトル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ClampMagnitude(this Vector3 v, float min, float max)
        {
            var magnitude = v.magnitude;
            return magnitude == 0f ? Vector3.zero : v / magnitude * magnitude.Clamp(min, max);
        }
        
        /// <summary>
        /// ベクトルの大きさを指定された範囲内に収める
        /// </summary>
        /// <param name="v">収めるベクトル</param>
        /// <param name="max">最大値</param>
        /// <returns>収めたベクトル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ClampMagnitude(this Vector3 v, float max = 1f) => ClampMagnitude(v, 0f, max);
        
        #endregion
        #region Repeat

        /// <summary>
        /// 指定された値を [min, max] の範囲内に繰り返すように丸めます
        /// </summary>
        /// <param name="f">丸められる値</param>
        /// <param name="min">丸められる値の下限</param>
        /// <param name="max">丸められる値の上限</param>
        /// <returns>丸められた値</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Repeat(this float f, float min, float max) => f - MathF.Floor(f / (max - min)) * (max - min);

        /// <summary>
        /// 指定された値を [0, length] の範囲に繰り返すように丸めます
        /// </summary>
        /// <param name="f">丸められる値</param>
        /// <param name="max">丸められる値の上限</param>
        /// <returns>丸められた値</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Repeat(this float f, float max = 1f) => Repeat(f, 0.0f, max);

        #endregion
        #region Remap

        /// <summary>
        /// 値を指定された範囲から別の範囲に変換する
        /// </summary>
        /// <param name="f">変換する値</param>
        /// <param name="fromMin">変換前の最小値</param>
        /// <param name="fromMax">変換前の最大値</param>
        /// <param name="toMin">変換後の最小値</param>
        /// <param name="toMax">変換後の最大値</param>
        /// <returns>変換後の値</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Remap(this float f, float fromMin, float fromMax, float toMin, float toMax) =>
            (f - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;

        #endregion
        #region RemapMagnitude

        /// <summary>
        /// ベクトルの大きさを指定された範囲から別の範囲に変換する
        /// </summary>
        /// <param name="v">変換するベクトル</param>
        /// <param name="fromMin">変換前の最小値</param>
        /// <param name="fromMax">変換前の最大値</param>
        /// <param name="toMin">変換後の最小値</param>
        /// <param name="toMax">変換後の最大値</param>
        /// <returns>変換後のベクトル</returns>
        public static Vector2 RemapMagnitude(this Vector2 v, float fromMin, float fromMax, float toMin = 0f, 
            float toMax = 1f)
        {
            var magnitude = v.magnitude;
            return v / magnitude * magnitude.Remap(fromMin, fromMax, toMin, toMax);
        }

        /// <summary>
        /// ベクトルの大きさを指定された範囲から別の範囲に変換する
        /// </summary>
        /// <param name="value">変換するベクトル</param>
        /// <param name="fromMin">変換前の最小値</param>
        /// <param name="fromMax">変換前の最大値</param>
        /// <param name="toMin">変換後の最小値</param>
        /// <param name="toMax">変換後の最大値</param>
        /// <returns>変換後のベクトル</returns>
        public static Vector3 RemapMagnitude(this Vector3 value, float fromMin, float fromMax, float toMin = 0f, 
            float toMax = 1f)
        {
            var magnitude = value.magnitude;
            return value / magnitude * magnitude.Remap(fromMin, fromMax, toMin, toMax);
        }

        #endregion
        #region Only

        /// <summary>
        /// X成分以外を0にしたベクトルを返す
        /// </summary>
        /// <param name="v">元となるベクトル</param>
        /// <returns>X成分以外を0にしたベクトル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 OnlyX(this Vector2 v) => new(v.x, 0f);
        
        /// <summary>
        /// Y成分以外を0にしたベクトルを返す
        /// </summary>
        /// <param name="v">元となるベクトル</param>
        /// <returns>Y成分以外を0にしたベクトル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 OnlyY(this Vector2 v) => new(0f, v.y);
        
        /// <summary>
        /// X成分以外を0にしたベクトルを返す
        /// </summary>
        /// <param name="v">元となるベクトル</param>
        /// <returns>X成分以外を0にしたベクトル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 OnlyX(this Vector3 v) => new(v.x, 0f, 0f);
        
        /// <summary>
        /// Y成分以外を0にしたベクトルを返す
        /// </summary>
        /// <param name="v">元となるベクトル</param>
        /// <returns>Y成分以外を0にしたベクトル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 OnlyY(this Vector3 v) => new(0f, v.y, 0f);
        
        /// <summary>
        ///Z成分以外を0にしたベクトルを返す
        /// </summary>
        /// <param name="v">元となるベクトル</param>
        /// <returns>Z成分以外を0にしたベクトル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 OnlyZ(this Vector3 v) => new(0f, 0f, v.z);

        #endregion
        #region Except
        
        /// <summary>
        /// X成分のみを0にしたベクトルを返す
        /// </summary>
        /// <param name="v">元となるベクトル</param>
        /// <returns>X成分のみを0にしたベクトル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ExceptX(this Vector2 v) => new(0f, v.y);
        
        /// <summary>
        /// Y成分のみを0にしたベクトルを返す
        /// </summary>
        /// <param name="v">元となるベクトル</param>
        /// <returns>Y成分のみを0にしたベクトル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ExceptY(this Vector2 v) => new(v.x, 0f);
        
        /// <summary>
        /// X成分のみを0にしたベクトルを返す
        /// </summary>
        /// <param name="v">元となるベクトル</param>
        /// <returns>X成分のみを0にしたベクトル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ExceptX(this Vector3 v) => new(0f, v.y, v.z);
        
        /// <summary>
        /// Y成分のみを0にしたベクトルを返す
        /// </summary>
        /// <param name="v">元となるベクトル</param>
        /// <returns>Y成分のみを0にしたベクトル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ExceptY(this Vector3 v) => new(v.x, 0f, v.z);
        
        /// <summary>
        /// Z成分のみを0にしたベクトルを返す
        /// </summary>
        /// <param name="v">元となるベクトル</param>
        /// <returns>Z成分のみを0にしたベクトル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ExceptZ(this Vector3 v) => new(v.x, v.y, 0f);

        #endregion
        #region ToVector2
        
        /// <summary>
        /// 3次元ベクトルから変換した2次元ベクトル(X,Z)を返す
        /// </summary>
        /// <param name="v">元となる3次元ベクトル</param>
        /// <returns>変換した2次元ベクトル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 XYZToXZ(this Vector3 v) => new(v.x, v.z);
        
        #endregion
        #region ToVector3
        
        /// <summary>
        /// 2次元ベクトル(X,Y)と指定したZ要素の値を組み合わせた3次元ベクトルを返す
        /// </summary>
        /// <param name="v">元となる2次元ベクトル</param>
        /// <param name="z">指定するZ要素の値</param>
        /// <returns>変換した3次元ベクトル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XYToXYZ(this Vector2 v, float z = 0f) => new(v.x, v.y, z);

        /// <summary>
        /// 2次元ベクトル(X,Z)と指定したY要素の値を組み合わせた3次元ベクトルを返す
        /// </summary>
        /// <param name="v">元となる2次元ベクトル</param>
        /// <param name="y">指定するY要素の値</param>
        /// <returns>変換した3次元ベクトル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XZToXYZ(this Vector2 v, float y = 0f) => new(v.x, y, v.y);
        
        #endregion
    }
}