import gulp from 'gulp'
import babel from 'gulp-babel'

const dirs = {
    entry: `./Src/Scripts/**/*.js`,
    dist: `./RelaxMiniGame/Scripts`
}

gulp.task('scripts', () => {
    return gulp.src(dirs.entry)
                .pipe(babel({
                    presets: ['es2015']
                }))
                .pipe(gulp.dest(dirs.dist))
})
