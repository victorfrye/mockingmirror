import { useContext } from 'react';

import { DarkModeContext } from '@mockingmirror/theme/DarkMode';

const useDarkMode = () => useContext(DarkModeContext);

export default useDarkMode;
