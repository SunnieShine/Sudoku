[assembly: BuiltInStepSearcher<SingleStepSearcher>(0)]
[assembly: BuiltInStepSearcher<DirectIntersectionStepSearcher>(0)]
[assembly: BuiltInStepSearcher<DirectSubsetStepSearcher>(0)]
[assembly: BuiltInStepSearcher<ComplexSingleStepSearcher>(1)]
[assembly: BuiltInStepSearcher<LockedSubsetStepSearcher>(0)]
[assembly: BuiltInStepSearcher<LockedCandidatesStepSearcher>(0)]
[assembly: BuiltInStepSearcher<LawOfLeftoverStepSearcher>(0)]
[assembly: BuiltInStepSearcher<NormalSubsetStepSearcher>(0)]
[assembly: BuiltInStepSearcher<NormalFishStepSearcher>(1)]
[assembly: BuiltInStepSearcher<TwoStrongLinksStepSearcher>(1)]
[assembly: BuiltInStepSearcher<GroupedTwoStrongLinksStepSearcher>(1)]
[assembly: BuiltInStepSearcher<RegularWingStepSearcher>(1)]
[assembly: BuiltInStepSearcher<UniqueRectangleStepSearcher>(1)]
[assembly: BuiltInStepSearcher<AlmostLockedCandidatesStepSearcher>(1)]
[assembly: BuiltInStepSearcher<SueDeCoqStepSearcher>(1)]
[assembly: BuiltInStepSearcher<SueDeCoq3DimensionStepSearcher>(1)]
[assembly: BuiltInStepSearcher<IrregularWingStepSearcher>(1)]
[assembly: BuiltInStepSearcher<ExtendedSubsetPrincipleStepSearcher>(1)]
[assembly: BuiltInStepSearcher<UniqueLoopStepSearcher>(1)]
[assembly: BuiltInStepSearcher<ExtendedRectangleStepSearcher>(1)]
[assembly: BuiltInStepSearcher<EmptyRectangleStepSearcher>(1)]
[assembly: BuiltInStepSearcher<UniqueMatrixStepSearcher>(1)]
[assembly: BuiltInStepSearcher<BorescoperDeadlyPatternStepSearcher>(1)]
[assembly: BuiltInStepSearcher<BivalueUniversalGraveStepSearcher>(1)]
[assembly: BuiltInStepSearcher<QiuDeadlyPatternStepSearcher>(1)]
[assembly: BuiltInStepSearcher<ReverseBivalueUniversalGraveStepSearcher>(2)]
[assembly: BuiltInStepSearcher<EmptyRectangleIntersectionPairStepSearcher>(1)]
[assembly: BuiltInStepSearcher<FireworkStepSearcher>(1)]
[assembly: BuiltInStepSearcher<AntiGurthSymmetricalPlacementStepSearcher>(0)]
[assembly: BuiltInStepSearcher<XyzRingStepSearcher>(1)]
[assembly: BuiltInStepSearcher<UniquenessClueCoverStepSearcher>(2)]
[assembly: BuiltInStepSearcher<ChainStepSearcher>(2)]
[assembly: BuiltInStepSearcher<AlmostLockedSetsXzStepSearcher>(1)]
[assembly: BuiltInStepSearcher<AlmostLockedSetsXyWingStepSearcher>(1)]
[assembly: BuiltInStepSearcher<AlignedExclusionStepSearcher>(1)]
[assembly: BuiltInStepSearcher<AlmostLockedSetsWWingStepSearcher>(1)]
[assembly: BuiltInStepSearcher<GuardianStepSearcher>(1)]
[assembly: BuiltInStepSearcher<ComplexFishStepSearcher>(2)]
[assembly: BuiltInStepSearcher<BivalueOddagonStepSearcher>(1)]
[assembly: BuiltInStepSearcher<ChromaticPatternStepSearcher>(2)]
[assembly: BuiltInStepSearcher<DeathBlossomStepSearcher>(2)]
#if true
[assembly: BuiltInStepSearcher<BlossomLoopStepSearcher>(3)]
[assembly: BuiltInStepSearcher<MultipleChainingStepSearcher>(3)]
#endif
[assembly: BuiltInStepSearcher<BowmanBingoStepSearcher>(3, Areas = 0)]
[assembly: BuiltInStepSearcher<TemplateStepSearcher>(3, Areas = 0)]
[assembly: BuiltInStepSearcher<PatternOverlayStepSearcher>(3, Areas = StepSearcherRunningArea.Collecting)]
[assembly: BuiltInStepSearcher<ExocetStepSearcher>(3)]
[assembly: BuiltInStepSearcher<DominoLoopStepSearcher>(3)]
[assembly: BuiltInStepSearcher<MultisectorLockedSetsStepSearcher>(3)]
#if true
[assembly: BuiltInStepSearcher<AdvancedMultipleChainingStepSearcher>(3, Areas = 0)]
#endif
[assembly: BuiltInStepSearcher<BruteForceStepSearcher>(4, Areas = StepSearcherRunningArea.Searching)]
