using System;
using System.Runtime.Serialization;

namespace convert_to_png
{ 
    public class BlenderException : Exception
    {
        public BlenderException()
        {
        }

        public BlenderException(string message) : base(message)
        {
        }

        protected BlenderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BlenderException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

