YandiContainer
==============

Yet Another Dependency Injection Container

A lightweight Portable Dependency Injection Container with Unity semantics, a small footprint and understandable code

Features

* Lifetime Support (ILifetime)
  * TransientLifetime: a new instance is created each time it is resolved
  * PerResolveLifetime: a single instance is used for each call to Resolve
  * ContainerLifetime: a single instance is held by the container and disposed when the container is disposed
  * HierarchicalLifetime: (not yet implemented)

* Factory Creation Support (IFactory)
  * DefaultFactory: The most complex constructor is chosen to instantiate the class and the arguments are resolved using the container
  * LambdaFactory: Allows you to provide a custom factory function for instantiating your class
  
* Registration
  * Named or Nameless Registration: Register a single type mapping, or multiple with each one having a different name
  * Auto Registration: a default registration is created on demand for classes not already registered with the container, using the TransientLifetime and DefaultFactory  
  * Registration through code
  * Configuration file based Registration: (not yet implemented)

* Portable Built from the ground up as a Portable Class Library supporting
  * .NET 4.0+
  * Silverlight 4+
  * Windows Phone 7+
  * .NET for Windows Store Apps
  * Xbox 
