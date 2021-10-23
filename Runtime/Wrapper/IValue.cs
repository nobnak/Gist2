using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Wrapper {

    public interface IValue<out T> {

        T Value { get; }
    }
}