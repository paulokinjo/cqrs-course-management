﻿namespace Domain.Repositories;

public interface IUnityOfWork
{
    Task<bool> CommitAsync();
}
