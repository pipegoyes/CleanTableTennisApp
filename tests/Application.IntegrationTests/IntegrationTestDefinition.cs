// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Xunit;

namespace CleanTableTennisApp.Application.IntegrationTests;

[CollectionDefinition(nameof(IntegrationTestLifeTime))]
public class IntegrationTestDefinition : ICollectionFixture<IntegrationTestLifeTime>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}
