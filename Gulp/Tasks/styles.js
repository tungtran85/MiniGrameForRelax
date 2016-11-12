import gulp from 'gulp'
import postcss from 'gulp-postcss'
import cssnano from 'cssnano'
import postcssCssnext from 'postcss-cssnext'
import postcssImport from 'postcss-import'
import postcssNested from 'postcss-nested'

const dirs = {
    entry: './Src/Styles/app.css',
    dist: './RelaxMiniGame/Content/Styles/'
}

gulp.task('styles', () => {
    let processors = [
        postcssImport(),
        postcssNested(),
        postcssCssnext({
            browsers: 'last 1 version',
            warnForDuplicates: false
        }),
        cssnano()
    ]

    return gulp.src(dirs.entry)
                .pipe(postcss(processors))
                .pipe(gulp.dest(dirs.dist))
})
