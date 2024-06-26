﻿using LeenAutoCovadis.Api.Data;
using LeenAutoCovadis.Api.Models;
using LeenAutoCovadis.shared.Dtos;
using LeenAutoCovadis.shared.Enums;
using LeenAutoCovadis.shared.Interfaces;
using LeenAutoCovadis.shared.Requests;
using LeenAutoCovadis.shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace LeenAutoCovadis.Api.Services
{
    public class UserService(CovadisContext dbContext, ICurrentUserContext userContext)
    {
        private readonly CovadisContext dbContext = dbContext;

        public IEnumerable<UserDto> GetUsers()
        {
            return dbContext.Users.Select(x => new UserDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email
            });
        }

        public UserDto? GetUserById(int id)
        {
            if (userContext.IsInRole(nameof(UserRole.User)))
            {
                id = userContext.User.Id;
            }

            var user = dbContext.Users.Find(id);

            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public UserDto CreateUser(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public UserDto? UpdateUser(int id, User user)
        {
            throw new NotImplementedException();
        }

        public AssignRoleResponse? AssignRole(AssignRoleRequest request)
        {
            var user = dbContext.Users
                .Include(x => x.Roles)
                .FirstOrDefault(x => x.Id == request.UserId);
            var role = dbContext.Roles.Find(request.RoleId);

            if (user == null || role == null)
            {
                return null;
            }

            if (user.Roles.Contains(role))
            {
                throw new ArgumentException("User already assigned to role");
            }

            user.Roles.Add(role);
            dbContext.SaveChanges();

            return new AssignRoleResponse
            {
                UserName = user.Name,
                RoleName = role.Name,
            };
        }
    }
}
