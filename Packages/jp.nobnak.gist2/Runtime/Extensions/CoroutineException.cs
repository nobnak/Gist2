using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Extensions.CoroutineExt {

    public static class CoroutineExtension {

        public static Token StartCoroutineWithToken(this MonoBehaviour mono, IEnumerator routine) {
            var token = new Token();
            token.co = mono.StartCoroutine(WrapRoutine(token, routine));
            return token;

            static IEnumerator WrapRoutine(Token token, IEnumerator routine) {
                yield return null;

                while (true) {
                    object o = null;
                    System.Exception ex = null;

                    try {
                        if (!routine.MoveNext())
                            break;
                        o = routine.Current;
                    } catch (System.Exception e) {
                        ex = e;
                        token.NotifyOnException(e);
                    }

                    if (ex != null) {
                        yield return ex;
                        yield break;
                    }
                    yield return o;
                }

                token.NotifyOnComplete();
            }
        }

        public class Token {
            public event System.Action<System.Exception> OnException;
            public event System.Action OnComplete;

            public Coroutine co;

            public Token RegisterOnException(System.Action<System.Exception> action) {
                OnException += action;
                return this;
            }
            public Token RegisterOnComplete(System.Action action) {
                OnComplete += action;
                return this;
            }

            public Token NotifyOnException(System.Exception e) {
                OnException?.Invoke(e);
                return this;
            }
            public Token NotifyOnComplete() {
                OnComplete?.Invoke();
                return this;
            }
        }
    }
}