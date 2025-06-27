using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentTableService.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class UserNotAuthenticatedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotAuthenticatedException"/> class.
        /// </summary>
        public UserNotAuthenticatedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotAuthenticatedException"/> class.
        /// </summary>
        /// <param name="message">Mensaje que describe el error.</param>
        public UserNotAuthenticatedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotAuthenticatedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public UserNotAuthenticatedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
