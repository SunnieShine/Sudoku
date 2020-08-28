﻿namespace Sudoku.Solving.BruteForces.Bitwise
{
	partial class UnsafeBitwiseSolver
	{
		private static readonly byte[] TblShrinkMask =
		{
			0, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 3, 3, 2, 3, 3, 3, 3, 3, 3, 3, 2, 3, 3, 3, 3, 3, 3, 3,
			2, 3, 3, 3, 3, 3, 3, 3, 2, 3, 3, 3, 3, 3, 3, 3, 2, 3, 3, 3, 3, 3, 3, 3, 2, 3, 3, 3, 3, 3, 3, 3,
			4, 5, 5, 5, 5, 5, 5, 5, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7,
			6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7,
			4, 5, 5, 5, 5, 5, 5, 5, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7,
			6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7,
			4, 5, 5, 5, 5, 5, 5, 5, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7,
			6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7,
			4, 5, 5, 5, 5, 5, 5, 5, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7,
			6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7,
			4, 5, 5, 5, 5, 5, 5, 5, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7,
			6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7,
			4, 5, 5, 5, 5, 5, 5, 5, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7,
			6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7,
			4, 5, 5, 5, 5, 5, 5, 5, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7,
			6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7, 6, 7, 7, 7, 7, 7, 7, 7,
		};

		/// <summary>
		/// To keep mini rows still valid.
		/// </summary>
		private static readonly int[] TblComplexMask =
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0x381C71C0, 0x3F1C71C0, 0x381FF1C0, 0x3F1FF1C0,
			0, 0, 0, 0, 0x38FC71C0, 0x3FFC71C0, 0x3FFFF1C0, 0x3FFFF1C0,
			0, 0, 0x381F8038, 0x38FF8038, 0, 0, 0x381FF038, 0x38FFF038,
			0, 0, 0x3F1F8038, 0x3FFF8038, 0, 0, 0x3FFFF038, 0x3FFFF038,
			0, 0, 0x381F81F8, 0x3FFF81F8, 0x381C71F8, 0x3FFC71F8, 0x381FF1F8, 0x3FFFF1F8,
			0, 0, 0x3F1F81F8, 0x3FFF81F8, 0x38FC71F8, 0x3FFC71F8, 0x3FFFF1F8, 0x3FFFF1F8,
			0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0x38E00FC0, 0x38E38FC0, 0x3FE00FC0, 0x3FE38FC0,
			0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0x38FC0FC0, 0x3FFF8FC0, 0x3FFC0FC0, 0x3FFF8FC0,
			0, 0x38E38007, 0, 0x38FF8007, 0, 0x38E38E07, 0, 0x38FF8E07,
			0, 0x38E381C7, 0, 0x3FFF81C7, 0x38E00FC7, 0x38E38FC7, 0x3FFC0FC7, 0x3FFF8FC7,
			0, 0x3FE38007, 0, 0x3FFF8007, 0, 0x3FFF8E07, 0, 0x3FFF8E07,
			0, 0x3FE381C7, 0, 0x3FFF81C7, 0x38FC0FC7, 0x3FFF8FC7, 0x3FFC0FC7, 0x3FFF8FC7,
			0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0x38E07FC0, 0x38E3FFC0, 0x3FE3FFC0, 0x3FE3FFC0,
			0, 0, 0, 0, 0x381C7FC0, 0x3F1FFFC0, 0x381FFFC0, 0x3F1FFFC0,
			0, 0, 0, 0, 0x38FC7FC0, 0x3FFFFFC0, 0x3FFFFFC0, 0x3FFFFFC0,
			0, 0x38E3803F, 0x381F803F, 0x38FF803F, 0, 0x38E3FE3F, 0x381FFE3F, 0x38FFFE3F,
			0, 0x38E381FF, 0x3F1F81FF, 0x3FFF81FF, 0x38E07FFF, 0x38E3FFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0, 0x3FE381FF, 0x381F81FF, 0x3FFF81FF, 0x381C7FFF, 0x3FFFFFFF, 0x381FFFFF, 0x3FFFFFFF,
			0, 0x3FE381FF, 0x3F1F81FF, 0x3FFF81FF, 0x38FC7FFF, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0x3F000E38, 0x3F007E38, 0, 0, 0x3FE00E38, 0x3FE07E38,
			0, 0x3F007007, 0, 0x3F007E07, 0, 0x3F1C7007, 0, 0x3F1C7E07,
			0, 0x3F00703F, 0x3F000E3F, 0x3F007E3F, 0, 0x3FFC703F, 0x3FFC0E3F, 0x3FFC7E3F,
			0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0x3F1C0E38, 0x3FFC7E38, 0, 0, 0x3FFC0E38, 0x3FFC7E38,
			0, 0x3FE07007, 0, 0x3FFC7E07, 0, 0x3FFC7007, 0, 0x3FFC7E07,
			0, 0x3FE0703F, 0x3F1C0E3F, 0x3FFC7E3F, 0, 0x3FFC703F, 0x3FFC0E3F, 0x3FFC7E3F,
			0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0x3F038E38, 0x3F03FE38, 0, 0, 0x3FE3FE38, 0x3FE3FE38,
			0, 0x3F0071C7, 0, 0x3F03FFC7, 0x381C71C7, 0x3F1C71C7, 0x381FFFC7, 0x3F1FFFC7,
			0, 0x3F0071FF, 0x3F038FFF, 0x3F03FFFF, 0x38FC71FF, 0x3FFC71FF, 0x3FFFFFFF, 0x3FFFFFFF,
			0, 0, 0x381F8E38, 0x38FFFE38, 0, 0, 0x381FFE38, 0x38FFFE38,
			0, 0, 0x3F1F8E38, 0x3FFFFE38, 0, 0, 0x3FFFFE38, 0x3FFFFE38,
			0, 0x3FE071FF, 0x381F8FFF, 0x3FFFFFFF, 0x381C71FF, 0x3FFC71FF, 0x381FFFFF, 0x3FFFFFFF,
			0, 0x3FE071FF, 0x3F1F8FFF, 0x3FFFFFFF, 0x38FC71FF, 0x3FFC71FF, 0x3FFFFFFF, 0x3FFFFFFF,
			0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0x3F000FF8, 0x3F03FFF8, 0x38E00FF8, 0x38E3FFF8, 0x3FE00FF8, 0x3FE3FFF8,
			0, 0x3F03F007, 0, 0x3F03FE07, 0, 0x3F1FFE07, 0, 0x3F1FFE07,
			0, 0x3F03F1FF, 0x3F000FFF, 0x3F03FFFF, 0x38FC0FFF, 0x3FFFFFFF, 0x3FFC0FFF, 0x3FFFFFFF,
			0, 0x38E3F007, 0, 0x38FFFE07, 0, 0x38E3FE07, 0, 0x38FFFE07,
			0, 0x38E3F1FF, 0x3F1C0FFF, 0x3FFFFFFF, 0x38E00FFF, 0x38E3FFFF, 0x3FFC0FFF, 0x3FFFFFFF,
			0, 0x3FE3F007, 0, 0x3FFFFE07, 0, 0x3FFFFE07, 0, 0x3FFFFE07,
			0, 0x3FE3F1FF, 0x3F1C0FFF, 0x3FFFFFFF, 0x38FC0FFF, 0x3FFFFFFF, 0x3FFC0FFF, 0x3FFFFFFF,
			0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0x3F038FF8, 0x3F03FFF8, 0x38E07FF8, 0x38E3FFF8, 0x3FE3FFF8, 0x3FE3FFF8,
			0, 0x3F03F1C7, 0, 0x3F03FFC7, 0x381C7FC7, 0x3F1FFFC7, 0x381FFFC7, 0x3F1FFFC7,
			0, 0x3F03F1FF, 0x3F038FFF, 0x3F03FFFF, 0x38FC7FFF, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0, 0x38E3F03F, 0x381F8E3F, 0x38FFFE3F, 0, 0x38E3FE3F, 0x381FFE3F, 0x38FFFE3F,
			0, 0x38E3F1FF, 0x3F1F8FFF, 0x3FFFFFFF, 0x38E07FFF, 0x38E3FFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0, 0x3FE3F1FF, 0x381F8FFF, 0x3FFFFFFF, 0x381C7FFF, 0x3FFFFFFF, 0x381FFFFF, 0x3FFFFFFF,
			0, 0x3FE3F1FF, 0x3F1F8FFF, 0x3FFFFFFF, 0x38FC7FFF, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
		};

		/// <summary>
		/// Kill in other blocks locked column/box.
		/// </summary>
		private static readonly int[] TblMaskSingle =
		{
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3FDFEFF7, 0x3FDBEDF6, 0x3FD7EBF5, 0x3FDFEFF7, 0x3FCFE7F3, 0x3FDFEFF7, 0x3FDFEFF7, 0x3FDFEFF7,
			0x3FBFDFEF, 0x3FBBDDEE, 0x3FB7DBED, 0x3FBFDFEF, 0x3FAFD7EB, 0x3FBFDFEF, 0x3FBFDFEF, 0x3FBFDFEF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3F7FBFDF, 0x3F7BBDDE, 0x3F77BBDD, 0x3F7FBFDF, 0x3F6FB7DB, 0x3F7FBFDF, 0x3F7FBFDF, 0x3F7FBFDF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3EFF7FBF, 0x3EFB7DBE, 0x3EF77BBD, 0x3EFF7FBF, 0x3EEF77BB, 0x3EFF7FBF, 0x3EFF7FBF, 0x3EFF7FBF,
			0x3EDF6FB7, 0x3EDB6DB6, 0x3ED76BB5, 0x3EDF6FB7, 0x3ECF67B3, 0x3EDF6FB7, 0x3EDF6FB7, 0x3EDF6FB7,
			0x3EBF5FAF, 0x3EBB5DAE, 0x3EB75BAD, 0x3EBF5FAF, 0x3EAF57AB, 0x3EBF5FAF, 0x3EBF5FAF, 0x3EBF5FAF,
			0x3EFF7FBF, 0x3EFB7DBE, 0x3EF77BBD, 0x3EFF7FBF, 0x3EEF77BB, 0x3EFF7FBF, 0x3EFF7FBF, 0x3EFF7FBF,
			0x3E7F3F9F, 0x3E7B3D9E, 0x3E773B9D, 0x3E7F3F9F, 0x3E6F379B, 0x3E7F3F9F, 0x3E7F3F9F, 0x3E7F3F9F,
			0x3EFF7FBF, 0x3EFB7DBE, 0x3EF77BBD, 0x3EFF7FBF, 0x3EEF77BB, 0x3EFF7FBF, 0x3EFF7FBF, 0x3EFF7FBF,
			0x3EFF7FBF, 0x3EFB7DBE, 0x3EF77BBD, 0x3EFF7FBF, 0x3EEF77BB, 0x3EFF7FBF, 0x3EFF7FBF, 0x3EFF7FBF,
			0x3EFF7FBF, 0x3EFB7DBE, 0x3EF77BBD, 0x3EFF7FBF, 0x3EEF77BB, 0x3EFF7FBF, 0x3EFF7FBF, 0x3EFF7FBF,
			0x3DFEFF7F, 0x3DFAFD7E, 0x3DF6FB7D, 0x3DFEFF7F, 0x3DEEF77B, 0x3DFEFF7F, 0x3DFEFF7F, 0x3DFEFF7F,
			0x3DDEEF77, 0x3DDAED76, 0x3DD6EB75, 0x3DDEEF77, 0x3DCEE773, 0x3DDEEF77, 0x3DDEEF77, 0x3DDEEF77,
			0x3DBEDF6F, 0x3DBADD6E, 0x3DB6DB6D, 0x3DBEDF6F, 0x3DAED76B, 0x3DBEDF6F, 0x3DBEDF6F, 0x3DBEDF6F,
			0x3DFEFF7F, 0x3DFAFD7E, 0x3DF6FB7D, 0x3DFEFF7F, 0x3DEEF77B, 0x3DFEFF7F, 0x3DFEFF7F, 0x3DFEFF7F,
			0x3D7EBF5F, 0x3D7ABD5E, 0x3D76BB5D, 0x3D7EBF5F, 0x3D6EB75B, 0x3D7EBF5F, 0x3D7EBF5F, 0x3D7EBF5F,
			0x3DFEFF7F, 0x3DFAFD7E, 0x3DF6FB7D, 0x3DFEFF7F, 0x3DEEF77B, 0x3DFEFF7F, 0x3DFEFF7F, 0x3DFEFF7F,
			0x3DFEFF7F, 0x3DFAFD7E, 0x3DF6FB7D, 0x3DFEFF7F, 0x3DEEF77B, 0x3DFEFF7F, 0x3DFEFF7F, 0x3DFEFF7F,
			0x3DFEFF7F, 0x3DFAFD7E, 0x3DF6FB7D, 0x3DFEFF7F, 0x3DEEF77B, 0x3DFEFF7F, 0x3DFEFF7F, 0x3DFEFF7F,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3FDFEFF7, 0x3FDBEDF6, 0x3FD7EBF5, 0x3FDFEFF7, 0x3FCFE7F3, 0x3FDFEFF7, 0x3FDFEFF7, 0x3FDFEFF7,
			0x3FBFDFEF, 0x3FBBDDEE, 0x3FB7DBED, 0x3FBFDFEF, 0x3FAFD7EB, 0x3FBFDFEF, 0x3FBFDFEF, 0x3FBFDFEF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3F7FBFDF, 0x3F7BBDDE, 0x3F77BBDD, 0x3F7FBFDF, 0x3F6FB7DB, 0x3F7FBFDF, 0x3F7FBFDF, 0x3F7FBFDF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3BFDFEFF, 0x3BF9FCFE, 0x3BF5FAFD, 0x3BFDFEFF, 0x3BEDF6FB, 0x3BFDFEFF, 0x3BFDFEFF, 0x3BFDFEFF,
			0x3BDDEEF7, 0x3BD9ECF6, 0x3BD5EAF5, 0x3BDDEEF7, 0x3BCDE6F3, 0x3BDDEEF7, 0x3BDDEEF7, 0x3BDDEEF7,
			0x3BBDDEEF, 0x3BB9DCEE, 0x3BB5DAED, 0x3BBDDEEF, 0x3BADD6EB, 0x3BBDDEEF, 0x3BBDDEEF, 0x3BBDDEEF,
			0x3BFDFEFF, 0x3BF9FCFE, 0x3BF5FAFD, 0x3BFDFEFF, 0x3BEDF6FB, 0x3BFDFEFF, 0x3BFDFEFF, 0x3BFDFEFF,
			0x3B7DBEDF, 0x3B79BCDE, 0x3B75BADD, 0x3B7DBEDF, 0x3B6DB6DB, 0x3B7DBEDF, 0x3B7DBEDF, 0x3B7DBEDF,
			0x3BFDFEFF, 0x3BF9FCFE, 0x3BF5FAFD, 0x3BFDFEFF, 0x3BEDF6FB, 0x3BFDFEFF, 0x3BFDFEFF, 0x3BFDFEFF,
			0x3BFDFEFF, 0x3BF9FCFE, 0x3BF5FAFD, 0x3BFDFEFF, 0x3BEDF6FB, 0x3BFDFEFF, 0x3BFDFEFF, 0x3BFDFEFF,
			0x3BFDFEFF, 0x3BF9FCFE, 0x3BF5FAFD, 0x3BFDFEFF, 0x3BEDF6FB, 0x3BFDFEFF, 0x3BFDFEFF, 0x3BFDFEFF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3FDFEFF7, 0x3FDBEDF6, 0x3FD7EBF5, 0x3FDFEFF7, 0x3FCFE7F3, 0x3FDFEFF7, 0x3FDFEFF7, 0x3FDFEFF7,
			0x3FBFDFEF, 0x3FBBDDEE, 0x3FB7DBED, 0x3FBFDFEF, 0x3FAFD7EB, 0x3FBFDFEF, 0x3FBFDFEF, 0x3FBFDFEF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3F7FBFDF, 0x3F7BBDDE, 0x3F77BBDD, 0x3F7FBFDF, 0x3F6FB7DB, 0x3F7FBFDF, 0x3F7FBFDF, 0x3F7FBFDF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3FDFEFF7, 0x3FDBEDF6, 0x3FD7EBF5, 0x3FDFEFF7, 0x3FCFE7F3, 0x3FDFEFF7, 0x3FDFEFF7, 0x3FDFEFF7,
			0x3FBFDFEF, 0x3FBBDDEE, 0x3FB7DBED, 0x3FBFDFEF, 0x3FAFD7EB, 0x3FBFDFEF, 0x3FBFDFEF, 0x3FBFDFEF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3F7FBFDF, 0x3F7BBDDE, 0x3F77BBDD, 0x3F7FBFDF, 0x3F6FB7DB, 0x3F7FBFDF, 0x3F7FBFDF, 0x3F7FBFDF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3FDFEFF7, 0x3FDBEDF6, 0x3FD7EBF5, 0x3FDFEFF7, 0x3FCFE7F3, 0x3FDFEFF7, 0x3FDFEFF7, 0x3FDFEFF7,
			0x3FBFDFEF, 0x3FBBDDEE, 0x3FB7DBED, 0x3FBFDFEF, 0x3FAFD7EB, 0x3FBFDFEF, 0x3FBFDFEF, 0x3FBFDFEF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3F7FBFDF, 0x3F7BBDDE, 0x3F77BBDD, 0x3F7FBFDF, 0x3F6FB7DB, 0x3F7FBFDF, 0x3F7FBFDF, 0x3F7FBFDF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
			0x3FFFFFFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FFFFFFF, 0x3FEFF7FB, 0x3FFFFFFF, 0x3FFFFFFF, 0x3FFFFFFF,
		};

		/// <summary>
		/// Keep only rows with single.
		/// </summary>
		private static readonly int[] TblShrinkSingle =
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x54, 0x54, 0x54, 0x54, 0, 0, 0, 0, 0x54, 0x54, 0x54, 0x54,
			0, 0, 0x62, 0x62, 0, 0, 0x62, 0x62, 0, 0, 0x62, 0x62, 0, 0, 0x62, 0x62, 0, 0, 0x62, 0x62, 0x54, 0x54, 0x40, 0x40, 0, 0, 0x62, 0x62, 0x54, 0x54, 0x40, 0x40,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x8C, 0x8C, 0x8C, 0x8C, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x8C, 0x8C, 0x8C, 0x8C,
			0, 0xA1, 0, 0xA1, 0, 0xA1, 0, 0xA1, 0, 0xA1, 0, 0xA1, 0x8C, 0x80, 0x8C, 0x80, 0, 0xA1, 0, 0xA1, 0, 0xA1, 0, 0xA1, 0, 0xA1, 0, 0xA1, 0x8C, 0x80, 0x8C, 0x80,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x8C, 0x8C, 0x8C, 0x8C, 0, 0, 0, 0, 0x54, 0x54, 0x54, 0x54, 0, 0, 0, 0, 0x4, 0x4, 0x4, 0x4,
			0, 0xA1, 0x62, 0x20, 0, 0xA1, 0x62, 0x20, 0, 0xA1, 0x62, 0x20, 0x8C, 0x80, 0, 0, 0, 0xA1, 0x62, 0x20, 0x54, 0, 0x40, 0, 0, 0xA1, 0x62, 0x20, 0x4, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x10A, 0x10A, 0, 0, 0x10A, 0x10A, 0, 0x111, 0, 0x111, 0, 0x111, 0, 0x111, 0, 0x111, 0x10A, 0x100, 0, 0x111, 0x10A, 0x100,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x10A, 0x10A, 0, 0, 0x10A, 0x10A, 0, 0x111, 0, 0x111, 0, 0x111, 0, 0x111, 0, 0x111, 0x10A, 0x100, 0, 0x111, 0x10A, 0x100,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x10A, 0x10A, 0, 0, 0x10A, 0x10A, 0, 0x111, 0, 0x111, 0x54, 0x10, 0x54, 0x10, 0, 0x111, 0x10A, 0x100, 0x54, 0x10, 0, 0,
			0, 0, 0x62, 0x62, 0, 0, 0x62, 0x62, 0, 0, 0x2, 0x2, 0, 0, 0x2, 0x2, 0, 0x111, 0x62, 0, 0x54, 0x10, 0x40, 0, 0, 0x111, 0x2, 0, 0x54, 0x10, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x10A, 0x10A, 0x8C, 0x8C, 0x8, 0x8, 0, 0x111, 0, 0x111, 0, 0x111, 0, 0x111, 0, 0x111, 0x10A, 0x100, 0x8C, 0, 0x8, 0,
			0, 0xA1, 0, 0xA1, 0, 0xA1, 0, 0xA1, 0, 0xA1, 0x10A, 0, 0x8C, 0x80, 0x8, 0, 0, 0x1, 0, 0x1, 0, 0x1, 0, 0x1, 0, 0x1, 0x10A, 0, 0x8C, 0, 0x8, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x10A, 0x10A, 0x8C, 0x8C, 0x8, 0x8, 0, 0x111, 0, 0x111, 0x54, 0x10, 0x54, 0x10, 0, 0x111, 0x10A, 0x100, 0x4, 0, 0, 0,
			0, 0xA1, 0x62, 0x20, 0, 0xA1, 0x62, 0x20, 0, 0xA1, 0x2, 0, 0x8C, 0x80, 0, 0, 0, 0x1, 0x62, 0, 0x54, 0, 0x40, 0, 0, 0x1, 0x2, 0, 0x4, 0, 0, 0,
		};

		/// <summary>
		/// 1 is row not defined in block mode 1 to 111.
		/// </summary>
		private static readonly byte[] TblRowUniq =
		{
			7, 6, 6, 6, 6, 6, 6, 6, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4,
			5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4,
			3, 2, 2, 2, 2, 2, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
			1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
			3, 2, 2, 2, 2, 2, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
			1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
			3, 2, 2, 2, 2, 2, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
			1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
			3, 2, 2, 2, 2, 2, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
			1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
			3, 2, 2, 2, 2, 2, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
			1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
			3, 2, 2, 2, 2, 2, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
			1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
			3, 2, 2, 2, 2, 2, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
			1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
		};

		/// <summary>
		/// Single in column applied to shrinked block.
		/// </summary>
		private static readonly int[] TblColumnSingle =
		{
			0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0,
			0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0,
			0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0,
			0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0,
			0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1FF, 0x1FF, 0x1B6, 0x1FF, 0x1B6, 0x1B6, 0x1B6,
			0x0, 0x1FF, 0x1FF, 0x1B6, 0x1FF, 0x1B6, 0x1B6, 0x1B6, 0x0, 0x16D, 0x16D, 0x124, 0x16D, 0x124, 0x124, 0x124,
			0x0, 0x1FF, 0x1FF, 0x1B6, 0x1FF, 0x1B6, 0x1B6, 0x1B6, 0x0, 0x16D, 0x16D, 0x124, 0x16D, 0x124, 0x124, 0x124,
			0x0, 0x16D, 0x16D, 0x124, 0x16D, 0x124, 0x124, 0x124, 0x0, 0x16D, 0x16D, 0x124, 0x16D, 0x124, 0x124, 0x124,
			0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1FF, 0x1FF, 0x1B6, 0x1FF, 0x1B6, 0x1B6, 0x1B6,
			0x0, 0x1FF, 0x1FF, 0x1B6, 0x1FF, 0x1B6, 0x1B6, 0x1B6, 0x0, 0x16D, 0x16D, 0x124, 0x16D, 0x124, 0x124, 0x124,
			0x0, 0x1FF, 0x1FF, 0x1B6, 0x1FF, 0x1B6, 0x1B6, 0x1B6, 0x0, 0x16D, 0x16D, 0x124, 0x16D, 0x124, 0x124, 0x124,
			0x0, 0x16D, 0x16D, 0x124, 0x16D, 0x124, 0x124, 0x124, 0x0, 0x16D, 0x16D, 0x124, 0x16D, 0x124, 0x124, 0x124,
			0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xDB, 0xDB, 0x92, 0xDB, 0x92, 0x92, 0x92,
			0x0, 0xDB, 0xDB, 0x92, 0xDB, 0x92, 0x92, 0x92, 0x0, 0x49, 0x49, 0x0, 0x49, 0x0, 0x0, 0x0,
			0x0, 0xDB, 0xDB, 0x92, 0xDB, 0x92, 0x92, 0x92, 0x0, 0x49, 0x49, 0x0, 0x49, 0x0, 0x0, 0x0,
			0x0, 0x49, 0x49, 0x0, 0x49, 0x0, 0x0, 0x0, 0x0, 0x49, 0x49, 0x0, 0x49, 0x0, 0x0, 0x0,
			0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1FF, 0x1FF, 0x1B6, 0x1FF, 0x1B6, 0x1B6, 0x1B6,
			0x0, 0x1FF, 0x1FF, 0x1B6, 0x1FF, 0x1B6, 0x1B6, 0x1B6, 0x0, 0x16D, 0x16D, 0x124, 0x16D, 0x124, 0x124, 0x124,
			0x0, 0x1FF, 0x1FF, 0x1B6, 0x1FF, 0x1B6, 0x1B6, 0x1B6, 0x0, 0x16D, 0x16D, 0x124, 0x16D, 0x124, 0x124, 0x124,
			0x0, 0x16D, 0x16D, 0x124, 0x16D, 0x124, 0x124, 0x124, 0x0, 0x16D, 0x16D, 0x124, 0x16D, 0x124, 0x124, 0x124,
			0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xDB, 0xDB, 0x92, 0xDB, 0x92, 0x92, 0x92,
			0x0, 0xDB, 0xDB, 0x92, 0xDB, 0x92, 0x92, 0x92, 0x0, 0x49, 0x49, 0x0, 0x49, 0x0, 0x0, 0x0,
			0x0, 0xDB, 0xDB, 0x92, 0xDB, 0x92, 0x92, 0x92, 0x0, 0x49, 0x49, 0x0, 0x49, 0x0, 0x0, 0x0,
			0x0, 0x49, 0x49, 0x0, 0x49, 0x0, 0x0, 0x0, 0x0, 0x49, 0x49, 0x0, 0x49, 0x0, 0x0, 0x0,
			0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xDB, 0xDB, 0x92, 0xDB, 0x92, 0x92, 0x92,
			0x0, 0xDB, 0xDB, 0x92, 0xDB, 0x92, 0x92, 0x92, 0x0, 0x49, 0x49, 0x0, 0x49, 0x0, 0x0, 0x0,
			0x0, 0xDB, 0xDB, 0x92, 0xDB, 0x92, 0x92, 0x92, 0x0, 0x49, 0x49, 0x0, 0x49, 0x0, 0x0, 0x0,
			0x0, 0x49, 0x49, 0x0, 0x49, 0x0, 0x0, 0x0, 0x0, 0x49, 0x49, 0x0, 0x49, 0x0, 0x0, 0x0,
			0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xDB, 0xDB, 0x92, 0xDB, 0x92, 0x92, 0x92,
			0x0, 0xDB, 0xDB, 0x92, 0xDB, 0x92, 0x92, 0x92, 0x0, 0x49, 0x49, 0x0, 0x49, 0x0, 0x0, 0x0,
			0x0, 0xDB, 0xDB, 0x92, 0xDB, 0x92, 0x92, 0x92, 0x0, 0x49, 0x49, 0x0, 0x49, 0x0, 0x0, 0x0,
			0x0, 0x49, 0x49, 0x0, 0x49, 0x0, 0x0, 0x0, 0x0, 0x49, 0x49, 0x0, 0x49, 0x0, 0x0, 0x0,
		};

		/// <summary>
		/// Rows where single  found  000 to 111.
		/// </summary>
		private static readonly int[] TblRowMask =
		{
			0x7FFFFFF, 0x7FFFE00, 0x7FC01FF, 0x7FC0000, 0x3FFFF, 0x3FE00, 0x1FF, 0x0,
		};

		private static readonly byte[] CellToSubBand =
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0,
			1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1,
			2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2,
		};

		private static readonly int[] CellToMask =
		{
			0x1, 0x2, 0x4, 0x8, 0x10, 0x20, 0x40, 0x80, 0x100,
			0x200, 0x400, 0x800, 0x1000, 0x2000, 0x4000, 0x8000, 0x10000, 0x20000,
			0x40000, 0x80000, 0x100000, 0x200000, 0x400000, 0x800000, 0x1000000, 0x2000000, 0x4000000,
			0x1, 0x2, 0x4, 0x8, 0x10, 0x20, 0x40, 0x80, 0x100,
			0x200, 0x400, 0x800, 0x1000, 0x2000, 0x4000, 0x8000, 0x10000, 0x20000,
			0x40000, 0x80000, 0x100000, 0x200000, 0x400000, 0x800000, 0x1000000, 0x2000000, 0x4000000,
			0x1, 0x2, 0x4, 0x8, 0x10, 0x20, 0x40, 0x80, 0x100,
			0x200, 0x400, 0x800, 0x1000, 0x2000, 0x4000, 0x8000, 0x10000, 0x20000,
			0x40000, 0x80000, 0x100000, 0x200000, 0x400000, 0x800000, 0x1000000, 0x2000000, 0x4000000,
		};

		private static readonly byte[] DigitToBaseBand =
		{
			0, 3, 6, 9, 12, 15, 18, 21, 24,
		};

		private static readonly int[] TblSelfMask =
		{
			0x37E3F001, 0x37E3F002, 0x37E3F004, 0x371F8E08, 0x371F8E10, 0x371F8E20, 0x30FC7E40, 0x30FC7E80,
			0x30FC7F00, 0x2FE003F8, 0x2FE005F8, 0x2FE009F8, 0x2F1C11C7, 0x2F1C21C7, 0x2F1C41C7, 0x28FC803F,
			0x28FD003F, 0x28FE003F, 0x1807F1F8, 0x180BF1F8, 0x1813F1F8, 0x18238FC7, 0x18438FC7, 0x18838FC7,
			0x19007E3F, 0x1A007E3F, 0x1C007E3F, 0x37E3F001, 0x37E3F002, 0x37E3F004, 0x371F8E08, 0x371F8E10,
			0x371F8E20, 0x30FC7E40, 0x30FC7E80, 0x30FC7F00, 0x2FE003F8, 0x2FE005F8, 0x2FE009F8, 0x2F1C11C7,
			0x2F1C21C7, 0x2F1C41C7, 0x28FC803F, 0x28FD003F, 0x28FE003F, 0x1807F1F8, 0x180BF1F8, 0x1813F1F8,
			0x18238FC7, 0x18438FC7, 0x18838FC7, 0x19007E3F, 0x1A007E3F, 0x1C007E3F, 0x37E3F001, 0x37E3F002,
			0x37E3F004, 0x371F8E08, 0x371F8E10, 0x371F8E20, 0x30FC7E40, 0x30FC7E80, 0x30FC7F00, 0x2FE003F8,
			0x2FE005F8, 0x2FE009F8, 0x2F1C11C7, 0x2F1C21C7, 0x2F1C41C7, 0x28FC803F, 0x28FD003F, 0x28FE003F,
			0x1807F1F8, 0x180BF1F8, 0x1813F1F8, 0x18238FC7, 0x18438FC7, 0x18838FC7, 0x19007E3F, 0x1A007E3F,
			0x1C007E3F,
		};

		private static readonly byte[] TblAnother1 =
		{
			0x1, 0x0, 0x0, 0x4, 0x3, 0x3, 0x7, 0x6,
			0x6, 0xA, 0x9, 0x9, 0xD, 0xC, 0xC, 0x10,
			0xF, 0xF, 0x13, 0x12, 0x12, 0x16, 0x15, 0x15,
			0x19, 0x18, 0x18,
		};

		private static readonly byte[] TblAnother2 =
		{
			0x2, 0x2, 0x1, 0x5, 0x5, 0x4, 0x8, 0x8,
			0x7, 0xB, 0xB, 0xA, 0xE, 0xE, 0xD, 0x11,
			0x11, 0x10, 0x14, 0x14, 0x13, 0x17, 0x17, 0x16,
			0x1A, 0x1A, 0x19,
		};

		private static readonly int[] TblOtherMask =
		{
			0x3FFBFDFE, 0x3FF7FBFD, 0x3FEFF7FB, 0x3FDFEFF7, 0x3FBFDFEF, 0x3F7FBFDF, 0x3EFF7FBF, 0x3DFEFF7F,
			0x3BFDFEFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FEFF7FB, 0x3FDFEFF7, 0x3FBFDFEF, 0x3F7FBFDF, 0x3EFF7FBF,
			0x3DFEFF7F, 0x3BFDFEFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FEFF7FB, 0x3FDFEFF7, 0x3FBFDFEF, 0x3F7FBFDF,
			0x3EFF7FBF, 0x3DFEFF7F, 0x3BFDFEFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FEFF7FB, 0x3FDFEFF7, 0x3FBFDFEF,
			0x3F7FBFDF, 0x3EFF7FBF, 0x3DFEFF7F, 0x3BFDFEFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FEFF7FB, 0x3FDFEFF7,
			0x3FBFDFEF, 0x3F7FBFDF, 0x3EFF7FBF, 0x3DFEFF7F, 0x3BFDFEFF, 0x3FFBFDFE, 0x3FF7FBFD, 0x3FEFF7FB,
			0x3FDFEFF7, 0x3FBFDFEF, 0x3F7FBFDF, 0x3EFF7FBF, 0x3DFEFF7F, 0x3BFDFEFF, 0x3FFBFDFE, 0x3FF7FBFD,
			0x3FEFF7FB, 0x3FDFEFF7, 0x3FBFDFEF, 0x3F7FBFDF, 0x3EFF7FBF, 0x3DFEFF7F, 0x3BFDFEFF, 0x3FFBFDFE,
			0x3FF7FBFD, 0x3FEFF7FB, 0x3FDFEFF7, 0x3FBFDFEF, 0x3F7FBFDF, 0x3EFF7FBF, 0x3DFEFF7F, 0x3BFDFEFF,
			0x3FFBFDFE, 0x3FF7FBFD, 0x3FEFF7FB, 0x3FDFEFF7, 0x3FBFDFEF, 0x3F7FBFDF, 0x3EFF7FBF, 0x3DFEFF7F,
			0x3BFDFEFF,
		};

		private static readonly byte[] CellToRow =
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0,
			1, 1, 1, 1, 1, 1, 1, 1, 1,
			2, 2, 2, 2, 2, 2, 2, 2, 2,
			3, 3, 3, 3, 3, 3, 3, 3, 3,
			4, 4, 4, 4, 4, 4, 4, 4, 4,
			5, 5, 5, 5, 5, 5, 5, 5, 5,
			6, 6, 6, 6, 6, 6, 6, 6, 6,
			7, 7, 7, 7, 7, 7, 7, 7, 7,
			8, 8, 8, 8, 8, 8, 8, 8, 8,
		};

		private static readonly byte[] Mod3 =
		{
			0, 1, 2, 0, 1, 2, 0, 1, 2,
			0, 1, 2, 0, 1, 2, 0, 1, 2,
			0, 1, 2, 0, 1, 2, 0, 1, 2,
		};

		private static readonly byte[] Mod27 =
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8,
			9, 10, 11, 12, 13, 14, 15, 16, 17,
			18, 19, 20, 21, 22, 23, 24, 25, 26,
			0, 1, 2, 3, 4, 5, 6, 7, 8,
			9, 10, 11, 12, 13, 14, 15, 16, 17,
			18, 19, 20, 21, 22, 23, 24, 25, 26,
			0, 1, 2, 3, 4, 5, 6, 7, 8,
			9, 10, 11, 12, 13, 14, 15, 16, 17,
			18, 19, 20, 21, 22, 23, 24, 25, 26,
		};

		private static readonly byte[] MultiplyDeBruijnBitPosition32 =
		{
			0, 1, 28, 2, 29, 14, 24, 3, 30, 22, 20, 15, 25, 17, 4, 8,
			31, 27, 13, 23, 21, 19, 16, 7, 26, 12, 18, 6, 11, 5, 10, 9
		};
	}
}
