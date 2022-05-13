using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Scope {

    public class ScopedPropertyBlock : System.IDisposable {

        public Renderer Rend { get; }
        public MaterialPropertyBlock Block { get; }

        public ScopedPropertyBlock(Renderer rend, MaterialPropertyBlock block) {
            Rend = rend;
            Block = block;

            rend.GetPropertyBlock(block);
        }

        public void Dispose() {
            Rend.SetPropertyBlock(Block);
        }

        public static implicit operator MaterialPropertyBlock (ScopedPropertyBlock spb)
            => spb.Block;
    }
}
