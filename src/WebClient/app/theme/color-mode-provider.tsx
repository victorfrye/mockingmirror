'use client';

import {
  ReactNode,
  createContext,
  useCallback,
  useEffect,
  useMemo,
  useState,
} from 'react';

import { useLocalStorage } from '@mockingmirror/storage';
import ColorMode from '@mockingmirror/theme/color-mode';
import initColorMode from '@mockingmirror/theme/init-color-mode';
import ThemePreferences from '@mockingmirror/theme/theme-preferences';
import useThemeMediaQuery from '@mockingmirror/theme/use-theme-media-query';

interface ColorModeContextProps {
  colorMode: ColorMode;
  isLight: boolean;
  isDark: boolean;
  onColorModeToggle: () => void;
  onColorModeChange: (colorMode: ColorMode) => void;
}

const initialMode = initColorMode();

export const ColorModeContext = createContext<ColorModeContextProps>({
  colorMode: initialMode,
  isLight: initialMode === 'light',
  isDark: initialMode === 'dark',
  /* eslint-disable @typescript-eslint/no-unused-vars, @typescript-eslint/no-empty-function */
  onColorModeToggle: () => { },
  onColorModeChange: (_mode: ColorMode) => { },
  /* eslint-enable @typescript-eslint/no-unused-vars, @typescript-eslint/no-empty-function */
});

interface ColorModeProviderProps {
  children: ReactNode;
}

export default function ColorModeProvider({
  children,
}: Readonly<ColorModeProviderProps>) {
  const {
    value: themePreferences,
    handleValueChange: handleThemePreferencesChange,
  } = useLocalStorage<ThemePreferences>('theme');
  const systemPrefersDark = useThemeMediaQuery();

  const [colorMode, setColorMode] = useState<ColorMode>(
    themePreferences?.colorMode ?? (systemPrefersDark ? 'dark' : 'light')
  );

  const isLight = colorMode === 'light';
  const isDark = colorMode === 'dark';

  const handleColorModeChange = useCallback(
    (colorMode: ColorMode) => {
      handleThemePreferencesChange({
        ...themePreferences,
        colorMode: colorMode,
      });
      setColorMode(colorMode);
    },
    [handleThemePreferencesChange, themePreferences]
  );

  const handleColorModeToggle = useCallback(() => {
    const newMode = colorMode === 'dark' ? 'light' : 'dark';

    handleColorModeChange(newMode);
  }, [colorMode, handleColorModeChange]);

  const context = useMemo(
    () => ({
      colorMode,
      isLight,
      isDark,
      onColorModeToggle: handleColorModeToggle,
      onColorModeChange: handleColorModeChange,
    }),
    [colorMode, isLight, isDark, handleColorModeToggle, handleColorModeChange]
  );

  /* eslint-disable react-hooks/set-state-in-effect */
  useEffect(() => {
    if (themePreferences?.colorMode !== undefined) {
      return;
    }

    setColorMode(systemPrefersDark ? 'dark' : 'light');
  }, [themePreferences, systemPrefersDark]);
  /* eslint-enable react-hooks/set-state-in-effect */

  return (
    <ColorModeContext.Provider value={context}>
      {children}
    </ColorModeContext.Provider>
  );
}
