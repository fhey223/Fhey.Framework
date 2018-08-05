namespace Fhey.Framework.Uility.Expressions.Interface
{
    public interface IExpressionEvaluator<TExpression>
    {
        TResult Eval<TResult>(TExpression expression);
    }
}
