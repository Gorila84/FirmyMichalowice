﻿using FirmyMichalowice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Repositories
{
    public interface IPhotoRepository
    {
        void CreateFolderAndSaveImage(string userId);
       

    }
}
