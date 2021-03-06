﻿using System;
using System.Linq;
using System.Linq.Expressions;

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
            return new ArgumentOutOfRangeException(GetMemberName(expressionDelegate), actualValue, message ?? "");
        }

        public static ArgumentInvalidException CreateArgumentInvalidException<T>(Expression<Func<T>> expressionDelegate, string message, object actualValue)
        {
            return new ArgumentInvalidException(GetMemberName(expressionDelegate), actualValue, message ?? "");
        }

        public static string GetMemberName<T>(Expression<Func<T>> expressionDelegate)
        {
            MemberExpression memberExpression = (MemberExpression)expressionDelegate.Body;
            return memberExpression.Member.Name;
        }
    }
}