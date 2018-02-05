namespace Fhey.Framework.Uility.Expression.Interface
{
    public interface IExpressionEvaluator<TExpression>
    {
        TResult Eval<TResult>(TExpression expression);
    }
}
