using FirmyMichalowice.Dto_s;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {


        public void CreateFolderAndSaveImage(string companyId)
        {
            string path = ConfigurationManager.AppSettings["ImagePath"];

            if(!Directory.Exists(path + companyId))
            {
                Directory.CreateDirectory(path + companyId);
            }
        }

     
    }
}
