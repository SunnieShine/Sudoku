﻿namespace Sudoku.Platforms.QQ.Modules.Group;

[GroupModule("加分")]
file sealed class UpdateSourceModule : GroupModule
{
	/// <inheritdoc/>
	public override string[] RaisingPrefix => CommonCommandPrefixes.HashTag;

	/// <inheritdoc/>
	public override GroupRoleKind RequiredSenderRole => GroupRoleKind.God;

	[DoubleArgument("昵称")]
	[Hint("表示你要为谁进行加分操作。该参数指定的是用户的群名片。")]
	public string? UserNickname { get; set; }

	[DoubleArgument("QQ")]
	[Hint("表示你要为谁进行加分操作。该参数指定的是用户的 QQ 号。")]
	public string? UserId { get; set; }

	[DoubleArgument("经验")]
	[Hint("表示你要为该用户加多少经验值。")]
	[ValueConverter<NumericConverter<int>>]
	public int ExperiencePointAddition { get; set; }

	[DoubleArgument("金币")]
	[Hint("表示你要为该用户加多少金币。")]
	[ValueConverter<NumericConverter<int>>]
	public int CoinAddition { get; set; }


	/// <inheritdoc/>
	protected override async Task ExecuteCoreAsync(GroupMessageReceiver messageReceiver)
	{
		if (messageReceiver is not { Sender.Group: var group })
		{
			return;
		}

		var folder = Environment.GetFolderPath(SpecialFolder.MyDocuments);
		if (!Directory.Exists(folder))
		{
			// Error. The computer does not contain "My Documents" folder.
			// This folder is special; if the computer does not contain the folder, we should return directly.
			return;
		}

		var botDataFolder = $"""{folder}\BotData""";
		if (!Directory.Exists(botDataFolder))
		{
			Directory.CreateDirectory(botDataFolder);
		}

		var botUsersDataFolder = $"""{botDataFolder}\Users""";
		if (!Directory.Exists(botUsersDataFolder))
		{
			Directory.CreateDirectory(botUsersDataFolder);
		}

		switch (UserNickname, UserId, ExperiencePointAddition, CoinAddition)
		{
			case (_, _, 0, _) or (_, _, _, 0):
			{
				await messageReceiver.SendMessageAsync("经验或金币数至少有一个数必须非 0。");
				break;
			}
			case ({ } name, null, var expAdd, var coinAdd):
			{
				switch (await group.GetMatchedMembersViaNicknameAsync(name))
				{
					case null or []:
					{
						await messageReceiver.SendMessageAsync($"本群不存在昵称为“{name}”的用户。");
						break;
					}
					case [{ Id: var userId } target]:
					{
						var fileName = $"""{botUsersDataFolder}\{userId}.json""";
						var userData = File.Exists(fileName)
							? Deserialize<User>(await File.ReadAllTextAsync(fileName))!
							: new() { QQ = userId, ComboCheckedIn = 0, LastCheckIn = DateTime.MinValue };

						userData.ExperiencePoint += expAdd;
						userData.Coin += coinAdd;

						await File.WriteAllTextAsync(fileName, Serialize(userData));

						await messageReceiver.SendMessageAsync(
							$"恭喜用户“{name}”获得{(expAdd, coinAdd) switch
							{
								(not 0, 0) => $" {Scorer.GetEarnedScoringDisplayingString(expAdd)} 经验",
								(0, not 0) => $" {Scorer.GetEarnedCoinDisplayingString(coinAdd)} 金币",
								_ => $" {Scorer.GetEarnedScoringDisplayingString(expAdd)} 经验，{Scorer.GetEarnedCoinDisplayingString(coinAdd)} 金币"
							}}！"
						);

						break;
					}
					default:
					{
						await messageReceiver.SendMessageAsync($"本群至少存在两个用户昵称为“{name}”。请改用 QQ 号来确保用户的唯一性");
						break;
					}
				}

				break;
			}
			case (null, { } userId, var expAdd, var coinAdd):
			{
				switch (await group.GetMatchedMemberViaIdAsync(userId))
				{
					case { Name: var name }:
					{
						var fileName = $"""{botUsersDataFolder}\{userId}.json""";
						var userData = File.Exists(fileName)
							? Deserialize<User>(await File.ReadAllTextAsync(fileName))!
							: new() { QQ = userId, ComboCheckedIn = 0, LastCheckIn = DateTime.MinValue };

						userData.ExperiencePoint += expAdd;

						await File.WriteAllTextAsync(fileName, Serialize(userData));
						await messageReceiver.SendMessageAsync(
							$"恭喜用户“{name}”获得{(expAdd, coinAdd) switch
							{
								(not 0, 0) => $" {Scorer.GetEarnedScoringDisplayingString(expAdd)} 经验",
								(0, not 0) => $" {Scorer.GetEarnedCoinDisplayingString(coinAdd)} 金币",
								_ => $" {Scorer.GetEarnedScoringDisplayingString(expAdd)} 经验，{Scorer.GetEarnedCoinDisplayingString(coinAdd)} 金币"
							}}！"
						);

						break;
					}
					default:
					{
						await messageReceiver.SendMessageAsync($"本群不存在 QQ 为“{userId}”的用户。");
						break;
					}
				}

				break;
			}
		}
	}
}
