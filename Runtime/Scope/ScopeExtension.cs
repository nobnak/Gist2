using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Scope.ScopeExt {

    public static class ScopeExtension {

        public static ScopedPropertyBlock Scope(this MaterialPropertyBlock b, Renderer rend)
            => new ScopedPropertyBlock(rend, b);
    }
}
