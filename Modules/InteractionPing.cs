namespace MusicPlayer
{
    public class InteractionPing : InteractionModuleBase<SocketInteractionContext>
    {
        [
            SlashCommand(
            "ping",
             "Get bot's ping"
             )
             ]
        public async Task HandlePingCommand()
        {
            // Embed Constructer
            var embed = new EmbedBuilder();

            // Embed Author Builder
            var Author = new EmbedAuthorBuilder()
            .WithName(Context.Client.CurrentUser.Username.ToString())
            .WithIconUrl(Context.Client.CurrentUser.GetAvatarUrl().ToString())
            .WithUrl(ConfigurationManager.AppSettings.Get("SiteURL"));
            // Embed Footer Builder
            var Footer = new EmbedFooterBuilder()
            .WithText($"{Context.Client.CurrentUser.Username.ToString()} by 幽霊Ｓ☄#1227")
            .WithIconUrl(Context.Client.CurrentUser.GetAvatarUrl().ToString());

            // Embed Field Builder
            // var Field = new EmbedFieldBuilder();

            // Color Code Generator
            var ColorRed = new Random();
            var ColorGreen = new Random();
            var ColorBlue = new Random();

            // Embed Method
            embed
            .WithAuthor(Author)
            .WithFooter(Footer)
            .WithColor(new Color(ColorRed.Next(256), ColorGreen.Next(256), ColorBlue.Next(256)))
            .WithTitle("Pong!")
            .WithDescription($"Delay {Math.Abs(Context.Client.Latency)} ms")
            .WithCurrentTimestamp();
            await RespondAsync(
                embed: embed.Build(),
                ephemeral: false);
        }
    }
}
