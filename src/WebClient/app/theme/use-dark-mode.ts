import { useContext } from 'react';

import { DarkModeContext } from '@mockingmirror/theme/dark-mode-provider';

export default function useDarkMode() {
  return useContext(DarkModeContext);
}
