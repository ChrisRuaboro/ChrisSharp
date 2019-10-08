﻿using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Net;
using Discord.WebSocket;
using System.IO;
using System.Collections;
using System.Net;
using Discord;

namespace ChrisSharpBot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task Ping()
        {
            await ReplyAsync("Pong");
        }
        [Command("CoolerChris")]
        public async Task Chris()
        {
            await ReplyAsync("Chris R. of course");
        }
        [Command("delete")]
        public async Task DeleteMsgs(SocketUser user, int numMsgs = 0)
        {
            int cap = 20;
            try
            {
                if (numMsgs < 1 || numMsgs > cap)
                    await ReplyAsync($"{Context.Message.Author.Mention} Max amount is {cap}.\"");
                else
                {
                    var messages = await Context.Channel.GetMessagesAsync(100).FlattenAsync();
                    int count = 0;
                    foreach (IMessage msg in messages)
                    {
                        if (count < numMsgs)
                        {
                            if (msg.Author.Id == user.Id)
                            {
                                await Context.Channel.DeleteMessageAsync(msg);
                                count++;
                            }
                        }
                        else break;
                    }
                    await ReplyAsync($"{Context.Message.Author.Mention} Deleted {count}/{numMsgs} from {user.Mention}.\"");
                }
            }
            catch
            {
                await ReplyAsync($"{Context.Message.Author.Mention} Please use format \"!delete @example#1337 5\"");
            }
        }
        [Command("delete")]
        public async Task DeleteMsgs(int numMsgs = 0)
        {
            int cap = 100;
            try
            {
                if (numMsgs < 1 || numMsgs > cap)
                    await ReplyAsync($"{Context.Message.Author.Mention} Max amount is {cap}.\"");
                else
                {
                    var messages = await Context.Channel.GetMessagesAsync(numMsgs + 1).FlattenAsync();
                    var channel = Context.Channel as ITextChannel;
                    await channel.DeleteMessagesAsync(messages);
                    await ReplyAsync($"{Context.Message.Author.Mention} Deleted {numMsgs} messages from {Context.Channel.Name}.");
                }
            }
            catch
            {
                await ReplyAsync($"{Context.Message.Author.Mention} Please use format \"!delete 5\"");
            }
        }
        [Command("github")]
        public async Task GitHub(SocketUser user)
        {
            try
            {
                ulong userId = Context.Message.Author.Id;
                ulong targetId = user.Id;
                string path = $"UserData\\{targetId}.txt";
                if (File.Exists(path))
                {
                    string gitHub = File.ReadAllText(path);
                    await ReplyAsync($"{user.Mention}'s GitHub:\n{gitHub}");
                }
                else
                {
                    await ReplyAsync($"{user.Mention} doesn't have a GitHub linked.");
                }

            }
            catch
            {
                await ReplyAsync($"{Context.Message.Author.Mention} Please use format \"!github @example#1337\"");
            }
        }
        [Command("linkgithub")]
        public async Task LinkGitHub(string gitHub = null)
        {
            //https://github.com/discordapp
            ulong userId = Context.Message.Author.Id;
            var user = Context.Message.Author;
            if (gitHub == null || !gitHub.ToLower().Contains("github.com/") || gitHub.Contains("?"))
            {
                await ReplyAsync($"{user.Mention} Please use format \"!linkgithub https://github.com/example\"");
            }
            else
            {
                bool validGitHub = false;
                try
                {
                    WebClient temp = new WebClient();
                    string response = temp.DownloadString(gitHub);
                    if (response.Contains("Block or report user"))
                        validGitHub = true;
                }
                catch
                {
                    validGitHub = false;
                }
                if (validGitHub)
                {
                    string path = $"UserData\\{userId}.txt";
                    File.WriteAllText(path, gitHub);
                    await ReplyAsync($"{user.Mention} GitHub[{gitHub.Split(".com/", StringSplitOptions.RemoveEmptyEntries)[1]}]: Linked."); //
                }
                else
                    await ReplyAsync($"{user.Mention} Please use format \"!linkgithub https://github.com/example\"");
            }
        }
    }
}
