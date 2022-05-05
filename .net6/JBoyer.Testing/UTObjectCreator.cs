using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace JBoyer.Testing
{

    public class UTObjectCreator
    {

        #region Public Methods

        /// <summary>
        /// Uses reflection to create an instance of System.Data.SqlClient.SqlException with the specified error code number.
        /// </summary>
        /// <param name="errorCode">The number that identifies the type of error.</param>
        /// <returns>A SqlEception with the specified ErrorCode</returns>
        public static SqlException NewSqlException(int errorCode) => newSqlExceptionLogic(errorCode, "This is a Unit Test Generated SqlException.");

        /// <summary>
        /// Uses reflection to create an instance of System.Data.SqlClient.SqlException with the specified error code number and message.
        /// </summary>
        /// <param name="errorCode">The number that identifies the type of error.</param>
        /// <param name="message">The message that is applied to the SqlException</param>
        /// <returns>A SqlEception with the specified ErrorCode and message</returns>
        public static SqlException NewSqlException(int errorCode, string message) => newSqlExceptionLogic(errorCode, message);

        #endregion

        #region Private Methods

        [ExcludeFromCodeCoverage]
        private static T construct<T>(params object[] args)
        {
            Type constructorType = typeof(T);

            var construtors = constructorType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).ToArray();

            ConstructorInfo? construtorInfoUse = null;
            foreach (var construtor in construtors)
            {
                var parameters = construtor.GetParameters().Select(p => p.ParameterType).ToArray();
                if (parameters.Length != args.Length)
                {
                    continue;
                }

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

            if (construtorInfoUse == null)
            {
                throw new InvalidOperationException("Could not find a valid constructor!");
            }

            return (T)construtorInfoUse.Invoke(args);
        }

        [ExcludeFromCodeCoverage]
        private static SqlException newSqlExceptionLogic(int errorCode, string message)
        {
            SqlErrorCollection collection = construct<SqlErrorCollection>();
            SqlError error = construct<SqlError>(errorCode, (byte)2, (byte)3, "server name", message, "proc", 100, new Exception(message));

            typeof(SqlErrorCollection)
                .GetMethod("Add", BindingFlags.NonPublic | BindingFlags.Instance)?
                .Invoke(collection, new object[] { error });

            SqlException exception = typeof(SqlException)
                .GetMethod(
                    "CreateException",
                    BindingFlags.NonPublic | BindingFlags.Static,
                    null,
                    CallingConventions.ExplicitThis,
                    new[] { typeof(SqlErrorCollection), typeof(string) },
                    new ParameterModifier[] { }
                )?
                .Invoke(null, new object[] { collection, "7.0.0" }) as SqlException ?? throw new InvalidOperationException("Test");

            typeof(SqlException)
                .GetProperty("HResult")?
                .SetMethod?
                .Invoke(exception, new object[] { errorCode });

            return exception;
        }

        #endregion

    }


}
