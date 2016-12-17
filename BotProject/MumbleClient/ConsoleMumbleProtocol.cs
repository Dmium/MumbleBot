using System;
using System.Collections.Generic;
using System.Linq;
using MumbleSharp;
using MumbleSharp.Audio;
using MumbleSharp.Audio.Codecs;
using MumbleSharp.Model;
using MumbleSharp.Packets;
using NAudio.Wave;
using System.Diagnostics;
namespace MumbleClient
{
    /// <summary>
    /// A test mumble protocol. Currently just prints the name of whoever is speaking, as well as printing messages it receives
    /// </summary>
    public class ConsoleMumbleProtocol
        : BasicMumbleProtocol
    {
        public Process p = null;
        public string song;
        //readonly Dictionary<User, AudioPlayer> _players = new Dictionary<User, AudioPlayer>(); 

        public override void EncodedVoice(byte[] data, uint userId, long sequence, IVoiceCodec codec, SpeechTarget target)
        {
            User user = Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
                //Console.WriteLine(user.Name + " is speaking. Seq" + sequence);

                base.EncodedVoice(data, userId, sequence, codec, target);
        }

        protected override void UserJoined(User user)
        {
            base.UserJoined(user);
            if (user.Name != null)
            {
                LocalUser.SendMessage("Hello " + user.Name);
            }
            //_players.Add(user, new AudioPlayer(user.Voice));
        }

        protected override void UserLeft(User user)
        {
            base.UserLeft(user);

            //_players.Remove(user);
        }

        public override void ServerConfig(ServerConfig serverConfig)
        {
            base.ServerConfig(serverConfig);

            Console.WriteLine(serverConfig.WelcomeText);
        }

        protected override void ChannelMessageReceived(ChannelMessage message)
        {
            if (message.Channel.Equals(LocalUser.Channel))
            {
                string tempmessagestring;
                string[] tempmessagestring2;
                tempmessagestring = message.Text;
                tempmessagestring2 = tempmessagestring.Split(' ');
                if (tempmessagestring2[0].StartsWith("!"))
                {
                    try
                    {
                        switch (tempmessagestring2[0].ToUpper())
                        {
                            case "!SONG":
                                //foreach (Process process in Process.GetProcessesByName("chrome"))
                                //{
                                //    LocalUser.SendMessage(process.MainWindowTitle);
                                //}
                                LocalUser.SendMessage(p.MainWindowTitle);
                                break;
                            case "!PLAY":
                                foreach (Process process in Process.GetProcessesByName("firefox"))
                                {
                                    process.Kill();
                                }
                                //if (p != null)
                                //{
                                //    p.Kill();
                                //    Console.WriteLine("P Killed");
                                //}
                                p = System.Diagnostics.Process.Start("firefox.exe", "https://www.youtube.com/" + tempmessagestring2[1]);
                                Console.WriteLine(p.Id);
                                LocalUser.SendMessage("Loading song...");
                                break;
                            case "!ROLL":
                                Random dice = new Random();
                                LocalUser.SendMessage(Convert.ToString(dice.Next(1, Convert.ToInt32(tempmessagestring2[1]))));
                                break;
                            case "!GAME":
                                LocalUser.SendMessage("No.");
                                break;
                            default:
                                LocalUser.SendMessage("Invalid command");
                                break;
                        }
                    }
                    catch
                    {
                        LocalUser.SendMessage("Execution error, most likely caused by incorrect input");
                    }
                }
                else
                {
                    Console.WriteLine(string.Format("{0} (channel message): {1}", message.Sender.Name, message.Text));
                    LocalUser.SendMessage(message.Sender.Name + " said: " + message.Text);
                    base.ChannelMessageReceived(message);
                }
            }
        }

        protected override void PersonalMessageReceived(PersonalMessage message)
        {
            Console.WriteLine(string.Format("{0} (personal message): {1}", message.Sender.Name, message.Text));
            base.PersonalMessageReceived(message);
        }

        //private class AudioPlayer
        //{
        //    private readonly WaveOut _playbackDevice = new WaveOut();

        //    public AudioPlayer(IWaveProvider provider)
        //    {
        //        _playbackDevice.Init(provider);
        //        //_playbackDevice.Play();

        //        _playbackDevice.PlaybackStopped += (sender, args) => Console.WriteLine("Playback stopped: " + args.Exception);
        //    }
        //}
    }
}
