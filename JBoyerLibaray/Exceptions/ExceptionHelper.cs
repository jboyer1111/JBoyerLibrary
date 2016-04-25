using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace JBoyerLibaray.Exceptions
{
    public static class ExceptionHelper
    {
        public static ArgumentNullException CreateArgumentNullException<T>(Expression<Func<T>> expressionDelegate)
        {
            return new ArgumentNullException(GetMemberName(expressionDelegate));
        }

        public static ArgumentNullException CreateArgumentNullException<T>(Expression<Func<T>> expressionDelegate, string message)
        {
            return new ArgumentNullException(GetMemberName(expressionDelegate), message);
        }

        public static ArgumentException CreateArgumentException<T>(Expression<Func<T>> expressionDelegate, string message)
        {
            return new ArgumentException(message ?? "", GetMemberName(expressionDelegate));
        }

        public static ArgumentOutOfRangeException CreateArgumentOutOfRangeException<T>(Expression<Func<T>> expressionDelegate, string message, object actualValue)
        {
            return new ArgumentOutOfRangeException(message ?? "", actualValue, GetMemberName(expressionDelegate));
        }

        public static string GetMemberName<T>(Expression<Func<T>> expressionDelegate)
        {
            MemberExpression memberExpression = (MemberExpression)expressionDelegate.Body;
            return memberExpression.Member.Name;
        }
    }
}