﻿[assembly:
	SearcherConfiguration<SingleStepSearcher>(SearcherDisplayingLevel.A),
	SearcherConfiguration<LockedCandidatesStepSearcher>(SearcherDisplayingLevel.A),
	SearcherConfiguration<SubsetStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<NormalFishStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<TwoStrongLinksStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<RegularWingStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<WWingStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<UniqueRectangleStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<AlmostLockedCandidatesStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<SueDeCoqStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<SueDeCoq3DimensionStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<UniqueLoopStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<ExtendedRectangleStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<EmptyRectangleStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<UniqueMatrixStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<UniquePolygonStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<QiuDeadlyPatternStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<BivalueUniversalGraveStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<ReverseBivalueUniversalGraveStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<UniquenessClueCoverStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<RwNPlus1TheoryStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<EmptyRectangleIntersectionPairStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<FireworkStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<GuardianStepSearcher>(SearcherDisplayingLevel.C),
	SearcherConfiguration<ComplexFishStepSearcher>(SearcherDisplayingLevel.C),
	SearcherConfiguration<BivalueOddagonStepSearcher>(SearcherDisplayingLevel.C),
	SearcherConfiguration<ChromaticPatternStepSearcher>(SearcherDisplayingLevel.C),
	SearcherConfiguration<AlmostLockedSetsXzStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<AlmostLockedSetsXyWingStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<AlmostLockedSetsWWingStepSearcher>(SearcherDisplayingLevel.B),
	SearcherConfiguration<AlternatingInferenceChainStepSearcher>(SearcherDisplayingLevel.C),
	SearcherConfiguration<BowmanBingoStepSearcher>(
		SearcherDisplayingLevel.C,
		EnabledArea = SearcherEnabledArea.None,
		DisabledReason = SearcherDisabledReason.TooSlow | SearcherDisabledReason.LastResort),
	SearcherConfiguration<PatternOverlayStepSearcher>(
		SearcherDisplayingLevel.C,
		EnabledArea = SearcherEnabledArea.Gathering,
		DisabledReason = SearcherDisabledReason.LastResort),
	SearcherConfiguration<TemplateStepSearcher>(
		SearcherDisplayingLevel.C,
		EnabledArea = SearcherEnabledArea.None,
		DisabledReason = SearcherDisabledReason.LastResort),
	SearcherConfiguration<JuniorExocetStepSearcher>(SearcherDisplayingLevel.D),
	SearcherConfiguration<SeniorExocetStepSearcher>(
		SearcherDisplayingLevel.D,
		EnabledArea = SearcherEnabledArea.None,
		DisabledReason = SearcherDisabledReason.DeprecatedOrNotImplemented),
	SearcherConfiguration<DominoLoopStepSearcher>(SearcherDisplayingLevel.D),
	SearcherConfiguration<MultisectorLockedSetsStepSearcher>(SearcherDisplayingLevel.D),
	SearcherConfiguration<BruteForceStepSearcher>(
		SearcherDisplayingLevel.E,
		EnabledArea = SearcherEnabledArea.Default,
		DisabledReason = SearcherDisabledReason.LastResort)
]
