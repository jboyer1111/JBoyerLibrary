using JBoyerLibaray.FileSystem;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.HelperClasses
{

    [ExcludeFromCodeCoverage]
    public class FSHSetupInfo
    {

        public IFileSystemHelper FileSystemHelper { get; private set; }

        public Mock<IDirectoryWrapper> MockDirectory { get; private set; }

        public Mock<IFileWrapper> MockFile { get; private set; }

        public static IFileSystemHelper Default => new FSHSetupInfo().FileSystemHelper;

        /// <summary>
        /// Setups up an object to quickly mock up the FileSystemHelper class.
        /// This will allow tests access to the mock reference as to call verify methods if needed.
        /// </summary>
        public FSHSetupInfo()
        {
            MockDirectory = new Mock<IDirectoryWrapper>();
            MockFile = new Mock<IFileWrapper>();

            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.Directory).Returns(MockDirectory.Object);
            mockFileSystemHelper.Setup(f => f.File).Returns(MockFile.Object);

            FileSystemHelper = mockFileSystemHelper.Object;
        }

    }

}
