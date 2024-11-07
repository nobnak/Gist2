using Gist2.Deferred;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Gist2.Adapter {

    public class GraphicsList<T> : System.IDisposable, IList<T> where T : struct {

        protected Validator assure = new Validator();
        protected List<T> list;
        protected GraphicsBuffer buf;

        public GraphicsList(System.Func<int, GraphicsBuffer> generater) {
            this.list = new List<T>();

            assure.OnValidate += () => {
                if (buf != null && buf.count != list.Count) ReleaseBuffer();
                if (buf == null && list.Count > 0) buf = generater(list.Count);
                buf?.SetData(list);
            };
        }

        #region interface

        #region IDisposable
        public void Dispose() {
            ReleaseBuffer();
        }
        #endregion

        #region IList
        public T this[int index] { get => list[index]; set { assure.Invalidate(); list[index] = value; } }
        public void Add(T item) { assure.Invalidate(); list.Add(item); }
        public void Clear() { assure.Invalidate(); list.Clear(); }
        public void Insert(int index, T item) { assure.Invalidate(); list.Insert(index, item); }
        public bool Remove(T item) { assure.Invalidate(); return list.Remove(item); }
        public void RemoveAt(int index) { assure.Invalidate(); list.RemoveAt(index); }

        public int Count => list.Count;
        public bool IsReadOnly => false;
        public bool Contains(T item) => list.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);
        public IEnumerator<T> GetEnumerator() => list.GetEnumerator();
        public int IndexOf(T item) => list.IndexOf(item);
        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();
        #endregion

        #endregion

        #region static
        public static implicit operator GraphicsBuffer(GraphicsList<T> list) {
            list.assure.Validate();
            return list.buf;
        }
        #endregion

        #region member
        private void ReleaseBuffer() {
            buf?.Dispose();
            buf = null;
        }
        #endregion
    }
}
