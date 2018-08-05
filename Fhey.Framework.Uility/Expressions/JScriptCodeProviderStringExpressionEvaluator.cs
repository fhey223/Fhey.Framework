using System;
using System.CodeDom.Compiler;
using System.Reflection;
using Fhey.Framework.Uility.Expression.Interface;
using Microsoft.JScript;

namespace Fhey.Framework.Uility.Expressions
{
    public class JScriptCodeProviderStringExpressionEvaluator : IStringExpressionEvaluator
    {
        public static readonly string _JscriptSource = @"class Evaluator { public function Eval(expr : String) : String {  return eval(expr); } }";

        

        protected static readonly Type _EvalType;
        protected static readonly object _EvalObject;

        static JScriptCodeProviderStringExpressionEvaluator()
        {
            CodeDomProvider provider = new JScriptCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateInMemory = true;
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, _JscriptSource);
            Assembly assembly = results.CompiledAssembly;
            _EvalType = assembly.GetType("Evaluator");
            _EvalObject = Activator.CreateInstance(_EvalType);
        }

        public T Eval<T>(string expression)
        {
            object value = _EvalType.InvokeMember("Eval", BindingFlags.InvokeMethod, null, _EvalObject, new object[] { expression });
            return (T)System.Convert.ChangeType(value, typeof(T));
        }
    }
}
