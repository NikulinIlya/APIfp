using System;

public class Class1
{
    abstract class Expression<TArg, TResult>
    {
        public static Expression<TArg, TResult> Value(TResult constant)
        {
            return new ValueExpression<TArg, TResult>(constant);
        }

        public static IfThenElseExpression<TArg, TResult> If(Func<TArg, bool> cond)
        {
            return new IfThenElseExpression<TArg, TResult>(cond);
        }
    }

    class ValueExpression<TArg, TResult> : Expression<TArg, TResult>
    {
        TResult val;
        public ValueExpression(TResult value) { val = value; }
        public TResult Evaluate(TArg arg)
        {
            return this.val;
        }
    }

    class IfThenElseExpression<TArg, TResult> : Expression<TArg, TResult>
    {
        public IfThenElseExpression(Func<TArg, bool> cond) { }

        public ThenElseExpression<TArg, TResult> Then(Func<TArg, bool> thenner)
        {
            return new ThenElseExpression<TArg, TResult>(this, thenner);
        }

    }

    class ThenElseExpression<TArg, TResult> : Expression<TArg, TResult>
    {
        public ThenElseExpression(IfThenElseExpression<TArg, TResult> iffer, Func<TArg, bool> elser) { }

        public ElseExpression<TArg, TResult> Else(Func<TArg, TResult> elser)
        {
            return new ElseExpression<TArg, TResult>(this, elser);
        }

    }

    class ElseExpression<TArg, TResult> : Expression<TArg, TResult>
    {
        public ElseExpression(ThenElseExpression<TArg, TResult> ifthenner, Func<TArg, TResult> elser) { }
        /*public Evaluate(TArg arg)
        {
            var condValue = this.cond(arg);
            if (condValue) {
                return this.thenner();
            } else {
                return this.elser();
            }
        }*/

        /*public static IfThen<T> If(Func<dynamic, Boolean> cond)
{
    return new IfThen<T>(cond);
}*/
    }
    public Class1()
	{
	}
}
