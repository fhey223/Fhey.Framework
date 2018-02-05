using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;
using Fhey.Framework.Uility.Expression.Interface;
using Microsoft.CSharp;

namespace Fhey.Framework.Uility.Expression
{
    public class CSharpStringExpressionEvaluator 
    {

        public T Eval<T>(string nameSpace, string className,string methodName, string expression)
        {
            T result = default(T);
            //string nameSpace = "Com.Centaline.Framework.QuickQuery";
            //string className = "QuickQueryCSharpStringEvaluator";
            //string methodName = "GetValue";
            CSharpCodeProvider comp = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            parameters.ReferencedAssemblies.AddRange(new[] { "system.dll", "system.data.dll", "system.xml.dll" });
            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = true;

            string code = string.Format(@"using System; 
namespace {0} {{ 
	public class {1} {{ 
		public {2} {3}() {{ 
		return ({4});
		}}
	}}
}}", nameSpace, className, typeof(T).Name,methodName,expression);

            CompilerResults cr = comp.CompileAssemblyFromSource(parameters, code);
            if (cr.Errors.HasErrors)
            {
                StringBuilder error = new StringBuilder();
                error.Append("编译有错误的表达式: ");
                foreach (CompilerError err in cr.Errors)
                {
                    error.AppendFormat("{0}\n", err.ErrorText);
                }
                throw new System.Exception("编译错误: " + error.ToString());
            }
            object compiled = cr.CompiledAssembly.CreateInstance(string.Format("{0}.{1}", nameSpace, className));
            if (null != compiled)
            {
                MethodInfo mi = compiled.GetType().GetMethod(methodName);
                result = (T)mi.Invoke(compiled, null);
            }
            return result;
        }
    }
}
