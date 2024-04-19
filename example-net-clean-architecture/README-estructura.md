# Estructura

- estructura original

```text
+ ProductCategories
+ Commands
    + CreateProductCategory
        - CreateProductCategoryCommand.cs
        - CreateProductCategoryCommandHandler.cs
        - CreateProductCategoryCommandValidator.cs
    + UpdateProductCategory
        - UpdateProductCategoryCommand.cs
        - UpdateProductCategoryCommandHandler.cs
        - UpdateProductCategoryCommandValidator.cs
    + DeleteProductCategory
        - DeleteProductCategoryCommand.cs
        - DeleteProductCategoryCommandHandler.cs
```

- estructura mejorada

```text
+ ProductCategories
    + Commands
        - CreateProductCategory.cs
        - UpdateProductCategory.cs
        - DeleteProductCategory.cs
```
