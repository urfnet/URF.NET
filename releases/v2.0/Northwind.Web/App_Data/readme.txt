
Sql Server CE connection string, Database: Northwind.Web/App_Data/Northwind.sdf, 
this is the default since a demo of this site is deployed Azure Website PaaS, site using Sql Server CE is free, since it's not using Sql Azure
<add name="NorthwindContext" connectionString="Data Source=|DataDirectory|\Northwind.sdf;Persist Security Info=False;" providerName="System.Data.SqlServerCe.4.0" />

Sql Server LocalDb connection string, Database: Northwind.Web/App_Data/Northwind.mdf
<add name="NorthwindContext" connectionString="Data Source=(LocalDb)\v11.0;Initial Catalog=Northwind;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|\Northwind.mdf" providerName="System.Data.SqlClient" />