using System;
using System.Collections.Immutable;


namespace Shared.ROP
{
    /// <summary>
    /// Throw an error will mean to get the result if there is no errors or throw all the errors as exceptions
    /// </summary>
    public static class Result_Throw
    {
        public static T Throw<T>(this Result<T> r)
        {
            if (!r.Success)
            {
                r.Errors.Throw();
            }

            return r.Value;
        }

        public static void Throw(this ImmutableArray<string> errors)
        {
            if (errors.Length > 0)
            {
                throw new Exception(string.Join(",", errors));
            }
        }
    }
}
