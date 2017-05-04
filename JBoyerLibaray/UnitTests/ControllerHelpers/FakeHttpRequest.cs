using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace JBoyerLibaray.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class FakeHttpRequest : HttpRequestBase
    {
        NameValueCollection _collection;
        NameValueCollection _form;
        byte[] _fileBytes = new byte[] { };

        public FakeHttpRequest(bool isAjaxRequest)
        {
            _form = new NameValueCollection();
            _collection = new NameValueCollection();
            if (isAjaxRequest)
            {
                _collection.Add("X-Requested-With", "XMLHttpRequest");
            }
        }

        public override string this[string key]
        {
            get
            {
                return null;
            }
        }

        public override NameValueCollection Headers
        {

            get
            {
                return _collection;
            }
        }

        public override NameValueCollection QueryString
        {
            get
            {
                return new NameValueCollection();
            }
        }

        public override string ApplicationPath
        {
            get
            {
                return "/Test/";
            }
        }

        public override string AppRelativeCurrentExecutionFilePath
        {
            get
            {
                return "/Test/";
            }
        }

        public override NameValueCollection Form
        {
            get
            {
                return _form;
            }
        }

        public override string RequestType
        {
            get
            {
                return "";
            }
            set
            {
                var temp = value;
            }
        }

        public override Uri Url
        {
            get
            {
                return new Uri("http://localhost/", UriKind.Absolute);
            }
        }

        public override byte[] BinaryRead(int count)
        {
            if (_fileBytes.Length < count)
            {
                return _fileBytes;
            }

            return _fileBytes.Take(count).ToArray();
        }

        public override int ContentLength
        {
            get
            {
                return _fileBytes.Length;
            }
        }

        public void AddFileToRequest(byte[] fileBytes)
        {
            _fileBytes = fileBytes;
        }
    }
}
