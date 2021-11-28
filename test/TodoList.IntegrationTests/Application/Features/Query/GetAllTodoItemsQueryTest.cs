using FluentAssertions;
using System;
using System.Threading.Tasks;
using TodoList.Application.Query;
using TodoList.IntegrationTests;
using Xunit;

namespace TodoList.Application.Command;

public class GetAllTodoItemsQueryTest : IClassFixture<BaseTestFixture>
{
    private readonly BaseTestFixture _fixture;

    public GetAllTodoItemsQueryTest(BaseTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ShouldReturnAllExistingTodoItems()
    {
        // Arrange 
        var command = new CreateTodoItemCommand
        {
            Description = Guid.NewGuid().ToString(),
        };
        var todoItem = await _fixture.SendAsync(command);

        // Act
        var result = await _fixture.SendAsync(new GetAllTodoItemsQuery());

        // Assert
        result.Should().NotBeEmpty();
        result.Should().ContainSingle(t => t.Id == todoItem.Id);
    }
}