"use strict";

var should = require("should"),
    Point = require("./test/Point.js"),
    PointHolder = require("./test/PointHolder.js"),
    Colors = require("./test/Colors.js");

var newPointBuilder = function() {
  var pointBuilder = Point.NewBuilder();
  pointBuilder.Column = 1;
  pointBuilder.Row = 2;
  return pointBuilder;
}

var newHolderBuilder = function() {
  var holderBuilder = PointHolder.NewBuilder();
  holderBuilder.Name = "name";
  holderBuilder.Point = newPointBuilder();
  return holderBuilder;
}

describe('Point', function() {
  it("should provide a way to get a new builder", function() {
    var pointBuilder = newPointBuilder();
    var point = pointBuilder.Build();
    point.Column.should.equal(1);
    point.Row.should.equal(2);
  });

  it("should provide a string representation", function() {
    var point = newPointBuilder().Build();
    point.ToString().should.equal('Point {\n  "Column": 1,\n  "Row": 2\n}');
  });

  it("should provide object equality", function() {
    var point = newPointBuilder().Build();
    point.Equals(newPointBuilder().Build()).should.equal(true);
    newPointBuilder().Build().Equals(point).should.equal(true);
  });

  it("should not be possible to mutate a field", function() {
    var point = newPointBuilder().Build();
    should.throws(function() {
      point.Column = 5;
    });
  });

  it("should not be possible to add a field", function() {
    var point = newPointBuilder().Build();
    should.throws(function() {
      point.NewField = "seven";
    });
  });

  it("should not be possible to delete a field", function() {
    var point = newPointBuilder().Build();
    should.throws(function() {
      delete point.Row;
    });
  });

  it("should clone arguments to NewBuilder", function() {
    var array = [3];
    var builder = Point.NewBuilder({Column: array});
    array[0] = 4;
    builder.Column[0].should.equal(3);
  })
});

describe('PointBuilder', function() {
  it("should provide a string representation", function() {
    var builder = newPointBuilder();
    builder.ToString().should.equal(
        'PointBuilder {\n  "Column": 1,\n  "Row": 2\n}');
  });

  it("should provide object equality", function() {
    var builder = newPointBuilder();
    builder.Equals(newPointBuilder()).should.equal(true);
    newPointBuilder().Equals(builder).should.equal(true);
  });

  it("should not be possible to add a field", function() {
    var builder = newPointBuilder();
    should.throws(function() {
      builder.NewField = "seven";
    });
  });

  it("should not be possible to delete a field", function() {
    var builder = newPointBuilder();
    should.throws(function() {
      delete builder.Row;
    });
  });

  it("should default fields to null", function() {
    var builder = Point.NewBuilder();
    should.equal(builder.Row, null);
    should.equal(builder.Column, null);
  });
});

describe("PointHolder", function() {
  it("should be deeply immutable when built", function() {
    var holder = newHolderBuilder().Build();
    should.throws(function() {
      holder.Point.Row = 3;
    });
  });

  it("should recursively convert children in ToBuilder", function() {
    var holder = newHolderBuilder().Build();
    var builder = holder.ToBuilder();
    builder.Point.Row = 1;
    builder.Build().Point.Row.should.equal(1);
  });

  it("should allow mutation of nested builders", function() {
    var builder = newHolderBuilder();
    builder.Point.Column = 4;
    var holder = builder.Build();
    holder.Point.Column.should.equal(4);
  });

  it("should allow assignment of builders", function() {
    var pointBuilder = newPointBuilder();
    var holderBuilder = newHolderBuilder();
    pointBuilder.Column = 7;
    holderBuilder.Point = pointBuilder;
    pointBuilder.Row = 10;
    var holder = holderBuilder.Build();
    holder.Point.Row.should.equal(10);
    holder.Point.Column.should.equal(7);
  });

  it("should provide recursive equality", function() {
    var holder1 = newHolderBuilder().Build();
    var holder2 = newHolderBuilder().Build();
    holder1.Equals(holder2).should.equal(true);
    var builder = newHolderBuilder();
    builder.Point.Row = 7;
    var holder3 = builder.Build();
    holder1.Equals(holder3).should.equal(false);
  });
});

describe("Colors", function() {
  it("should have two colors that are not equal", function() {
    (Colors.Red == Colors.Blue).should.equal(false);
  });

  it("should have enum values that are equal to themselves", function() {
    (Colors.Red == Colors.Red).should.equal(true);
  });

  it("should not be possible to add new colors", function() {
    should.throws(function() {
      Colors.Yellow = "Colors.Yellow";
    });
  });

  it("should not be possible to change the values of colors", function() {
    should.throws(function() {
      Colors.Red = "Colors.Blue";
    });
  });
});
