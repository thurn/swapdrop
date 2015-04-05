var gulp = require("gulp"),
    replace = require("gulp-replace"),
    concat = require("gulp-concat");

gulp.task("default", function() {
  gulp.src(["src/**/*.js"])
    .pipe(concat('app.js'))
    .pipe(replace("#pragma strict\n\n", ""))
    .pipe(replace(/ :(\w|\.)+/g, ""))
    .pipe(replace(/function (\w+)/g, "this.$1 = function"))
    .pipe(replace(/class (\w+)/g, "function $1()"))
    .pipe(gulp.dest('cloud'));
});
