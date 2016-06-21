import * as gulp from 'gulp';
import { join } from 'path';

import { APP_DEST } from '../../config';

export = () => {
  return gulp.src(join(APP_DEST, '**/*'))
    .pipe(gulp.dest('../wwwroot'));
};
