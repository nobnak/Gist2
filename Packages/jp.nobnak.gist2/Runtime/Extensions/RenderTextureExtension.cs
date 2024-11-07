using Gist2.Extensions.EditorExt;
using Gist2.Extensions.SizeExt;
using System.Collections;
using System.Collections.Generic;
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
    }
}
