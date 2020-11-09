using Microsoft.AspNetCore.Http;

namespace TokanPages.BackEnd.Helpers.Statics
{

    public static class IpAddress
    {

        /// <summary>
        /// Simple wrapper for returning IP address (IPv4).
        /// </summary>
        /// <param name="AHttpContext"></param>
        /// <returns></returns>
        public static string Get(HttpContext AHttpContext)
        {
            try
            {
                var LIpAddress = AHttpContext.Connection.RemoteIpAddress.ToString();
                if (LIpAddress == "::1") { LIpAddress = "127.0.0.1"; }
                return LIpAddress;
            }
            catch
            {
                return "0.0.0.0";
            }

        }


    }

}