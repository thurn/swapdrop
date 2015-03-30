var dot = require("dot"),
    glob = require("glob"),
    fs = require("fs"),
    path = require("path");

dot.templateSettings.strip = false;
var templates = dot.process({path: __dirname});

glob("**/*.entity.json", {}, function(error, files) {
  if (error) return;
  for (var i = 0; i < files.length; ++i) {
    var file = files[i];
    fs.readFile(file, {encoding: "utf-8"}, function(error, data) {
      if (error) return;
      var fileName = path.join(path.dirname(file),
          path.basename(file).replace(".entity.json", ""));
      var json = JSON.parse(data);
      var output = "";
      for (var j = 0; j < json.length; ++j) {
        var object = json[j];
        object.capitalize = function(string) {
          return string.charAt(0).toUpperCase() + string.slice(1);
        };

        if (object.type === "entity") {
          output += templates.javascriptEntity(object);
        } else if (object.enumName) {
          output += templates.javascriptEnum(object);
        } else {
          throw "Unknown object type.";
        }
        output += "\n\n";
      }
      fs.writeFile(fileName + ".js", output);
    });
  }
});
