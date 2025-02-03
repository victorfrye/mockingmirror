import { useContext } from 'react';

import { DarkModeContext } from '@mockingmirror/providers/DarkMode';

const useDarkMode = () => useContext(DarkModeContext);

export default useDarkMode;
