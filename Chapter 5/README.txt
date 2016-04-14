Testing with F# - Chapter 5
---------------------------

This is the code bundle for testing with F# chapter 5. Dependencies to the project will be
downloaded at first compilation.

This project has database examples using type provider functionality. This means that you
will have to go through the following steps in order to get this code to compile.

1. Install Microsoft SQL Server 2014 Express from the following URL:
   http://www.microsoft.com/en-us/server-cloud/products/sql-server-editions/sql-server-express.aspx
   (no special setup is required, just that your local user is able to connect to the
   database and perform operations on it)

2. Open up Microsoft SQL Server 2014 Management Studio and connect to local database.

3. File / Open / File / setup/chapter05.sql

4. Execute!

5. File / Open / File / setup/chapter05_migrations.sql

6. Execute!

7. Open the solution file in Visual Studio 2013/Community.

8. Right click on chapter05.code.service project and "View In Browser", 
   the web service type provider is depending on this up and running.

9. Rebuild All

Now the project should be compiling.
Happy coding!

Best regards,
Mikael Lundin