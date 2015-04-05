var dot = require("dot"),
    glob = require("glob"),
    fs = require("fs"),
    path = require("path");

dot.templateSettings.strip = false;
var templates = dot.process({path: __dirname});

glob("**/*.entity.json", {}, function(error, files) {
  if (error) return;
  for (var i = 0; i < files.length; ++i) {
    fs.readFile(files[i], {encoding: "utf-8"}, function(file, error, data) {
      if (error) return;
      var fileName = path.join(path.dirname(file),
          path.basename(file).replace(".entity.json", ""));
      var object = JSON.parse(data);
      var output = "";
      object.capitalize = function(string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
      };

      if (object.type === "entity") {
        output += templates.js_entity(object);
      } else if (object.type == "enum") {
        output += templates.js_enum(object);
      } else {
        throw "Unknown object type.";
      }
      output += "\n\n";
      fs.writeFile(fileName + ".js", output);
    }.bind(this, files[i]));
  }
});
