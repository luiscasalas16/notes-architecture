# Clases Base

## Validador

- implementación tradicional

  ```csharp
  public class CommandValidator : AbstractValidator<Command> {}
  ```

- implementación con clases base

  ```csharp
  public class CommandValidator : CommandValidatorBase<Command> {}
  ```

## Commands

- implementación tradicional sin retorno

  ```csharp
  public class Command : IRequest {}
  public class CommandHandler : IRequestHandler<Command>
  {
  //
  private readonly IProductCategoryRepository _repository;
  private readonly IMapper _mapper;
  private readonly ILogger _logger;
  //
  public CommandHandler(IProductCategoryRepository repository, IMapper mapper, ILogger logger)
  {
      _repository = repository;
      _mapper = mapper;
      _logger = logger;
  }
  }
  ```

- implementación tradicional con retorno

  ```csharp
  public class Command : IRequest<int> {}
  public class CommandHandler : IRequestHandler<Command, int>
  {
  //
  private readonly IProductCategoryRepository _repository;
  private readonly IMapper _mapper;
  private readonly ILogger _logger;
  //
  public CommandHandler(IProductCategoryRepository repository, IMapper mapper, ILogger logger)
  {
      _repository = repository;
      _mapper = mapper;
      _logger = logger;
  }
  }
  ```

- implementación con clases base sin retorno

  ```csharp
  public class Command : CommandBase {}
  public class CommandHandler : CommandHandlerBase<Command>
  {
  //
  private readonly IProductCategoryRepository _repository;
  //
  public CommandHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
      : base(mapper, logger)
  {
      _repository = repository;
  }
  }
  ```

- implementación con clases base con retorno

  ```csharp
  public class Command : CommandBase<int> {}
  public class CommandHandler : CommandHandlerBase<Command, int>
  {
  //
  private readonly IProductCategoryRepository _repository;
  //
  public CommandHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
      : base(repository, mapper, logger)
  {
      _repository = repository;
  }
  }
  ```

- implementación con clases base y repositorio defecto sin retorno

  ```csharp
  public class Command : CommandBase {}
  public class CommandHandler : CommandHandlerRepositoryBase<Command, IProductCategoryRepository>
  {
  //
  public CommandHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
      : base(repository, mapper, logger)
  {
  }
  }
  ```

- implementación con clases base y repositorio defecto con retorno

  ```csharp
  public class Command : CommandBase<int> {}
  public class CommandHandler : CommandHandlerRepositoryBase<Command, int, IProductCategoryRepository>
  {
  //
  public CommandHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
      : base(repository, mapper, logger)
  {
  }
  }
  ```

## Queries

- implementación tradicional

  ```csharp
  public class Query : IRequest<List<Response>> {}
  public class QueryHandler : IRequestHandler<Query, List<Response>>
  {
  //
  private readonly IProductCategoryRepository _repository;
  private readonly IMapper _mapper;
  private readonly ILogService _logger;
  //
  public QueryHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
  {
      _repository = repository;
      _mapper = mapper;
      _logger = logger;
  }
  }
  ```

- implementación con clases base

  ```csharp
  public class Query : QueryBase<List<Response>> {}
  public class QueryHandler : QueryHandlerBase<Query, List<Response>>
  {
  //
  private readonly IProductCategoryRepository _repository;
  //
  public QueryHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
      : base(mapper, logger)
  {
      _repository = repository;
  }
  }
  ```

- implementación con clases base y repositorio defecto

  ```csharp
  public class Query : QueryBase<List<Response>> {}
  public class QueryHandler : QueryHandlerRepositoryBase<Query, List<Response>, IProductCategoryRepository>
  {
  //
  public QueryHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
      : base(repository, mapper, logger)
  {
  }
  }
  ```

- uso de interfaces de mapeo de objetos

- implementación tradicional

  ```csharp
  public class MapProfile : Profile
  {
  public MapProfile()
  {
  CreateMap<ProductCategory, GetProductCategories.Response>();
  CreateMap<CreateProductCategory.Command, ProductCategory>();
  CreateMap<UpdateProductCategory.Command, ProductCategory>();
  }
  }
  ```

- implementación con interfaces

  ```csharp
  public class MapProfile : MapProfileBase
  {
  public MapProfile()
  : base(Assembly.GetExecutingAssembly())
  {
  //
  }
  }
  //
  public class CreateProductCategory
  {
  public class Command : CommandBase<int>, IMapTo<ProductCategory>
  }
  //
  public class UpdateProductCategory
  {
  public class Command : CommandBase, IMapTo<ProductCategory>
  }
  //
  public class GetProductCategories
  {
  public class Response : IMapFrom<ProductCategory>
  }
  ```
