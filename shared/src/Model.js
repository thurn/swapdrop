#pragma strict

class Model {
  private var x :int;
  public var y :String;

  private function Model(arg1 :int, arg2 :Boo.Lang.Hash) {
  }

  public function NewGame(configuration :Boo.Lang.Hash, players :UnityScript.Lang.Array) {
    var four = function() {
      return 7;
    }
    var three = alert("hello!");
  }

  public static function Static(value :Boo.Lang.Hash) {
    return this.NewGame();
  }
}

var model = new Model();
