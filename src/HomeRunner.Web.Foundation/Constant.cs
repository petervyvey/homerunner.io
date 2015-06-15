
namespace HomeRunner.Web.Foundation
{
    public static class Constant
    {
        public static class IdentityServer
        {
            public const string BASE_ADDRESS = "https://localhost:44333/core";
        }

        public static class Endpoint
        {
            public const string USER_INFO = Constant.IdentityServer.BASE_ADDRESS + "/connect/userinfo";

            public const string TOKEN = Constant.IdentityServer.BASE_ADDRESS + "/connect/token";
        }
    }


}
