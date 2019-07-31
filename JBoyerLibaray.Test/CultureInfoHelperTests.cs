using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;

namespace JBoyerLibaray
{

    [TestClass, ExcludeFromCodeCoverage]
    public class CultureInfoHelperTests
    {

        [TestMethod]
        public void CultureInfoHelper_ExecuteInCultureChangesCultureForMethodByName()
        {
            // Arrange
            string result = null;

            // Act
            CultureInfoHelper.ExecuteInCulture("tr-Tr", () =>
            {
                result = "the mississippi is very long.".ToUpper();
            });

            // Assert
            Assert.AreEqual("THE MİSSİSSİPPİ İS VERY LONG.", result);
        }

        [TestMethod]
        public void CultureInfoHelper_ExecuteInCultureChangesBackOnceDoneByName()
        {
            // Arrange
            CultureInfo before = Thread.CurrentThread.CurrentCulture;
            CultureInfo during = null;

            // Act
            CultureInfoHelper.ExecuteInCulture("tr-Tr", () =>
            {
                during = Thread.CurrentThread.CurrentCulture;
            });

            CultureInfo after = Thread.CurrentThread.CurrentCulture;

            // Assert
            Assert.AreEqual(new CultureInfo("tr-Tr"), during);
            Assert.AreEqual(before, after);
        }

        [TestMethod]
        public void CultureInfoHelper_ExecuteInCultureChangesBackOnceDoneEvenIfErrorByName()
        {
            // Arrange
            CultureInfo before = Thread.CurrentThread.CurrentCulture;

            // Act
            try
            {
                CultureInfoHelper.ExecuteInCulture("tr-Tr", () =>
                {
                    throw new Exception();
                });
            }
            catch
            {
                // This is hear to catch the expcted exception and continue with test
            }

            CultureInfo after = Thread.CurrentThread.CurrentCulture;

            // Assert
            Assert.AreEqual(before, after);
        }

        [TestMethod]
        public void CultureInfoHelper_ExecuteInCultureChangesCultureForMethodByInfo()
        {
            // Arrange
            string result = null;

            // Act
            CultureInfoHelper.ExecuteInCulture(new CultureInfo("tr-Tr"), () =>
            {
                result = "the mississippi is very long.".ToUpper();
            });

            // Assert
            Assert.AreEqual("THE MİSSİSSİPPİ İS VERY LONG.", result);
        }

        [TestMethod]
        public void CultureInfoHelper_ExecuteInCultureChangesBackOnceDoneByInfo()
        {
            // Arrange
            CultureInfo before = Thread.CurrentThread.CurrentCulture;
            CultureInfo during = null;

            // Act
            CultureInfoHelper.ExecuteInCulture(new CultureInfo("tr-Tr"), () =>
            {
                during = Thread.CurrentThread.CurrentCulture;
            });

            CultureInfo after = Thread.CurrentThread.CurrentCulture;

            // Assert
            Assert.AreEqual(new CultureInfo("tr-Tr"), during);
            Assert.AreEqual(before, after);
        }

        [TestMethod]
        public void CultureInfoHelper_ExecuteInCultureChangesBackOnceDoneEvenIfErrorByInfo()
        {
            // Arrange
            CultureInfo before = Thread.CurrentThread.CurrentCulture;

            // Act
            try
            {
                CultureInfoHelper.ExecuteInCulture(new CultureInfo("tr-Tr"), () =>
                {
                    throw new Exception();
                });
            }
            catch
            {
                // This is hear to catch the expcted exception and continue with test
            }

            CultureInfo after = Thread.CurrentThread.CurrentCulture;

            // Assert
            Assert.AreEqual(before, after);
        }

        [TestMethod]
        public void CultureInfoHelper_SetCultureCreatesDisposableObjectByName()
        {
            // Arrange

            // Act
            IDisposable disposeable = CultureInfoHelper.SetCulture("tr-Tr");

            // Assert
            Assert.IsNotNull(disposeable);
            disposeable.Dispose();
        }

        [TestMethod]
        public void CultureInfoHelper_SetCultureCreatesDisposableObjectByInfo()
        {
            // Arrange

            // Act
            IDisposable disposeable = CultureInfoHelper.SetCulture(new CultureInfo("tr-Tr"));

            // Assert
            Assert.IsNotNull(disposeable);
            disposeable.Dispose();
        }

        [TestMethod]
        public void CultureInfoHelper_SetCultureChangesForUsingByName()
        {
            // Arrange
            string result = null;

            // Act
            using (CultureInfoHelper.SetCulture("tr-Tr"))
            {
                result = "the mississippi is very long.".ToUpper();
            };

            // Assert
            Assert.AreEqual("THE MİSSİSSİPPİ İS VERY LONG.", result);
        }

        [TestMethod]
        public void CultureInfoHelper_SetCultureChangesCultureByName()
        {
            // Arrange
            CultureInfo before = Thread.CurrentThread.CurrentCulture;
            CultureInfo during = null;

            // Act
            using (CultureInfoHelper.SetCulture("tr-Tr"))
            {
                during = Thread.CurrentThread.CurrentCulture;
            }

            // Assert
            Assert.AreNotEqual(before, during);
        }

        [TestMethod]
        public void CultureInfoHelper_SetCultureChangesBackOnceDoneEvenIfErrorByName()
        {
            // Arrange
            CultureInfo before = Thread.CurrentThread.CurrentCulture;

            // Act
            try
            {
                using (CultureInfoHelper.SetCulture("tr-Tr"))
                {
                    throw new Exception();
                }
            }
            catch
            {
                // This is hear to catch the expcted exception and continue with test
            }

            CultureInfo after = Thread.CurrentThread.CurrentCulture;

            // Assert
            Assert.AreEqual(before, after);
        }

        [TestMethod]
        public void CultureInfoHelper_SetCultureChangesForUsingByInfo()
        {
            // Arrange
            string result = null;

            // Act
            using (CultureInfoHelper.SetCulture(new CultureInfo("tr-Tr")))
            {
                result = "the mississippi is very long.".ToUpper();
            };

            // Assert
            Assert.AreEqual("THE MİSSİSSİPPİ İS VERY LONG.", result);
        }

        [TestMethod]
        public void CultureInfoHelper_SetCultureChangesCultureByInfo()
        {
            // Arrange
            CultureInfo before = Thread.CurrentThread.CurrentCulture;
            CultureInfo during = null;

            // Act
            using (CultureInfoHelper.SetCulture(new CultureInfo("tr-Tr")))
            {
                during = Thread.CurrentThread.CurrentCulture;
            }

            // Assert
            Assert.AreNotEqual(before, during);
        }

        [TestMethod]
        public void CultureInfoHelper_SetCultureChangesBackOnceDoneEvenIfErrorByInfo()
        {
            // Arrange
            CultureInfo before = Thread.CurrentThread.CurrentCulture;

            // Act
            try
            {
                using (CultureInfoHelper.SetCulture(new CultureInfo("tr-Tr")))
                {
                    throw new Exception();
                }
            }
            catch
            {
                // This is hear to catch the expcted exception and continue with test
            }

            CultureInfo after = Thread.CurrentThread.CurrentCulture;

            // Assert
            Assert.AreEqual(before, after);
        }

    }

}
