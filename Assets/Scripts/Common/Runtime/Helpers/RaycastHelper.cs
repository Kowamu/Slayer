using UnityEngine;

namespace Common.Helpers.Raycasts
{
    /// <summary>
    /// Raycastのヘルパークラス
    /// </summary>
    public static class RaycastHelper
    {
        /// <summary>
        /// カプセルのRaycastを行う
        /// </summary>
        /// <param name="origin">中心座標</param>
        /// <param name="radius">カプセルの半径</param>
        /// <param name="height">カプセルの高さ</param>
        /// <param name="direction">キャストする方向</param>
        /// <param name="layerMask">ヒット判定のレイヤーマスク</param>
        /// <returns>ヒットしたかどうかとヒット情報</returns>
        public static (bool Hitting, RaycastHit Result) CylinderCast(Vector3 origin, float radius, float height,
            Vector3 direction, LayerMask layerMask)
        {
            var halfHeight = height / 2f;
            origin += -direction * (halfHeight + radius);
            height += radius;
            
            var hitting = Physics.SphereCast(origin, radius, direction, out var result, height, layerMask);
            if (!hitting) return (false, result);
            
            var pointHeight = Vector3.Dot(result.point - origin, direction);
            hitting = pointHeight >= radius && pointHeight <= height;
            return (hitting, result);
        }
    }
}