// This file stores built-in 'StepSearcher' instances,
// in order to create default collection of member 'StepSearcherPool.Default'.

[assembly: StepSearcherImport<SingleStepSearcher>(0)]
[assembly: StepSearcherImport<LockedSubsetStepSearcher>(0)]
[assembly: StepSearcherImport<LockedCandidatesStepSearcher>(0)]
[assembly: StepSearcherImport<NormalSubsetStepSearcher>(0)]
[assembly: StepSearcherImport<NormalFishStepSearcher>(1)]
[assembly: StepSearcherImport<TwoStrongLinksStepSearcher>(1)]
[assembly: StepSearcherImport<RegularWingStepSearcher>(1)]
[assembly: StepSearcherImport<WWingStepSearcher>(1)]
[assembly: StepSearcherImport<GroupedTwoStrongLinksStepSearcher>(1)]
[assembly: StepSearcherImport<MultiBranchWWingStepSearcher>(1)]
[assembly: StepSearcherImport<UniqueRectangleStepSearcher>(1)]
[assembly: StepSearcherImport<AlmostLockedCandidatesStepSearcher>(1)]
[assembly: StepSearcherImport<SueDeCoqStepSearcher>(1)]
[assembly: StepSearcherImport<SueDeCoq3DimensionStepSearcher>(1)]
[assembly: StepSearcherImport<UniqueLoopStepSearcher>(1)]
[assembly: StepSearcherImport<ExtendedRectangleStepSearcher>(1)]
[assembly: StepSearcherImport<EmptyRectangleStepSearcher>(1)]
[assembly: StepSearcherImport<UniqueMatrixStepSearcher>(1)]
[assembly: StepSearcherImport<BorescoperDeadlyPatternStepSearcher>(1)]
[assembly: StepSearcherImport<QiuDeadlyPatternStepSearcher>(1)]
[assembly: StepSearcherImport<BivalueUniversalGraveStepSearcher>(1)]
[assembly: StepSearcherImport<ReverseBivalueUniversalGraveStepSearcher>(2)]
[assembly: StepSearcherImport<EmptyRectangleIntersectionPairStepSearcher>(1)]
[assembly: StepSearcherImport<FireworkStepSearcher>(1)]
[assembly: StepSearcherImport<GurthSymmetricalPlacementStepSearcher>(0)]
[assembly: StepSearcherImport<NonMultipleChainingStepSearcher>(2)]
[assembly: StepSearcherImport<AlmostLockedSetsXzStepSearcher>(1)]
[assembly: StepSearcherImport<AlmostLockedSetsXyWingStepSearcher>(1)]
[assembly: StepSearcherImport<AlignedExclusionStepSearcher>(1)]
[assembly: StepSearcherImport<AlmostLockedSetsWWingStepSearcher>(1)]
[assembly: StepSearcherImport<GuardianStepSearcher>(2)]
[assembly: StepSearcherImport<ComplexFishStepSearcher>(2)]
[assembly: StepSearcherImport<BivalueOddagonStepSearcher>(2)]
[assembly: StepSearcherImport<ChromaticPatternStepSearcher>(2)]
[assembly: StepSearcherImport<BlossomLoopStepSearcher>(3)]
[assembly: StepSearcherImport<MultipleChainingStepSearcher>(3)]
[assembly: StepSearcherImport<BowmanBingoStepSearcher>(3, Areas = 0)]
[assembly: StepSearcherImport<TemplateStepSearcher>(3, Areas = 0)]
[assembly: StepSearcherImport<PatternOverlayStepSearcher>(3, Areas = StepSearcherRunningArea.Gathering)]
[assembly: StepSearcherImport<JuniorExocetStepSearcher>(3)]
[assembly: StepSearcherImport<SeniorExocetStepSearcher>(3, Areas = 0)]
[assembly: StepSearcherImport<DominoLoopStepSearcher>(3)]
[assembly: StepSearcherImport<MultisectorLockedSetsStepSearcher>(3)]
[assembly: StepSearcherImport<AdvancedMultipleChainingStepSearcher>(3, Areas = 0)]
[assembly: StepSearcherImport<BruteForceStepSearcher>(4, Areas = StepSearcherRunningArea.Searching)]
