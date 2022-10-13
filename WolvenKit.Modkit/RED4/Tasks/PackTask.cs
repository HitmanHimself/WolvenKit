using System.IO;
using System.Threading.Tasks;
using SharpDX;

namespace CP77Tools.Tasks
{
    public partial class ConsoleFunctions
    {
        /// <summary>
        /// Packs a folder or list of folders to .archive files.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="outpath"></param>
        public int PackTask(DirectoryInfo[] path, DirectoryInfo outpath)
        {
            if (path == null || path.Length < 1)
            {
                _loggerService.Error("Please fill in an input path.");
                return 1;
            }

            var result = 0;
            Parallel.ForEach(path, file => result += PackTaskInner(file, outpath));
            return result;
        }

        private int PackTaskInner(DirectoryInfo path, DirectoryInfo outpath, int cp = 0)
        {
            #region checks

            if (path is null)
            {
                _loggerService.Error("Please fill in an input path.");
                return 1;
            }

            if (!path.Exists)
            {
                _loggerService.Error("Input path does not exist.");
                return 1;
            }

            var basedir = path;
            if (basedir?.Parent == null)
            {
                return 1;
            }

            DirectoryInfo outDir;
            if (outpath is null)
            {
                outDir = basedir.Parent;
            }
            else
            {
                outDir = outpath;
                if (!outDir.Exists)
                {
                    outDir = Directory.CreateDirectory(outpath.FullName);
                }
            }

            #endregion checks

            var ar = _modTools.Pack(basedir, outDir);
            if (ar != null)
            {
                _loggerService.Success($"Finished packing {ar.ArchiveAbsolutePath}.");
                return 0;
            }
            else
            {
                _loggerService.Error($"Packing failed.");
                return 1;
            }
        }
    }
}
