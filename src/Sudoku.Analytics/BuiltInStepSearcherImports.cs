// This file stores built-in 'StepSearcher' instances,
// in order to create default collection of member 'StepSearcherPool.BuiltIn'.

[assembly: StepSearcherImport<SingleStepSearcher>(StepSearcherLevel.Elementary)]
[assembly: StepSearcherImport<LockedCandidatesStepSearcher>(StepSearcherLevel.Elementary)]
[assembly: StepSearcherImport<SubsetStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<NormalFishStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<TwoStrongLinksStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<RegularWingStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<WWingStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<MultiBranchWWingStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<UniqueRectangleStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<AlmostLockedCandidatesStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<SueDeCoqStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<SueDeCoq3DimensionStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<UniqueLoopStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<ExtendedRectangleStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<EmptyRectangleStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<UniqueMatrixStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<BorescoperDeadlyPatternStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<QiuDeadlyPatternStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<BivalueUniversalGraveStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<EmptyRectangleIntersectionPairStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<FireworkStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<GurthSymmetricalPlacementStepSearcher>(StepSearcherLevel.Elementary)]
[assembly: StepSearcherImport<NonMultipleChainingStepSearcher>(StepSearcherLevel.Hard)]
[assembly: StepSearcherImport<AlmostLockedSetsXzStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<AlmostLockedSetsXyWingStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<AlmostLockedSetsWWingStepSearcher>(StepSearcherLevel.Moderate)]
[assembly: StepSearcherImport<GuardianStepSearcher>(StepSearcherLevel.Hard)]
[assembly: StepSearcherImport<ComplexFishStepSearcher>(StepSearcherLevel.Hard)]
[assembly: StepSearcherImport<BivalueOddagonStepSearcher>(StepSearcherLevel.Hard)]
[assembly: StepSearcherImport<ChromaticPatternStepSearcher>(StepSearcherLevel.Hard)]
[assembly: StepSearcherImport<MultipleChainingStepSearcher>(StepSearcherLevel.Hard)]
[assembly: StepSearcherImport<BowmanBingoStepSearcher>(StepSearcherLevel.Hard, Areas = StepSearcherRunningArea.None)]
[assembly: StepSearcherImport<TemplateStepSearcher>(StepSearcherLevel.Hard, Areas = StepSearcherRunningArea.None)]
[assembly: StepSearcherImport<PatternOverlayStepSearcher>(StepSearcherLevel.Hard, Areas = StepSearcherRunningArea.Gathering)]
//[assembly: StepSearcherImport<JuniorExocetStepSearcher>(StepSearcherLevel.Fiendish)]
//[assembly: StepSearcherImport<SeniorExocetStepSearcher>(StepSearcherLevel.Fiendish, Areas = StepSearcherRunningArea.None)]
[assembly: StepSearcherImport<DominoLoopStepSearcher>(StepSearcherLevel.Fiendish)]
[assembly: StepSearcherImport<MultisectorLockedSetsStepSearcher>(StepSearcherLevel.Fiendish)]
[assembly: StepSearcherImport<AdvancedMultipleChainingStepSearcher>(StepSearcherLevel.Fiendish, Areas = StepSearcherRunningArea.None)]
[assembly: StepSearcherImport<BruteForceStepSearcher>(StepSearcherLevel.Hidden, Areas = StepSearcherRunningArea.Searching)]
