# Salto Back-end Code Test

## Scope
- registered users can log in
- API access controlled via JWT token
- Requests currently update locks real time.

## Tech Stack
- C# Asp.Net Core
- JWT Tokens
- EF Core

## Stack Choice Reason
The reason I decided to go with C# and an ASP.Net api is that It's the language I am most comfortable in building out APIs quickly, and it can easily be set up to get a working prototype ready for delivery.

I made use of JWT tokens for authentication as it gives a quick and easy way to set up a secure method of authentication.

For an ORM I went with EF Core, this has some benefits for being very quick to work with so can set up the prototype at speed.


## Architecture
The application follows a CRUD based Rest API making use of Services to handle database interactions to give a layer of abstraction and enable easier testing.

The endpoints perform the update in real time as opposed to making use of message queueing as I feel the nature of opening / closing locks should be on a very real time basis and a queue system would introduce potential delays.

This design does provide some scalability however there is some areas which can be greatly improved as detailed below.

## Areas which can be improved
Right now both controllers for User management and for Lock control / management are based within the same application - as we are making use of JWT tokens we could expand the token to contain more detailed user information allowing us to split the endpoints into different API Microservices. This would allow us to be able to scale the more in demand areas of the application without the overhead of scaling the other areas.

Another potential improvement would be to break out the models into a core library which can be used across multiple apps better suporting the above mentioned design changes. It would also allow for more segragated unit testing projects.

Moving away from real-time on some areas of the application could also be beneficial - for example user management areas could enqueue work - this would give potential speed improvements when interacting with locks while delegating things related to user management to be on a potentially slower queued system.

Another beneficial choice would be to expand the API which currently only supports single interactions - update one user etc into accepting multiple users so updates can be performed in bulk.

As the application is small now and interactions with the database are simplistic the use of EF core alone is suitable, for speed improvements as the application grew in complexity it may be beneficial to introduce a lower level tool such as Dapper to give more control over the queries.

Having a way to poll the actual hardware after the lock is 'unlocked', to identify when it has closed and locked by itself.

Add in better handling of errors and messages that a fed through as API responses.

Add in better test case coverage, and potentially move tests away from using the default memory database to using an in memory version of sqlite

Set up verying user groups to support different levels of access control - right now any users can set up door controls.

## Video demo of API
https://youtu.be/QJyBq4mfQSw
