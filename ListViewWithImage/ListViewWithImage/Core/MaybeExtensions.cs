using System;

namespace Runtime.Kernel.Core
{
    public static class MaybeExtensions
    {
        #region Public Methods and Operators

        public static Maybe<TOutput> Convert<TInput, TOutput>(this Maybe<TInput> input,
                                                              Func<TInput, TOutput> func) 
            where TOutput : class
        {
            return !input.HasValue
                       ? Maybe<TOutput>.Nothing
                       : func(input.Value).ToMaybe();
        }

        public static Maybe<TInput> Execute<TInput>(this Maybe<TInput> input,
                                                    Action<TInput> action)
        {
            if(input.HasValue)
            {
                action(input.Value);
            }

            return input;
        }

        public static Maybe<TInput> If<TInput>(this Maybe<TInput> input,
                                               Func<TInput, bool> evaluator) 
            where TInput : class
        {
            return input.HasValue
                       ? (evaluator(input.Value)
                              ? input
                              : Maybe<TInput>.Nothing)
                       : input;
        }

        public static TResult Return<TInput, TResult>(this Maybe<TInput> input,
                                                      Func<TInput, TResult> evaluator,
                                                      TResult failureValue) 
            where TInput : class
        {
            return !input.HasValue
                       ? failureValue
                       : evaluator(input.Value);
        }

        public static Maybe<TResult> Select<TInput, TResult>(this Maybe<TInput> input,
                                                             Func<TInput, TResult> evaluator) 
            where TResult : class 
            where TInput : class
        {
            return input.HasValue
                       ? evaluator(input.Value).ToMaybe()
                       : Maybe<TResult>.Nothing;
        }

        public static Maybe<T> ToMaybe<T>(this T value) where T : class 
        {
            return value == null
                       ? Maybe<T>.Nothing
                       : new Maybe<T>(value);
        }

        public static Maybe<TInput> Unless<TInput>(this Maybe<TInput> input,
                                                   Func<TInput, bool> evaluator)
            where TInput : class
        {
            return input.HasValue
                       ? (evaluator(input.Value)
                              ? Maybe<TInput>.Nothing
                              : input)
                       : input;
        }

        //public static Maybe<TResult> With<TInput, TResult>(this Maybe<TInput> input,
        //                                                   Func<TInput, TResult> evaluator) 
        //    where TResult : class 
        //    where TInput : class
        //{
        //    return input.HasValue
        //               ? evaluator(input.Value).ToMaybe() 
        //               : Maybe<TResult>.Nothing;
        //}

        #endregion
    }
}