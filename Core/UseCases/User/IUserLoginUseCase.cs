﻿using GloboClimaPlatform.Application.DTOs.User;
using GloboClimaPlatform.Infra.Entities;

namespace GloboClimaPlatform.Core.UseCases.User;

public interface IUserLoginUseCase
{
    public Task Execute(UsersEntity userEntity);
}