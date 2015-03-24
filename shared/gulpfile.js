var gulp = require('gulp'),
    sweetjs = require('gulp-sweetjs'),
    concat = require('gulp-concat');

gulp.task("default", function() {
  gulp.src(["macros/**/*.sjs", "src/**/*.js"])
    .pipe(concat('app.js'))
    .pipe(sweetjs({}))
    .pipe(gulp.dest('cloud'));
});