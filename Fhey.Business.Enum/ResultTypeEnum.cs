using System.ComponentModel;

namespace Fhey.Business.Enum
{
    public enum ResultTypeEnum
    {
        [Description("成功")]
        Success = 0,

        [Description("服务方法异常")]
        ServiceException = 1,

        [Description("Api参数错误")]
        ParamError = 2,

        [Description("Json对象反序列化失败")]
        JsonDeserializeFailed = 3,

        [Description("验证签名失败")]
        ValidateSignFailed = 4,

        [Description("网络连接失败")]
        HttpError = 5
    }
}
