using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Core.Common.Extensions;
using Mantis.Core.Common.Services;
using Mantis.Core.Files.Common;
using Mantis.Core.Files.Common.Enums;
using Mantis.Core.Files.Common.Services;

namespace Mantis.Core.Files.Services
{
    public class PathService(IEnvironmentVariableService environmentVariableService) : IPathService
    {
        private readonly IEnvironmentVariableService _environmentVariableService = environmentVariableService;

        public string GetRealPath(DirectoryPath path)
        {
            return path.Type switch
            {
                DirectoryPathTypeEnum.Absolute => path.Value,
                DirectoryPathTypeEnum.Relative => Path.Combine(Directory.GetCurrentDirectory(), path.Value),
                DirectoryPathTypeEnum.AppData => Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    this._environmentVariableService.GetCompany(),
                    this._environmentVariableService.GetProject(),
                    path.Value
                ),
                _ => throw new UnreachableException()
            };
        }

        public string GetRealPath(FilePath path)
        {
            return Path.Combine(this.GetRealPath(path.Directory), path.Name);
        }
    }
}
