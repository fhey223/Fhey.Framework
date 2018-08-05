using Fhey.Framework.Uility.Expression.Interface;
using Microsoft.JScript.Vsa;

namespace Com.Fhey.Framework.Uility.Expressions
{
    public class JScriptVsaEngineStringExpressionEvaluator : IStringExpressionEvaluator
    {
        protected static VsaEngine _JSEngine;
        protected readonly static object _LockObject;

        static JScriptVsaEngineStringExpressionEvaluator()
        {
            _JSEngine = VsaEngine.CreateEngine();
            _LockObject = new object();
        }

        public T Eval<T>(string expression)
        {
            T result = default(T);
            lock (_LockObject)
            {
                object value = Microsoft.JScript.Eval.JScriptEvaluate(expression, _JSEngine);
                result = (T)System.Convert.ChangeType(value, typeof(T));
            }
            return result;
        }
    }
}
