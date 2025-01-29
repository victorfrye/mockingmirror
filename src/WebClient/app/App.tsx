import DarkModeProvider from '@mockingmirror/providers/DarkMode';
import ThemeProvider from '@mockingmirror/providers/Theme';
import Mirror from '@mockingmirror/components/Mirror';

const App = () => {
  return (
    <DarkModeProvider>
      <ThemeProvider>
        <Mirror />
      </ThemeProvider>
    </DarkModeProvider>
  );
};

export default App;
