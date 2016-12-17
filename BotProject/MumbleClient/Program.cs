using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MumbleSharp;
using MumbleSharp.Model;
using MumbleSharp.Packets;
namespace MumbleClient
{
    internal class Program
    {
        public string[] songs = new string[5];
        private static void Main(string[] args)
        {
            string addr, name, pass;
            int port;
            FileInfo serverConfigFile = new FileInfo(Environment.CurrentDirectory + "\\server.txt");
            if (serverConfigFile.Exists)
            {
                using (StreamReader reader = new StreamReader(serverConfigFile.OpenRead()))
                {
                    addr = reader.ReadLine();
                    port = int.Parse(reader.ReadLine());
                    name = reader.ReadLine();
                    pass = reader.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Enter server address:");
                addr = Console.ReadLine();
                Console.WriteLine("Enter server port (leave blank for default (64738)):");
                string line = Console.ReadLine();
                if (line == "")
                {
                    port = 64738;
                }
                else
                {
                    port = int.Parse(line);
                }
                Console.WriteLine("Enter name:");
                name = Console.ReadLine();
                Console.WriteLine("Enter password:");
                pass = Console.ReadLine();

                using (StreamWriter writer = new StreamWriter(serverConfigFile.OpenWrite()))
                {
                    writer.WriteLine(addr);
                    writer.WriteLine(port);
                    writer.WriteLine(name);
                    writer.WriteLine(pass);
                }
            }

            ConsoleMumbleProtocol protocol = new ConsoleMumbleProtocol();
            MumbleConnection connection = new MumbleConnection(new IPEndPoint(Dns.GetHostAddresses(addr).First(a => a.AddressFamily == AddressFamily.InterNetwork), port), protocol);
            connection.Connect(name, pass, new string[0], addr);
            //Channel testchannel = new Channel(protocol.LocalUser.,);
            Thread t = new Thread(a => UpdateLoop(connection)) {IsBackground = true};
            //Thread tu = new Thread(a => UpdateLoopud(connection)) { IsBackground = true };
            t.Start();
            //tu.Start();

//var r = new MicrophoneRecorder(protocol);

            //When localuser is set it means we're really connected
            while (!protocol.ReceivedServerSync)
            {
            }
            Console.WriteLine("Connected as " + protocol.LocalUser.Id);
            bool tes;
            tes = false;
            while (tes ==false){
                //Console.Clear();

                DrawChannel("", protocol.Channels.ToArray(), protocol.Users.ToArray(), protocol.RootChannel);
                string[] inp = new string[1];
                inp[0] = Console.ReadLine();
                protocol.LocalUser.SendMessage(inp);
            }
            Console.ReadLine();
        }

        private static void DrawChannel(string indent, IEnumerable<Channel> channels, IEnumerable<User> users, Channel c)
        {
            Console.WriteLine();
            Console.WriteLine(indent + c.Name + (c.Temporary ? "(temp)" : ""));

            foreach (var user in users.Where(u => u.Channel.Equals(c)))
            {
                if (string.IsNullOrWhiteSpace(user.Comment))
                    Console.WriteLine(indent + "-> " + user.Name);
                else
                    Console.WriteLine(indent + "-> " + user.Name);
                    //Console.WriteLine(indent + "-> " + user.Name + " (" + user.Comment + ")");
            }
            foreach (var channel in channels.Where(ch => ch.Parent == c.Id && ch.Parent != ch.Id))
                DrawChannel(indent + "\t", channels, users, channel);
        }

        private static void UpdateLoop(MumbleConnection connection)
        {
            while (connection.State != ConnectionStates.Disconnected)
            {
                connection.Process();
                System.Threading.Thread.Sleep(1);
            }
        }
        //private static void UpdateLoopud(MumbleConnection connection)
        //{
        //    while (connection.State != ConnectionStates.Disconnected)
        //    {
        //        connection.Processu();
        //        System.Threading.Thread.Sleep(100);
        //    }
        //}
    }
    //static class test
    //{
    //    public static void SendMessage(this IEnumerable<Channel> channels, string[] message, bool recursive)
    //    {
    //        // It's conceivable that this group could include channels from multiple different server connections
    //        // group by server
    //        Console.WriteLine("botbotbot");
    //        foreach (var group in channels.GroupBy(a => a.Owner))
    //        {
    //            var owner = group.First().Owner;

    //            var msg = new TextMessage
    //            {
    //                Actor = owner.LocalUser.Id,
    //                Message = message,
    //            };

    //            if (recursive)
    //                msg.TreeId = group.Select(c => c.Id).ToArray();
    //            else
    //                msg.ChannelId = group.Select(c => c.Id).ToArray();

    //            owner.Connection.SendControl<TextMessage>(PacketType.TextMessage, msg);
    //        }
    //    }
    //}
}
