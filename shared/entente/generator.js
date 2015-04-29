var dot = require("dot"),
    glob = require("glob"),
    fs = require("fs"),
    path = require("path");

dot.templateSettings.strip = false;
var templates = dot.process({path: __dirname});

var ensureFieldIsPresent = function(object, field) {
  if (object[field] == null) {
    throw new Error("Missing field " + field + " on object " + object.name);
  }
}

var PRIMITIVE_TYPES = ["int", "long", "float", "double", "string"];
var isPrimitive = function(type) {
  return PRIMITIVE_TYPES.indexOf(type) != -1;
}

var parseEnumValue = function(data) {
  ensureFieldIsPresent(data, "name");
  return data;
}

var parseField = function(data) {
  ensureFieldIsPresent(data, "name");
  ensureFieldIsPresent(data, "type");
  if (isPrimitive(data.type)) {
    data.primitive = true;
  }
  return data;
}

var parseInput = function(data) {
  var object = JSON.parse(data);
  ensureFieldIsPresent(object, "type");
  ensureFieldIsPresent(object, "name");
  if (object.type === "entity") {
    ensureFieldIsPresent(object, "fields");
    for (var i = 0; i < object.fields.length; ++i) {
      object.fields[i] = parseField(object.fields[i]);
    }
  } else if (object.type === "enum") {
    ensureFieldIsPresent(object, "values");
    for (var i = 0; i < object.values.length; ++i) {
      object.values[i] = parseEnumValue(object.values[i]);
    }
  }
  return object;
}

glob("**/*.entity.json", {}, function(error, files) {
  if (error) return;
  for (var i = 0; i < files.length; ++i) {
    fs.readFile(files[i], {encoding: "utf-8"}, function(file, error, data) {
      if (error) return;
      var fileName = path.join(path.dirname(file),
          path.basename(file).replace(".entity.json", ""));
      var object = parseInput(data);
      var output = "";

      if (object.type === "entity") {
        output += templates.unityscript_entity(object);
      } else if (object.type == "enum") {
        output += templates.unityscript_enum(object);
      } else {
        throw "Unknown object type.";
      }
      // Strip blank lines:
      output = output.replace(/\s+[\n]/g, "\n");
      output += "\n";

      fs.writeFile(fileName + ".js", output);
    }.bind(this, files[i]));
  }
});
