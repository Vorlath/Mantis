using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Core.Files.Common;

namespace Mantis.Core.Files
{
    public class TextFile(FilePath path, string realPath, FileStream source) : ITextFile
    {
        private bool _dirty;
        private string? _content;
        private bool _disposedValue;

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public FilePath Path { get; } = path;

        public string RealPath { get; } = realPath;

        public FileStream Source { get; } = source;

        public string Content
        {
            get
            {
                if (this._content is null)
                {
                    this.Source.Position = 0;

                    Span<byte> buffer = stackalloc byte[(int)this.Source.Length];
                    this.Source.Read(buffer);

                    this._content = this.Encoding.GetString(buffer);
                }

                return this._content;
            }
            set
            {
                this._content = value;
                this._dirty = true;
            }
        }

        public void Delete()
        {
            this.Source.Dispose();
            File.Delete(this.RealPath);
        }

        public void Save()
        {
            if (this._dirty == false || this._content is null)
            {
                return;
            }

            Span<byte> buffer = stackalloc byte[this.Encoding.GetByteCount(this._content)];
            this.Encoding.GetBytes(this._content, buffer);

            this.Source.Position = 0;
            this.Source.Write(buffer);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null

                this.Save();
                this.Source.Dispose();

                this._disposedValue = true;
            }
        }

        // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~TextFile()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            this.Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}