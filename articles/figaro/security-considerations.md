# Security Considerations

When using environments, there are some security considerations to keep in mind:

## Database environment permissions

The directory used for the environment should have its permissions set to ensure that files in the environment are not accessible to users without appropriate permissions. Applications that add to the user's permissions (for example, UNIX ``setuid` or `setgid` commands), must be carefully checked to not permit illegal use of those permissions such as general file access in the environment directory.

## Environment Variables

Setting so that environment variables can be used during file naming can be dangerous. Setting those flags in Figaro applications with additional permissions (for example, UNIX `setuid` or `setgid` commands) could potentially allow users to read and write containers to which they would not normally have access.

## File Permissions

By default, Figaro always creates container and log files readable and writable by the owner and the group. The group ownership of created files is based on the system and directory defaults, and is not further specified by Figaro.


## Temporary Backing Files

If your Figaro application is also using Berkeley DB databases, then you should pay attention to temporary backing files.


If an unnamed database is created and the cache is too small to hold the database in memory, Berkeley DB will create a temporary physical file to enable it to page the database to disk as needed. In this case, environment variables such as `TMPDIR` may be used to specify the location of that temporary file. Although temporary backing files are created readable and writable by the owner only, some file systems may not sufficiently protect temporary files created in random directories from improper access. To be absolutely safe, applications storing sensitive data in unnamed databases should use the @Figaro.FigaroEnv.TempDirectory property to specify a temporary directory with known permissions.

