var should = require("should")
    Point = require("./test/Point.js");

var newPointBuilder = function() {
  var pointBuilder = Point.NewBuilder();
  pointBuilder.Column = 1;
  pointBuilder.Row = 2;
  return pointBuilder;
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
});
