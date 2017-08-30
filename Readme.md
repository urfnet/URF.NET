This framework (over 45K+ downloads) minimizes the surface area of your ORM technology from disseminating in your application. This framework was deliberately designed to be lightweight, small in footprint size, and non-intimidating to extend and maintain. **When we say lightweight we really mean lightweight, when using this framework with the Entity Framework provider there are only 10 classes.** This lightweight framework will allow you to elegantly, unobtrusively, and easily patternize your applications and systems with Repository, Unit of Work, and Domain Driven Design. To use Generic Repositories or not? The framework allows the freedom of both, generic repositories and the ability to add in your own domain specific repository methods.

Live demo: [longle.azurewebsites.net](http://longle.azurewebsites.net)

![Architecture Overview (Sample Northwind Application & Framework)](https://lelong37.files.wordpress.com/2015/01/2015-01-03_19-15-001.png)

1. UI (Presentation) Layer 

          >>ASP.NET MVC - (Sample app: Northwind.Web) 
          >>Kendo UI - (Sample app: Northwind.Web) 
          >>AngularJS - (Sample app: Northwind.Web) 
  
2. Service and Data Layer 

          >>Repository Pattern - Framework (Repository.Pattern, Repository.Pattern.Ef6, Northwind.Repository)   
          >>Unit of Work Pattern - Framework (Repository.Pattern, Repository.Pattern.EF6, Northwind.Repository)   
          >>Entity Framework   
          >>Service Pattern - Framework (Service.Pattern, Northwind.Service) 
  
3. Domain Driven Design (*slated for release v4.0.0) 

          >>Domain Events   
          >>*more to come 
          
Technology Stack

Visual Studio 2013, Entity Framework 6, Sql Server 2014 / Sql Azure, Azure WebSite, ASP.NET MVC 5, [AngularJS](http://angularjs.org/), [Kendo UI](http://http//www.telerik.com/kendo-ui), [Angular Kendo](http://kendo-labs.github.io/angular-kendo/#/), Web Api 2, OData, [Entlib Unity](http://unity.codeplex.com/)

Subscribe to updates: [@lelong37](http://twitter.com/lelong37)

Roadmap:

- URF v5 Alpha ETA: 9/2017
- URF v5 Beta ETA: 10/2017
- URF v5 RC1 ETA: 11/2017

URF v5 major feature will include (self) Trackable Entities across physical boundaries without DbConext/DataConext, coming soon...!
