public struct Address
{
    public int Port { get; set; }
    public string Server { get; set; }

    public Address(int port, string server)
    {
        Port = port;
        Server = server;
    }
}