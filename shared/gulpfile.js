var gulp = require("gulp"),
    replace = require("gulp-replace"),
    concat = require("gulp-concat"),
    repl = require("./unicycle"),
    wrapper = require("gulp-wrapper");

gulp.task("default", function() {
  gulp.src(["src/**/*.unity.js"])
    .pipe(concat('app.js'))
    .pipe(wrapper({
      header: "var Class;\n\"use strict\";\n"
    }))
    .pipe(replace("#pragma strict", ""))
    .pipe(replace(/ :(\w|\.)+/g, ""))
    .pipe(replace(/(private|public|protected|internal) var /g, "Class.prototype."))
    .pipe(replace(/(private|public|protected|internal)? static function (\w+)/g, "Class.$2 = function"))
    .pipe(replace(/(private|public|protected|internal)? function (\w+)/g, "Class.prototype.$2 = function"))
    .pipe(replace(/class (\w+) {\s*Class\.prototype\.\w+/g, "var $1 = Class"))
    .pipe(replace("\n}", ""))
    .pipe(gulp.dest('cloud'));
});

gulp.task("test", function() {
  gulp.src(["delete/*"])
    .pipe(repl("derek", "tereza"))
    .pipe(gulp.dest("two"))
});
