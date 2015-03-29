var dot = require("dot"),
    glob = require("glob"),
    fs = require("fs"),
    path = require("path");

var templates = dot.process({path: __dirname});

glob("**/*.entity.json", {}, function(error, files) {
  if (error) return;
  for (var i = 0; i < files.length; ++i) {
    var file = files[i];
    fs.readFile(file, {encoding: "utf-8"}, function(error, data) {
      if (error) return;
      var fileName = path.join(path.dirname(file),
          path.basename(file).replace(".entity.json", ""));
      fs.writeFile(fileName + ".js", templates.javascript(JSON.parse(data)));
    });
  }
});
