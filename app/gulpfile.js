var gulp = require('gulp'),
    del = require('del'),
    typescript = require('gulp-typescript'),
    sourcemaps = require('gulp-sourcemaps'),
    tslint = require('gulp-tslint'),
    tscConfig = require('./tsconfig.json'),
    express = require('express');

gulp.task('clean',function(){
    return del('dist/**/*.*');
});

gulp.task('tslint', function(){
    return gulp.src('src/app/**/*.ts')
        .pipe(tslint())
        .pipe(tslint.report('verbose')); 
});

gulp.task('assets', ['clean'], function(){
    return gulp.src(['src/index.html'])
        .pipe(gulp.dest('dist'));
});

gulp.task('compile', ['clean'], function(){
    return gulp
        .src('src/app/**/*.ts')
        .pipe(sourcemaps.init())
        .pipe(typescript(tscConfig.compilerOptions))
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest('dist/assets/js'));
});

function startExpress() {
   var app = express();
   app.use(express.static('dist'));
   app.listen(8080);
}

gulp.task('build', ['tslint', 'compile', 'assets'])
gulp.task('default', ['build'], function(){
    startExpress();
});