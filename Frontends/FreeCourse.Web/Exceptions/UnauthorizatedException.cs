using System.Runtime.Serialization;

namespace FreeCourse.Web.Exceptions
{
    public class UnauthorizatedException : Exception
    {
        public UnauthorizatedException()
        {
        }

        public UnauthorizatedException(string? message) : base(message)
        {
        }

        public UnauthorizatedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnauthorizatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
