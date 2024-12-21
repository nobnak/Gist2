using Gist2.Deferred;
using Gist2.Interfaces;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Gist2.Wrappers {

    public class GraphicsBuffereWrapper<T> : IValue<GraphicsBuffer>, System.IDisposable
        where T : struct {

        #region intializer
        public System.Func<int, int, GraphicsBuffer> Generator { get; set; }
        public event System.Action<GraphicsBuffereWrapper<T>> Changed;
        #endregion

        protected List<T> data = new();
        protected GraphicsBuffer buf;

        protected Validator dataChanged = new();

        public GraphicsBuffereWrapper(System.Func<int, int, GraphicsBuffer> generator) {
            this.Generator = generator;
            dataChanged.OnValidate += () => {
                if (buf == null || buf.count != data.Capacity) {
                    buf?.Release();
                    if (data.Count > 0)
                        buf = Generator(data.Count, Marshal.SizeOf<T>());
                }
                if (buf != null && data.Count > 0)
                    buf.SetData(data);
            };
        }
        public GraphicsBuffereWrapper() : this(null) { }

        #region properties
        public GraphicsBuffer Value {
            get {
                dataChanged.Validate();
                return buf;
            }
        }
        #endregion

        #region IDisposable
        public void Dispose() {
            if (buf != null) {
                buf.Release();
                buf = null;
            }
        }
        #endregion

        #region list
        public T this[int index] {
            get => data[index];
            set {
                data[index] = value;
                dataChanged.Invalidate();
            }
        }
        public GraphicsBuffereWrapper<T> Add(T item) {
            data.Add(item);
            dataChanged.Invalidate();
            return this;
        }
        public GraphicsBuffereWrapper<T> RemoveAt(int index) {
            data.RemoveAt(index);
            dataChanged.Invalidate();
            return this;
        }
        public int Count => data.Count;
        public GraphicsBuffereWrapper<T> Clear() {
            data.Clear();
            dataChanged.Invalidate();
            return this;
        }
        #endregion

        #region buffer
        public GraphicsBuffereWrapper<T> UploadToGPU() {
            dataChanged.Validate();
            if (buf != null && data.Count > 0)
                buf.SetData(data);
            return this;
        }
        #endregion

		#region member
        protected void Notify() => Changed?.Invoke(this);
        #endregion

        #region static
        public static implicit operator GraphicsBuffer(GraphicsBuffereWrapper<T> h) => h.Value;
        #endregion
    }
}
