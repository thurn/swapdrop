Entente is an entity class generator for JavaScript and C#. It processes JSON
files describing the desired entity classes and generates objects which are
immutable by default and can be constructed by following a Builder pattern. It
generates a C# version and a Javascript version of each entity with an
identical API. In this way, code can be written in either language to interact
with the entities.

## Types

Entente supports fields with the following primitive types:

* int32
* int64
* float32
* float64
* boolean
* string

In addition to these primitives, fields can be declared as any Entity type, provided that
an entity with the given name is found in the input JSON.

Furthermore, any field can include "repeated": true in its JSON to generate a List containing
values of the indicated type. Repeated field names should be singular, not pluralized.

## Example

Here is an example entity description file:

    {
      "type": "entity",
      "name": "Point",
      "desc": "Represents a point in a 2D grid.",
      "fields": [
        {
          "name": "Column",
          "type": "int",
          "desc": "The column number (0-based) of the point."
        },
        {
          "name": "Row",
          "type": "int",
          "desc": "The row number (0-based) of the point."
        }
      ]
    }

To create a new Point object, you need obtain a new PointBuilder by calling the NewBuilder
method. You can then set the desired values on the Builder object and call Build() when
the object is done being constructed.

    var point = Point.NewBuilder()
      .SetColumn(2)
      .SetRow(3)
      .Build();

Various other convenience methods are provided. For example, you can invoke ToBuilder() on
an entity to get back a Builder version of that entity. There are methods for getting a string
representation of an entity (ToString) and for comparing entities for equality (Equals).


## Reserved Property Names

Some property names are reserved for use by the generated code. Properties cannot
be named "Equals", "ToString", "ToBuilder", or "Build".