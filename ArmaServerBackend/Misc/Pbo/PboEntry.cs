﻿using System;

namespace ArmaServerBackend
{
    /// <summary>
    /// Represents a pbo file entry, immutable.
    /// </summary>
    public class PboEntry
    {
        #region Fields
        private PboPacking _packing;
        private string _name;
        private DateTimeOffset _timestamp;
        private PboFile _file;
        private long _size;
        private PboResolver _resolver;
        #endregion

        public string Name {
            get {
                return _name;
            }
        }

        public DateTimeOffset Timestamp {
            get {
                return _timestamp;
            }
        }

        /// <summary>
        /// Gets the size of the raw data.
        /// </summary>
        public long Size {
            get {
                return _size;
            }
        }

        public override string ToString() {
            return _name;
        }

        public PboResolver Resolver {
            get {
                return _resolver;
            }
        }

        public PboEntry(string name, long size, DateTimeOffset timestamp, PboFile file, PboResolver resolver) {
            _name = name;
            _size = size;
            _file = file;
            _timestamp = timestamp;
            _resolver = resolver;
        }
    }
}
