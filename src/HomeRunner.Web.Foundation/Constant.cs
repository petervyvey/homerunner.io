
namespace HomeRunner.Web.Foundation
{
    public static class Constant
    {
        public static class IdentityServer
        {
            public const string BASE_ADDRESS = "http://dev.homerunner.io/authorization/core";
        }

        public static class Endpoint
        {
            public const string OPENID_CONNECT_USER_INFO_ENDPOINT = Constant.IdentityServer.BASE_ADDRESS + "/connect/userinfo";

            public const string OPENID_CONNECT_TOKEN_ENDPOINT = Constant.IdentityServer.BASE_ADDRESS + "/connect/token";
        }
    }


}
