using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Core.Extensions.NetRelated;

public static class NetworkInterfaceExt
{
    public static IPAddress? Ipv4Address(this NetworkInterface value)
    {
        return value.AddressByFamily(AddressFamily.InterNetworkV6);
    }

    public static IPAddress? IpV6Address(this NetworkInterface value)
    {
        return value.AddressByFamily(AddressFamily.InterNetworkV6);
    }

    public static IPAddress? AddressByFamily(this NetworkInterface value, AddressFamily addressFamily)
    {
        return value
            .GetIPProperties()
            .UnicastAddresses
            .FirstOrDefault(i=>i.Address.AddressFamily == addressFamily)?.Address;
    }
}