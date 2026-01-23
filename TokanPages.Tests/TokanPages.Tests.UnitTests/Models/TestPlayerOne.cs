using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Tests.UnitTests.Models;

[DatabaseTable(Schema = "soccer", TableName =  "Players")]
internal abstract class TestPlayerOne { }
