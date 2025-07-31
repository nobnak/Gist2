using Unity.Mathematics;
using UnityEngine;

namespace Gist2.Extensions.RenderTextureExt {

	public static class RenderTextureExtension {

        public static RenderTexture Clear(this RenderTexture tex, bool clearColor = true, bool clearDepth = true, Color color = default) {
            var prev = RenderTexture.active;
            RenderTexture.active = tex;
            GL.Clear(clearColor, clearDepth, color);
            RenderTexture.active = prev;
            return tex;
        }
        public static RenderTexture Resize(this RenderTexture tex, int2 size) {
            if (tex != null && tex.width != size.x && tex.height != size.y) {
                tex.Release();
                tex.width = size.x;
                tex.height = size.y;
                tex.Create();
            }
            return tex;
        }
    }
}
