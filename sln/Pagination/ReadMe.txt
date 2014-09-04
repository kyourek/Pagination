**********
Change Log
**********

- 0.11.0
Made a few minor breaking changes by removing the IOrderedQueryable and IOrderedSource
references from the IOrderedSource and IOrderedPageSourceModel classes, respectively.
These properties are now handled by *new* keywords on the respective properties of the
base interfaces, ISource and IPageSourceModel.

Also added a factory method to create a page linker that only displays previous- and
next-page options.
