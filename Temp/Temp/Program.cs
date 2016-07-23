using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Temp
{
    class Program
    {
        private static string imageRetrievalURL = "https://uccxmlpre.scc.virginia.gov/Filing/Documents/";
        private static string imageRetrievalUsername = "jdillon@nationalcorp.com";
        private static string imageRetrievalPassword = "upF16tstng";

        static void Main(string[] args)
        {
            string outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Test.pdf");
            const string fileNumber = "16071172013";
            var bytes = getImageFromState(fileNumber);

            File.WriteAllBytes(outputPath, bytes);
        }

        static byte[] getImageFromState(string fileNumber)
        {
            using (MemoryStream pdfMs = new MemoryStream())
            {
                byte[] readBuffer = new byte[1024];
                byte[] pdfBytes = new byte[0];

                // Go out and get the image. Usually done with a GET to their server.
                var request = (HttpWebRequest)HttpWebRequest.Create(imageRetrievalURL + fileNumber);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Credentials = new NetworkCredential(imageRetrievalUsername, imageRetrievalPassword);

                int bytesRead = 0;
                using (Stream response = request.GetResponse().GetResponseStream())
                {
                    while ((bytesRead = response.Read(readBuffer, 0, 1024)) > 0)
                    {
                        pdfMs.Write(readBuffer, 0, bytesRead);
                    }
                }

                pdfMs.Seek(0, SeekOrigin.Begin);
                byte[] responseCheck = new byte[5];
                pdfMs.Read(responseCheck, 0, 5);
                pdfMs.Seek(0, SeekOrigin.Begin);

                byte[] pdfCheck = new byte[4];
                Array.ConstrainedCopy(responseCheck, 0, pdfCheck, 0, 4);

                if (pdfCheck.SequenceEqual(new byte[] { 37, 80, 68, 70 }))
                {
                    // PDF file if first 4 bytes are %PDF.
                    pdfBytes = pdfMs.ToArray();
                }
                else if (responseCheck.SequenceEqual(new byte[] { 69, 114, 114, 111, 114 }))
                {
                    // Else if Error then send out an alert.
                    string errorString = Encoding.UTF8.GetString(pdfMs.ToArray());
                    throw new Exception(String.Format(
                        "A# {0} - Error in XML filing image retrieval - State returned an error in the response string. {1}"
                        , 12345
                        , errorString));
                }
                else
                {
                    // This isn't necessarily an error situation because each state sends back things differently.
                    // The image could be blank because it isn't ready or they could be sending us more information about why it didn't come back.
                    string receivedString = Encoding.UTF8.GetString(pdfMs.ToArray());
                }

                pdfMs.Close();

                return pdfBytes;
            }
        }
    }
}
