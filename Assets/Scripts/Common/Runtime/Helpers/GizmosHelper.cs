using UnityEditor;
using UnityEngine;

namespace Common.Helpers.Gizmos
{
    /// <summary>
    /// Gizmosのヘルパークラス
    /// </summary>
    public static class GizmosHelper
    {
        /// <summary>
        /// カプセルのワイヤーフレームを描画する
        /// </summary>
        /// <param name="center">中心座標</param>
        /// <param name="rotation">回転</param>
        /// <param name="radius">半径</param>
        /// <param name="height">高さ</param>
        public static void DrawWireCapsule(Vector3 center, Quaternion rotation, float radius, float height)
        {
            var matrix = Matrix4x4.TRS(center, rotation, Handles.matrix.lossyScale);
            using (new Handles.DrawingScope(UnityEngine.Gizmos.color, matrix))
            {
                var pointOffset = (height - radius * 2f) / 2f;
                
                // 上の半球
                Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.left, Vector3.back, -180f, radius);
                Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.back, Vector3.left, 180f, radius);
                Handles.DrawWireDisc(Vector3.up * pointOffset, Vector3.up, radius);
                
                // 下の半球
                Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.left, Vector3.back, 180f, radius);
                Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.back, Vector3.left, -180f, radius);
                Handles.DrawWireDisc(Vector3.down * pointOffset, Vector3.up, radius);
                
                // 側面の線
                Handles.DrawLine(new Vector3(0f, pointOffset, -radius), new Vector3(0f, -pointOffset, -radius));
                Handles.DrawLine(new Vector3(0f, pointOffset, radius), new Vector3(0f, -pointOffset, radius));
                Handles.DrawLine(new Vector3(-radius, pointOffset, 0f), new Vector3(-radius, -pointOffset, 0f));
                Handles.DrawLine(new Vector3(radius, pointOffset, 0f), new Vector3(radius, -pointOffset, 0f));
            }
        }
        
        /// <summary>
        /// カプセルのワイヤーフレームを描画する
        /// </summary>
        /// <param name="center">中心座標</param>
        /// <param name="rotation">回転</param>
        /// <param name="radius">半径</param>
        /// <param name="height">高さ</param>
        public static void DrawWireCylinder(Vector3 center, Quaternion rotation, float radius, float height)
        {
            var matrix = Matrix4x4.TRS(center, rotation, Handles.matrix.lossyScale);
            using (new Handles.DrawingScope(UnityEngine.Gizmos.color, matrix))
            {
                var pointOffset = height / 2f;
                
                // 上面
                Handles.DrawLine(Vector3.up * pointOffset + Vector3.right * radius, Vector3.up * pointOffset + Vector3.left * radius);
                Handles.DrawLine(Vector3.up * pointOffset + Vector3.forward * radius, Vector3.up * pointOffset + Vector3.back * radius);
                Handles.DrawWireDisc(Vector3.up * pointOffset, Vector3.up, radius);
                
                // 下面
                Handles.DrawLine(Vector3.down * pointOffset + Vector3.right * radius, Vector3.down * pointOffset + Vector3.left * radius);
                Handles.DrawLine(Vector3.down * pointOffset + Vector3.forward * radius, Vector3.down * pointOffset + Vector3.back * radius);
                Handles.DrawWireDisc(Vector3.down * pointOffset, Vector3.up, radius);
                
                // 側面の線
                Handles.DrawLine(new Vector3(0, pointOffset, -radius), new Vector3(0, -pointOffset, -radius));
                Handles.DrawLine(new Vector3(0, pointOffset, radius), new Vector3(0, -pointOffset, radius));
                Handles.DrawLine(new Vector3(-radius, pointOffset, 0), new Vector3(-radius, -pointOffset, 0));
                Handles.DrawLine(new Vector3(radius, pointOffset, 0), new Vector3(radius, -pointOffset, 0));
            }
        }
    }
}