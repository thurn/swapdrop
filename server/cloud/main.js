var Game = Parse.Object.extend("Game");

Parse.Cloud.define("newGame", function(request, response) {
  var game = new Game();
  game.set("text", "Hello, world!");
  game.save(null, {
    success: function(newGame) {
      response.success(newGame.id);
    }
  });
});

Parse.Cloud.define("getGame", function(request, response) {
  var query = new Parse.Query(Game);
  query.get(request.params.gameId, {
    success: function(game) {
      response.success(game.get("text"));
    }
  });
});