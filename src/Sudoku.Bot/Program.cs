var filePath = @"A:\QQ机器人\bot.json";
var json = File.ReadAllText(filePath);
var botAccessInfo = (OpenApiAccessInfo?)JsonSerializer.Deserialize<BotInfo>(json, JsonOptions)!;
var apiProvider = new QQChannelApi(botAccessInfo);
apiProvider.UseBotIdentity();

var registeredCommands = (Command[])[
	new DisplayInfoCommand(),
	new ChangeInfoCommand(),
	new CheckInCommand(),
	new AnalysisCommand()
];
var bot = new QQGroupBot(apiProvider);
bot.RegisterChatEvent();
bot.ReceivedChatGroupMessage += message =>
{
	var content = message.GetCommandFullName();
	var found = false;
	foreach (var command in registeredCommands)
	{
		if (command.CommandFullName == content)
		{
			command.GroupCallback(apiProvider.GetChatMessageApi(), message);
			found = true;
			break;
		}
	}

	var (severity, m) = found
		? (LogSeverity.Info, $"接收消息：“{message.Content}”。")
		: (LogSeverity.Warning, $"接收消息：“{message.Content}”，但指令匹配失败。");
	WriteLog(severity, m);
};
bot.OnConnected += () =>
{
	var commandNames = string.Join(ChineseComma, from command in registeredCommands select command.CommandName);
	WriteLog("连接机器人成功！");
	WriteLog(LogSeverity.Info, $"已注册的指令一共 {registeredCommands.Length} 个指令：{commandNames}");
};
bot.AuthenticationSuccess += static () => WriteLog("机器人鉴权成功！现在可以用机器人了。");
bot.OnError += static ex => WriteLog(LogSeverity.Error, $"机器人执行指令时出现错误：{ex.Message}");
await bot.OnlineAsync();

WriteLog("请按 Q 键退出机器人。");
BlockConsole('q');
WriteLog("退出机器人。");


/// <summary>
/// 主方法包裹的程序类。
/// </summary>
file static partial class Program
{
	/// <summary>
	/// 在反序列化 JSON 期间使用到的解析控制选项。
	/// </summary>
	private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
}