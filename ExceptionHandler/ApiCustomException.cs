using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Armazenagem3L_API.ExceptionHandler {
    [Serializable]
    public class ApiCustomException : Exception {

        public ApiCustomException() {

        }
        public ApiCustomException(string name): base(name) {

        }

        protected ApiCustomException(SerializationInfo info, StreamingContext context) : base(info, context) {

        }
    }
}
