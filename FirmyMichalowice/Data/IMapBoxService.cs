﻿using FirmyMichalowice.Model;
using System.Threading.Tasks;

namespace FirmyMichalowice.Serv
{
    public interface IMapBoxService
    {
        Task<string> GetGeolocationURL(Firma firma);
    }
}