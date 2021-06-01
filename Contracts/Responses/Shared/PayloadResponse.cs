

using System.Collections.Generic;

namespace DesafioBack.Contracts.Responses.Shared
{
    public class PayloadResponse<E> where E : IResponse
    {
        public List<E> Payload { get; set; }

        public PayloadResponse(List<E> payload)
        {
            Payload = payload;
        }
    }
}