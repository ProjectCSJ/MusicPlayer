namespace MusicPlayer
{
    public class InteractionPing : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("ping", "Get bot's ping")]
        public async Task HandlePingCommand()
        {
            DateTime now = DateTime.Now;
            await RespondAsync($"Pong!\nDelay {Math.Abs(Context.Client.Latency)} ms");
        }
    }
}
