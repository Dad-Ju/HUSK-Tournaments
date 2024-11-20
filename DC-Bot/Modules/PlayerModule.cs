﻿using Database;
using Database.Models;
using Discord.Interactions;

namespace DC_Bot.Modules;

public class PlayerModule : InteractionModuleBase<SocketInteractionContext>
{
    // Dependencies can be accessed through Property injection, public properties with public setters will be set by the service provider
    public InteractionService Commands { get; set; }

    private InteractionHandler _handler;
    private DatabaseContext db;

    // Constructor injection is also a valid way to access the dependencies
    public PlayerModule(InteractionHandler handler, DatabaseContext database)
    {
        _handler = handler;
        db = database;
    }

    [SlashCommand("debug", description: "Debug Player DB")]
    public async Task DebugPlayerDB()
    {
        db.Players.Add(
            new Player { PlayerID = "audhadiada", Description = "Im Juu", discordId = "269217065896771584" });
        await db.SaveChangesAsync();

        await RespondAsync(text: "Debug Player DB");
    }

    [Group("profile", "Everything about you and other players")]
    public class GroupProfile : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("me", "Shows everything about your yourself!")]
        public async Task PlayerSelf()
            => await RespondAsync("Something for now!");

        [SlashCommand("register", "Allows you to register!")]
        public async Task PlayerNew()
            => await RespondAsync("Something for now, Register!");

        [SlashCommand("lookup", "Shows everything about other players!")]
        public async Task PlayerSearch()
            => await RespondAsync("Something for now, Search!");

        [SlashCommand("inbox", "Shows your Inbox and Invites!")]
        public async Task PlayerInbox()
            => await RespondAsync("Something for now, Inbox!");
    }

    [Group("teams", "Everything about you and other players")]
    public class GroupTeams : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("profile", "Allows you to register!")]
        public async Task TeamProfile()
            => await RespondAsync("Something for now, Register!");

        [SlashCommand("lookup", "Shows everything about your yourself!")]
        public async Task TeamLookup()
            => await RespondAsync("Something for now!");

        [SlashCommand("create", "Shows your Inbox and Invites!")]
        public async Task PlayerInbox()
            => await RespondAsync("Something for now, Inbox!");

        [SlashCommand("leave", "Shows everything about other players!")]
        public async Task PlayerSearch()
            => await RespondAsync("Something for now, Search!");
    }
}