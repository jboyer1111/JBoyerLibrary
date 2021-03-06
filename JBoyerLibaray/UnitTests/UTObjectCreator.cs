﻿using System;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace JBoyerLibaray.UnitTests
{

    public class UTObjectCreator
    {

        #region Public Methods

        /// <summary>
        /// Uses reflection to create an instance of System.Data.SqlClient.SqlException with the specified error code number.
        /// </summary>
        /// <param name="errorCode">The number that identifies the type of error.</param>
        /// <returns>A SqlEception with the specified ErrorCode</returns>
        public static SqlException NewSqlException(int errorCode)
        {
            return NewSqlException(errorCode, "This is a Unit Test Generated SqlException.");
        }

        /// <summary>
        /// Uses reflection to create an instance of System.Data.SqlClient.SqlException with the specified error code number and message.
        /// </summary>
        /// <param name="errorCode">The number that identifies the type of error.</param>
        /// <param name="message">The message that is applied to the SqlException</param>
        /// <returns>A SqlEception with the specified ErrorCode and message</returns>
        public static SqlException NewSqlException(int errorCode, string message)
        {
            SqlErrorCollection collection = construct<SqlErrorCollection>();
            SqlError error = construct<SqlError>(errorCode, (byte)2, (byte)3, "server name", message, "proc", 100);

            typeof(SqlErrorCollection)
                .GetMethod("Add", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(collection, new object[] { error });

            SqlException exception = typeof(SqlException)
                .GetMethod(
                    "CreateException",
                    BindingFlags.NonPublic | BindingFlags.Static,
                    null,
                    CallingConventions.ExplicitThis,
                    new[] { typeof(SqlErrorCollection), typeof(string) },
                    new ParameterModifier[] { }
                )
                .Invoke(null, new object[] { collection, "7.0.0" }) as SqlException;

            typeof(SqlException)
                .GetProperty("HResult")
                .SetMethod
                .Invoke(exception, new object[] { errorCode });

            return exception;
        }

        #endregion

        #region Private Methods

        [ExcludeFromCodeCoverage]
        private static T construct<T>(params object[] args)
        {
            Type constructorType = typeof(T);

            var construtors = constructorType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(c => c.GetParameters().Length == args.Length)
                .ToArray();

            ConstructorInfo construtorInfoUse = null;
            foreach (var construtor in construtors)
            {
                var parameters = construtor.GetParameters().Select(p => p.ParameterType).ToArray();
                bool canuse = true;
                for (int i = 0; i < parameters.Length; i++)
                {
                    var canAssign = parameters[i].IsAssignableFrom(args[i].GetType());
                    if (!canAssign)
                    {
                        canuse = false;
                        break;
                    }
                }

                if (canuse)
                {
                    construtorInfoUse = construtor;
                    break;
                }
            }

            return (T)construtorInfoUse.Invoke(args);
        }

        #endregion

    }


}
