using Gist2.Deferred;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Gist2.Adapter {

    public class ComputeList<T> : System.IDisposable, IList<T> where T : struct {

        protected Assurance assure = new Assurance();
        protected List<T> list;
        protected ComputeBuffer buf;

        public ComputeList(List<T> src) {
            this.list = src;

            assure.Renew += () => {
                if (buf != null && buf.count != list.Count) ReleaseBuffer();
                if (buf == null && list.Count > 0) buf = new ComputeBuffer(list.Count, Marshal.SizeOf<T>());
                buf?.SetData(list);
            };
        }
        public ComputeList() :this(new List<T>()) { }

        #region interface

        #region IDisposable
        public void Dispose() {
            ReleaseBuffer();
        }
        #endregion

        #region IList
        public T this[int index] { get => list[index]; set { assure.Expire(); list[index] = value; } }
        public void Add(T item) { assure.Expire(); list.Add(item); }
        public void Clear() { assure.Expire(); list.Clear(); }
        public void Insert(int index, T item) { assure.Expire(); list.Insert(index, item); }
        public bool Remove(T item) { assure.Expire(); return list.Remove(item); }
        public void RemoveAt(int index) { assure.Expire(); list.RemoveAt(index); }

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
        public static implicit operator ComputeBuffer (ComputeList<T> list) {
            list.assure.Assure();
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
