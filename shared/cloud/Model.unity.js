

var Model = Class = function(arg) {
    this.y = Model.Static(arg);
  }

  Class.prototype.x = 15;
  Class.prototype.y;

  Class.prototype.GetX = function() {
    return this.x;
  }

  Class.Static = function(value) {
    return value;
  }
