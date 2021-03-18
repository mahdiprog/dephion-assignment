# Dephion Assignment

**Build and run:**

 - in root of repo type `docker-compose build` and then  `docker-compose up`
 - or run it inside visual studio



 I've created two separate microservice to show if I could do that (or maybe not)
 Each microservice includes 3 projects for API, ApplicationCoe and Infrastructure
 Messaging project has been used to share messages between microservices
 I've used MassTransit for comminucation between services. if the message handler should accept external messages (inter-microservice), it should implement IExternalConsomer interface. otherwise it will be used as internal message handler (messages inside a microservice by MediatR)
 In memory databse has been used with EF


 *What skipped for now:*

 - creating tests: Ive added the structure but indeed I was out of time for implementation
 - client-side validation
 - save and check meetings


