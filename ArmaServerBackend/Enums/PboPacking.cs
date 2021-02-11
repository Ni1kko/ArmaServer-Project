﻿namespace ArmaServerBackend
{
    /// <summary>
    /// Defines the method of PBO packing.
    /// </summary>
    public enum PboPacking
    {
        /// <summary>
        /// Uncompressed.
        /// </summary>
        Uncompressed = 0,

        /// <summary>
        /// Compressed with run length encoding.
        /// </summary>
        Packed = 0x43707273,

        /// <summary>
        /// Internal use for product metadata.
        /// </summary>
        ProductEntry = 0x56657273
    }
}
