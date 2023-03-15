﻿namespace Sudoku.Analytics.InternalHelpers;

/// <summary>
/// Used by fish step searchers.
/// </summary>
internal static class FishStepSearcherHelper
{
	/// <summary>
	/// Check whether the fish is sashimi.
	/// </summary>
	/// <param name="baseSets">The base sets.</param>
	/// <param name="fins">All fins.</param>
	/// <param name="digit">The digit.</param>
	/// <returns>
	/// A <see cref="bool"/> value indicating that. All cases are as belows:
	/// <list type="table">
	/// <item>
	/// <term><see langword="true"/></term>
	/// <description>The fish is a sashimi finned fish.</description>
	/// </item>
	/// <item>
	/// <term><see langword="false"/></term>
	/// <description>The fish is a normal finned fish.</description>
	/// </item>
	/// <item>
	/// <term><see langword="null"/></term>
	/// <description>The fish doesn't contain any fin.</description>
	/// </item>
	/// </list>
	/// </returns>
	public static bool? IsSashimi(int[] baseSets, scoped in CellMap fins, int digit)
	{
		if (!fins)
		{
			return null;
		}

		var isSashimi = false;
		foreach (var baseSet in baseSets)
		{
			if ((HousesMap[baseSet] - fins & CandidatesMap[digit]).Count == 1)
			{
				isSashimi = true;
				break;
			}
		}

		return isSashimi;
	}

	/// <summary>
	/// Try to get the <see cref="Technique"/> code instance from the specified name, where the name belongs
	/// to a complex fish name, such as "Finned Franken Swordfish".
	/// </summary>
	/// <param name="name">The name.</param>
	/// <returns>The <see cref="Technique"/> code instance.</returns>
	/// <seealso cref="Technique"/>
	public static unsafe Technique GetComplexFishTechniqueCodeFromName(string name)
	{
		// Creates a buffer to store the characters that isn't a space or a bar.
		var buffer = stackalloc char[name.Length];
		var bufferLength = 0;
		fixed (char* p = name)
		{
			for (var ptr = p; *ptr != '\0'; ptr++)
			{
				if (*ptr is not ('-' or ' '))
				{
					buffer[bufferLength++] = *ptr;
				}
			}
		}

		// Parses via the buffer, and returns the result.
		return Enum.Parse<Technique>(new string(PointerOperations.GetArrayFromStart(buffer, bufferLength, 0)));
	}
}
