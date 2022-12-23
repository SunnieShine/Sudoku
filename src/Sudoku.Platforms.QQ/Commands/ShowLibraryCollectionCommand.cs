﻿namespace Sudoku.Platforms.QQ.Commands;

/// <summary>
/// Defines show library collection data command.
/// </summary>
[Command]
file sealed class ShowLibraryCollectionCommand : Command
{
	/// <inheritdoc/>
	public override string CommandName => R.Command("DisplayLib")!;

	/// <inheritdoc/>
	public override CommandComparison ComparisonMode => CommandComparison.Prefix;


	/// <inheritdoc/>
	protected override async Task<bool> ExecuteCoreAsync(string args, GroupMessageReceiver e)
	{
		var groupId = e.GroupId;
		if (InternalReadWrite.ReadLibraryConfiguration(groupId) is not { Count: var libCount and not 0 } libs)
		{
			await e.SendMessageAsync(R.MessageFormat("PuzzleLibraryIsNullOrEmpty")!);
			return true;
		}

		switch (args)
		{
			case []:
			{
				await e.SendMessageAsync(
					string.Format(R.MessageFormat("PuzzleLibGlobalInfo")!, libCount, string.Join('\u3001', from lib in libs select lib.Name))
				);

				break;
			}
			default:
			{
				var lib = libs.PuzzleLibraries.FirstOrDefault(lib => lib.Name == args);
				if (lib is null)
				{
					await e.SendMessageAsync(string.Format(R.MessageFormat("NoSpecifiedLibExists")!, args));
					return true;
				}

				var validPuzzlesCount = File.ReadLines(lib.PuzzleFilePath).Count(lineValidator);
				await e.SendMessageAsync(
					string.Format(
						R.MessageFormat("PuzzleLibSpecifiedInfo")!,
						lib.Name,
						lib.Description,
						lib.Tags switch { null or [] => R["None"]!, var tags => string.Join('\u3001', tags) },
						validPuzzlesCount,
						lib.FinishedPuzzlesCount,
						(double)lib.FinishedPuzzlesCount / validPuzzlesCount
					)
				);

				break;


				static bool lineValidator(string line) => !string.IsNullOrWhiteSpace(line) && Grid.TryParse(line, out _);
			}
		}

		return true;
	}
}
