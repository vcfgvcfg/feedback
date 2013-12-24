using System.Web;

namespace LiteReg.Web.Infrastructure
{
    /// <summary>
    /// check ip address is internal 
    /// http://en.wikipedia.org/wiki/Private_network
    /// http://khlo.co.uk/index.php/25-php-determining-if-a-serverdomainip-is
    /// </summary>
    public class IPAddressChecker
    {
        /// <summary>
        /// Get the client ip address based on the request.
        /// TODO:Do we need to find the real ip behind proxy?
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetClientIPAddress(HttpRequestBase request)
        {
            var ip = string.Empty;
            if (request != null)
            {
                if (request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    var split = request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',');
                    if (split.Length > 0 && split[0] != null)
                    {
                        ip = split[0].Trim();
                    }
                }
                else
                {
                    ip = request.ServerVariables["REMOTE_ADDR"];
                }
            }
            //for location service testing
            #if DEBUG
                ip = "69.194.141.69";
            #endif

            return ip;
        }

        /// <summary>
        /// Check if the ip address is internal or public
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static bool IPAddressIsInternal(string ipAddress)
        {
            var value = ConvertIPToLong(ipAddress);

            if ((value >= 167772160 && value <= 184549375) ||
                (value >= -1408237568 && value <= -1407188993) ||
                (value >= -1062731776 && value <= -1062666241) || (value >= 2130706432
                                                                   && value <= 2147483647) || value == -1)
            {

                return true;

            }
            return false;
            // 167772160 - 10.0.0.0

            // 184549375 - 10.255.255.255

            //

            // -1408237568 - 172.16.0.0

            // -1407188993 - 172.31.255.255

            //

            // -1062731776 - 192.168.0.0

            // -1062666241 - 192.168.255.255

            //

            // -1442971648 - 169.254.0.0

            // -1442906113 - 169.254.255.255

            //

            // 2130706432 - 127.0.0.0

            // 2147483647 - 127.255.255.255 (32 bit integer limit!!!)

            //

            // -1 is also b0rked
        }

        private static long ConvertIPToLong(string ipAddress)
        {
            System.Net.IPAddress ip;

            if (System.Net.IPAddress.TryParse(ipAddress, out ip))
            {
                byte[] bytes = ip.GetAddressBytes();

                return ((bytes[0] << 24) | (bytes[1] << 16) |
                              (bytes[2] << 8) | bytes[3]);
            }
            else
                return 0;
        }
    }
}