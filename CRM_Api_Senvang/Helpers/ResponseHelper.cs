using System.Dynamic;

namespace CRM_Api_Senvang.Helpers
{
    public class ResponseHelper
    {
        public int ErrCode { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public ResponseHelper() { }

        public ResponseHelper(int errCode, string? message, object? data)
        {
            ErrCode = errCode;
            Message = message;
            Data = data;
        }


        public IDictionary<string, object> HandleResponse()
        {
            dynamic obj = new ExpandoObject();
            obj.Error = this.ErrCode;
            obj.Message = this.Message;
            obj.Data = this.Data ?? null;
            IDictionary<string, object> result = (IDictionary<string, object>)obj;
            return result;
        }
    }
}
