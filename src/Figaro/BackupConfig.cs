using System;

namespace Figaro
{
    //file:///D:/DEV/dbxml/dbxml-6.0.18/dbxml/docs/api_reference/CXX/envset_backup_config.html

    /// <summary>
    /// Used to configure settings for backup operations.
    /// </summary>
    public class BackupConfig
    {
        /// <summary>
        /// Turning this on causes direct I/O to be used when writing pages to the disk. 
        /// </summary>
        /// <remarks>
        /// For some environments, direct I/O can provide faster write throughput, but usually it 
        /// is slower because the OS buffer pool offers asynchronous activity.
        /// <para>
        /// By default, this option is turned off.
        /// </para>
        /// <value>False</value>
        /// </remarks>
        public bool WriteDirect { get; set; }

        /// <summary>
        /// Configures the number of pages to read before pausing. 
        /// </summary>
        /// <remarks>
        /// Increasing this value increases the amount of I/O the backup process performs for any 
        /// given time interval. If your application is already heavily I/O bound, setting this value 
        /// to a lower number may help to improve your overall data throughput by reducing the I/O 
        /// demands placed on your system.
        /// <para>
        /// By default, all pages are read without a pause.
        /// </para>
        /// </remarks>
        public UInt32 PageReads { get; set; }

        /// <summary>
        /// Configures the number of microseconds to sleep between batches of reads. 
        /// </summary>
        /// <remarks>
        /// Increasing this value decreases the amount of I/O the backup process performs 
        /// for any given time interval. If your application is already heavily I/O bound, 
        /// setting this value to a higher number may help to improve your overall data throughput 
        /// by reducing the I/O demands placed on your system.
        /// </remarks>
        public UInt32 ReadSleep { get; set; }

        /// <summary>
        /// Configures the size of the buffer, in bytes, to read from the database. Default is 1 megabyte.
        /// </summary>
        /// <value>Defaults to one megabyte (1024).</value>
        public UInt32 BufferSize { get; set; }

        /// <summary>
        /// Gets or sets the options to use during the backup process. 
        /// </summary>
        public BackupOptions Options { get; set; }

        /// <summary>
        /// Gets or sets the backup path.
        /// </summary>
        public string BackupPath { get; set; }
    }
}
