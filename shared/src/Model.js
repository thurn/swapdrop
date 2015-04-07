#pragma strict

class Model {
  function Model(arg :int) {
    this.y = Model.Static(arg);
  }

  private var x = 15;
  public var y :int;

  public function GetX() {
    return this.x;
  }

  public static function Static(value :String) {
    return value;
  }
}
