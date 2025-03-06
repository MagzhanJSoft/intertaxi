using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using IntercityTaxi.Domain.Models;
using IntercityTaxi.Infrastructure.Repositories;
using IntercityTaxi.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IntercityTaxi.Tests.IntegrationTests
{
    public class UserRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // InMemory база для тестов
                .Options;

            _context = new AppDbContext(options);
            _userRepository = new UserRepository(_context);
        }

        [Fact]
        public async Task AddAsync_Should_Add_User_To_Database()
        {
            // Arrange
            var user = User.Register("77001112233", "John Doe", "hashedPassword", "").Value;

            // Act
            var result = await _userRepository.AddAsync(user);
            var savedUser = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == "77001112233");

            // Assert
            result.IsSuccess.Should().BeTrue();
            savedUser.Should().NotBeNull();
            savedUser.FullName.Should().Be("John Doe");
        }

        [Fact]
        public async Task GetByPhoneNumber_Should_Return_Correct_User()
        {
            // Arrange
            var user = User.Register("77001112233", "John Doe", "hashedPassword", "").Value;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetByPhoneNumber("77001112233");

            // Assert
            result.Should().NotBeNull();
            result.FullName.Should().Be("John Doe");
        }

        [Fact]
        public async Task GetByGuid_Should_Return_Correct_User()
        {
            // Arrange
            var user = User.Register("77001112233", "John Doe", "hashedPassword", "").Value;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetByGuid(user.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(user.Id);
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_User_Data()
        {
            // Arrange
            var user = User.Register("77001112233", "John Doe", "hashedPassword", "").Value;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            user.ChangePassword("newHashedPassword");

            // Act
            var result = await _userRepository.UpdateAsync(user);
            var updatedUser = await _context.Users.FindAsync(user.Id);

            // Assert
            result.IsSuccess.Should().BeTrue();
            updatedUser.Password.Should().Be("newHashedPassword");
        }

        [Fact]
        public async Task GetAllUsers_Should_Return_All_Users()
        {
            // Arrange
            _context.Users.Add(User.Register("77001112233", "User1", "password1", "").Value);
            _context.Users.Add(User.Register("77001112234", "User2", "password2", "").Value);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetAllUsers();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(2);
        }
    }
}
