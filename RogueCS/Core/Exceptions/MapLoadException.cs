using System.Runtime.Serialization;

namespace RogueCS.Core.Exceptions
{
    [Serializable]
    internal class MapLoadException : Exception
    {
        private object value1;
        private object value2;
        private object value3;
        private object value4;
        private object value5;
        private object value6;

        public MapLoadException()
        {
        }

        public MapLoadException(string? message) : base(message)
        {
        }

        public MapLoadException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public MapLoadException(object value1, object value2, object value3, object value4, object value5, object value6)
        {
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
            this.value4 = value4;
            this.value5 = value5;
            this.value6 = value6;
        }

        protected MapLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}