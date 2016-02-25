var gulp = require('gulp'),
    del = require('del'),
    typescript = require('gulp-typescript'),
    tslint = require('gulp-tslint'),
    tscConfig = require('./tsconfig.json'),
    concat = require('gulp-concat'),
    uglify = require('gulp-uglify'),
    stripDebug = require('gulp-strip-debug'),
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

gulp.task('vendor', ['clean'], function(){
    return gulp
        .src([//'node_modules/es6-shim/es6-shim.js',
        'node_modules/systemjs/dist/system-polyfills.js',
        'node_modules/systemjs/dist/system.js',
        'node_modules/angular2/bundles/angular2-polyfills.js',
        'node_modules/rxjs/bundles/Rx.js',
        'node_modules/angular2/bundles/angular2.js'])
        .pipe(concat('vendor.js'))
        .pipe(stripDebug())
        //.pipe(uglify())
        .pipe(gulp.dest('dist/assets/js'));
});

gulp.task('app', ['clean'], function(){
    return gulp
        .src('src/app/**/*.ts')
        .pipe(typescript(tscConfig.compilerOptions))
        //.pipe(concat("app.js"))
        .pipe(stripDebug())
        //.pipe(uglify())
        .pipe(gulp.dest('dist/assets/js'));
});

function startExpress() {
   var app = express();
   app.use(express.static('dist'));
   app.listen(8080);
}

gulp.task('build', ['tslint', 'vendor', 'app', 'assets'])
gulp.task('default', ['build'], function(){
    startExpress();
});