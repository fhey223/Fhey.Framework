using Fhey.Business.Enum;
using Fhey.Framework.Uility;

namespace Fhey.Business.Model
{
    public class ResponseBase
    {
        public ResultTypeEnum Code { get; set; }

        public string Message { get; set; }

        public ResponseBase()
        {
        }

        public static ResponseBase Create(ResultTypeEnum code, string message = "")
        {
            var obj = new ResponseBase();
            var description = new EnumUility().GetDescription(code);
            obj.Message = string.IsNullOrEmpty(message)
                ? description : $"{description},错误原因：{message}";
            obj.Code = code;
            return obj;
        }

        public ResponseBase(ResultTypeEnum code)
        {
            Code = code;
        }

        public ResponseBase(ResultTypeEnum resultType, string message = "")
        {
            var description = new EnumUility().GetDescription(resultType);
            if (string.IsNullOrEmpty(message))
            {
                Message = description;
            }
            else
            {
                Message = $"{description},错误原因：{message}";
            }
            Code = resultType;
        }
    }

    public class Response : ResponseBase { }

    public class Response<T> : ResponseBase
    {

        public T Data { get; set; }

        public Response()
        {
        }

        public static Response<T> Create(T data)
        {
            var obj = new Response<T>();
            obj.Code = ResultTypeEnum.Success;
            obj.Data = data;
            return obj;
        }

        public Response(T data)
        {
            Code = ResultTypeEnum.Success;
            Data = data;
        }
    }

   
}
