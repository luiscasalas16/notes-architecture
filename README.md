# notes-architecture

- History

  - N-Layered Architecture (2002)
  - Hexagon (Ports and Adapters) Architecture (2005)
  - Onion Architecture (2008)
  - Clean Architecture (2012)

- Clean Architecture

  - <https://www.educative.io/blog/clean-architecture-tutorial>
  - <https://github.com/kavaan/clean-architecture-solution-template/>
  - <https://awesome-architecture.com/clean-architecture/>

- Microservices

  - General
    - <https://microservices.io/index.html>
    - <https://www.educative.io/blog/clean-architecture-tutorial>
    - <https://medium.com/codenx/microservices-explained-d6839a24e3a2>
  - Books
    - <https://learn.microsoft.com/en-us/dotnet/architecture/microservices/>
    - <https://samnewman.io/books/building_microservices_2nd_edition/>
  - Patterns
    - <https://azure.microsoft.com/en-us/blog/design-patterns-for-microservices/>
    - <https://medium.com/codenx/microservice-architecture-decomposition-patterns-part-1-3ad5deb4e359>
    - <https://medium.com/codenx/microservice-architecture-decomposition-patterns-part-2-bee6d2da386b>
    - <https://medium.com/codenx/microservice-architecture-communications-part-1-bbf3662c815f>
    - <https://medium.com/codenx/microservice-architecture-communications-patternspart-2-38a12e3600a7>
    - <https://medium.com/codenx/microservice-architecture-communications-patterns-part-3-8b2c060a99e5>
    - <https://medium.com/codenx/microservice-architecture-communications-patterns-part-4-grpc-apis-f1eab7449264>
    - <https://medium.com/codenx/microservice-architecture-communications-patterns-part-5-api-gateways-patterns-service-af250b35d59f>
    - <https://medium.com/codenx/microservice-architecture-communications-patterns-part-6-asynchronous-communication-fan-out-1e354b625da5>
  - Problems
    - <https://blog.stackademic.com/part-1-microservice-challenges-that-senior-developers-are-solving-726a32126b42>
    - <https://blog.stackademic.com/part-2-microservice-challenges-senior-developers-are-solving-7ba6d3e12957>

- APIS

  - Best Practices
    - Use nouns instead of verbs. For example, instead of using /getAllClients to fetch all clients, use /clients
    - Use plural resource nouns. For example, instead of using /employee/:id/, use /employees/:id/
    - User proper status codes.
      - 200 for general success.
      - 4xx for bad requests.
      - 5xx for internal errors.
    - Don’t return plain text. Content-Type header to be application/JSON
    - Do proper error handling.
    - Use pagination. For example, /products?page=10&page_size=20
    - Use versioning. For example, /products/v1/4

- Software architect skills

  - Languages: (Java, C#, Python) choose the most appropriate language for the task
  - System design: architectures that are scalable, maintainable, and efficient. software not only meets current requirements but is also adaptable to future needs.
  - Database design: design effective database schemas, optimize queries, and select appropriate database systems.
  - Cloud: design systems that leverage the scalability, flexibility, and resilience offered by cloud environments.
  - Security: security skills are vital in safeguarding sensitive data, protecting against potential vulnerabilities, and ensuring that the software system remains resilient against cyber threats.
  - Performance: expertise to analyze and enhance the system’s performance under varying workloads, ensuring a smooth and reliable user experience.
  - Version control: manage code repositories effectively, implement sound branching strategies, and collaborate seamlessly with development teams.
  - DevOps: Docker and Kubernetes, continuous integration and continuous delivery (CI/CD)
  - APIs & Microservices

- Software Design Essential Concepts

  - <https://medium.com/aruva-io-tech/software-design-fundamentals-in-2023-essential-concepts-for-modern-software-developers-part-i-ae7d9893ff59>
  - <https://medium.com/aruva-io-tech/software-design-fundamentals-in-2023-essential-concepts-for-modern-software-developers-part-ii-b3d3af942088>
  - <https://medium.com/aruva-io-tech/software-design-fundamentals-in-2023-essential-concepts-for-modern-software-developers-part-iii-87a342ac9a0a>

- Patrones

  - <https://medium.com/@edin.sahbaz/exploring-design-patterns-in-net-core-204511a234cf>
  - <https://medium.com/@edin.sahbaz/comprehensive-guide-to-solid-principles-in-c-54d79e19b7d7>
  - <https://medium.com/@techworldwithmilan/how-to-select-a-design-pattern-567181b90e8c>

- Message Queues vs Event Bus vs Message Brokers
  - Message Queues.
    - It is designed to hold messages until a consumer service can process them. Use of point-to-point or producer-consumer communication model. One-to-one. The senders deliver the message and wait for acknowledgment.
    - When to use it:
      - Decoupling producer and consumer services.
      - Asynchronous processing of tasks.
    - Examples:
      - Order processing: Queue orders for one-by-one processing.
      - Data ingestion: Collect data at high velocity and process it when possible.
  - Event bus.
    - Designed for publishing events to many subscribers, Publish-Subscribe (pub-sub), one-to-many. The publishers and the subscribers don’t need to know about each other. High level of decoupling. Generally, the systems consume messages as they arrive instead of storing them. It does not guarantee delivery.
    - When to use it:
      - Real-time notifications across many subscribers.
      - Situations where many services need to react to specific events.
    - Examples.
      - Inventory System: Notify other services when a product is out of stock.
      - Monitoring system: Alert other departments when the system exceeds a certain threshold.
  - Message brokers.
    - A robust system that includes the features of both Event Bus and Message Queue. Pub-sub and Point-to-Point. Can be One-to-one, One-to-many, or many-to-many.
    - When to use it:
      - Complex data routing scenarios.
      - Many communication patterns within a single application.
      - Transformation and aggregation of messages.
    - Examples:
      - Multi-service orchestration: Coordinate tasks among various microservices using complex routing rules.
