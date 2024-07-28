namespace Sudoku.Bot.Commands;

/// <summary>
/// 表示修改信息的指令。
/// </summary>
public sealed class ChangeInfoCommand : Command
{
	/// <inheritdoc/>
	public override string CommandName => "更新";


	/// <inheritdoc/>
	public override async Task GroupCallback(ChatMessageApi api, ChatMessage message)
	{
		var qq = message.Author.MemberOpenId;
		switch (message.GetContentArguments())
		{
			case ["名称" or "名字" or "名" or "昵称", var newName]:
			{
				UserData.Update(qq, d => d.VirtualNickname = newName);
				await api.SendGroupMessageAsync(message, $"你的名称已经更新为“{newName}”。");
				break;
			}
			case ["名称" or "名字" or "名" or "昵称", .. { Length: >= 2 }]:
			{
				await api.SendGroupMessageAsync(message, "昵称不能带空格。请重试。");
				break;
			}
			default:
			{
				await api.SendGroupMessageAsync(message, "写法内容：“/更新 昵称 Sunnie”。");
				break;
			}
		}
	}
}
