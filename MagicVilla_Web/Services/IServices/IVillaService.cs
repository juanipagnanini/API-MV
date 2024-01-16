﻿using MagicVilla_Web.Models.DTO;
using System.Runtime.InteropServices.Marshalling;

namespace MagicVilla_Web.Services.IServices
{
    public interface IVillaService
    {
        Task<T> GetAll <T>();
        Task<T> Get<T>(int id);
        Task<T> Create<T>(VillaCreateDto dto);
        Task<T> Update<T>(VillaUpdateDto dto);
        Task<T> Delete<T>(int id);
    }
}
